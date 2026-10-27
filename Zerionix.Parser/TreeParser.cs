using Zerionix.Domain.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Zerionix.Parser.Interfaces;


namespace Zerionix.Parser
{
    public class TreeParser : ITreeParser
    {
        private readonly IMethodParserManager _methodParserManager;

        public TreeParser(IMethodParserManager methodParserManager)
        {
            _methodParserManager = methodParserManager;
        }

        public List<MethodGlobalStore> ParseTrees(List<SyntaxTree> treeList)
        {
            var references = new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };

            var compilation = CSharpCompilation.Create("Analysis", treeList, references);
            var methodList = treeList.SelectMany(t => t.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()).ToList();

            var methodSemantics = treeList.Select(tree => new MethodSemantic
            {
                SemanticTreeModel = compilation.GetSemanticModel(tree),
                MethodDeclarationList = tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().ToList()
            }).ToList();

            var methodGlobalStoreList = _methodParserManager.HandleMethodParsing(methodSemantics);

            return methodGlobalStoreList;
        }
    }
}
