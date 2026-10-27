using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Zerionix.Domain.Models;
using Zerionix.GlobalStore.Interfaces;
using Zerionix.Parser.ConverterParsers;
using Zerionix.Parser.Interfaces;
using Zerionix.Parser.Interfaces.EntrypointParser;

namespace Zerionix.Parser
{
    public class MethodParserManager : IMethodParserManager
    {
        private readonly IMethodParser _methodParser;
        private readonly IMethodGlobalStoreManager _methodGlobalStoreManager;
        private readonly IEntrypointDiscoveryParser _entrypointDiscoveryParser;

        public MethodParserManager(
            IMethodParser methodParser, 
            IMethodGlobalStoreManager methodGlobalStoreManager,
            IEntrypointDiscoveryParser entrypointDiscoveryParser)
        {
            _methodParser = methodParser;
            _methodGlobalStoreManager = methodGlobalStoreManager;
            _entrypointDiscoveryParser = entrypointDiscoveryParser;
        }

        public List<MethodGlobalStore> HandleMethodParsing(List<MethodSemantic> methodSemanticList)
        {
            var methodGlobalStoreList = new List<MethodGlobalStore>();

            var entrypointMethods = _entrypointDiscoveryParser.GetEntryPoints(methodSemanticList);

            foreach (var method in entrypointMethods)
            {
                HandleMethodCallParsing(method, methodSemanticList, methodGlobalStoreList);
            }

            return methodGlobalStoreList;
        }

        private void HandleMethodCallParsing(
            MethodDeclarationSyntax target,
            List<MethodSemantic> methodSemanticList,
            List<MethodGlobalStore> methodGlobalStoreList,
            MethodGlobalStore? targetMethodGlobalStore = null)
        {
            var methodGlobalStore = targetMethodGlobalStore ?? new MethodGlobalStore();

            var semanticModel = methodSemanticList
                .Where(m => m.MethodDeclarationList.Contains(target))
                .Select(m => m.SemanticTreeModel)
                .FirstOrDefault();

            if (semanticModel == null)
                return;

            _methodParser.ParseMethod(target, semanticModel, methodGlobalStore);

            if (methodGlobalStore.MethodState == MethodParsingState.Completed)
            {
                methodGlobalStoreList.Add(methodGlobalStore);
                return;
            }

            if (methodGlobalStore.MethodState == MethodParsingState.WaitingForMethodCall && methodGlobalStore.CurrentMethodCall != null)
            {
                var methodCallSymbol = methodSemanticList
                    .SelectMany(m => m.MethodDeclarationList)
                    .FirstOrDefault(methodDelclaration => methodGlobalStore.CurrentMethodCall.MethodSymbol.DeclaringSyntaxReferences.Any(r => r.GetSyntax() == methodDelclaration));

                if (methodCallSymbol != null)
                {
                    var methodGlobalStoreNested = new MethodGlobalStore();

                    MethodArgumentConverter.AddArgumentsToMethodStore(methodGlobalStoreNested, methodGlobalStore.CurrentMethodCall.ArgumentList);
                   
                    HandleMethodCallParsing(methodCallSymbol, methodSemanticList, methodGlobalStoreList, methodGlobalStoreNested);

                    MethodReturnConverter.AddReturnValuesToMethodStore(methodGlobalStore, methodGlobalStoreNested.ReturnValues);

                    if (methodGlobalStore.CurrentMethodCall.Return.ObjectName != null)
                    {
                        var symbolicValues = _methodGlobalStoreManager.FetchObjectSymbolicValues(methodGlobalStoreNested.SymbolicValueList, methodGlobalStore.CurrentMethodCall.Return.ObjectName);
                        methodGlobalStore.SymbolicValueList.AddRange(symbolicValues);
                    }
                    
                    methodGlobalStore.MethodState = MethodParsingState.Continue;
                }

                _methodParser.ParseMethod(target, semanticModel, methodGlobalStore);
                methodGlobalStoreList.Add(methodGlobalStore);
            }
        }
    }
}
