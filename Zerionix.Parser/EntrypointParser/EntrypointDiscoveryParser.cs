using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Parser.Interfaces.EntrypointParser;

namespace Zerionix.Parser.EntrypointParser
{
    public class EntrypointDiscoveryParser : IEntrypointDiscoveryParser
    {
        public List<MethodDeclarationSyntax> GetEntryPoints(List<MethodSemantic> methodSemantics)
        {
            var allMethods = methodSemantics
                .SelectMany(ms => ms.MethodDeclarationList)
                .ToList();

            var calledMethods = new HashSet<IMethodSymbol>(
                SymbolEqualityComparer.Default);

            foreach (var methodSemantic in methodSemantics)
            {
                var semanticModel = methodSemantic.SemanticTreeModel;

                if (semanticModel is null)
                    continue;

                foreach (var method in methodSemantic.MethodDeclarationList)
                {
                    foreach (var invocation in method.DescendantNodes().OfType<InvocationExpressionSyntax>())
                    {
                        var symbol = semanticModel
                            .GetSymbolInfo(invocation)
                            .Symbol as IMethodSymbol;

                        if (symbol is not null)
                        {
                            calledMethods.Add(symbol.OriginalDefinition);
                        }
                    }
                }
            }

            return allMethods.Where(method => IsMethodEntrypoint(method, methodSemantics, calledMethods)).ToList();
        }

        private bool IsMethodEntrypoint(MethodDeclarationSyntax method, List<MethodSemantic> methodSemantics, HashSet<IMethodSymbol> calledMethods)
        {
            var semanticModel = methodSemantics.First(ms => ms.MethodDeclarationList.Contains(method)).SemanticTreeModel;

            if (semanticModel is null)
                return false;

            var symbol = semanticModel.GetDeclaredSymbol(method);

            return symbol is not null && !calledMethods.Contains(symbol.OriginalDefinition, SymbolEqualityComparer.Default);
        }
    }
}
