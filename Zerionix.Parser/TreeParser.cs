using Zerionix.Domain.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Zerionix.Parser.Interfaces;


namespace Zerionix.Parser
{
    public class TreeParser : ITreeParser
    {
        private readonly IMethodParser _methodParser;

        public TreeParser(IMethodParser methodParser)
        {
            _methodParser = methodParser;
        }

        public List<MethodGlobalStore> ParseTrees(List<SyntaxTree> treeList)
        {
            var references = new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };

            var compilation = CSharpCompilation.Create("Analysis", treeList, references);
            var methodGlobalStoreList = new List<MethodGlobalStore>();  

            foreach (var tree in treeList)
            {
                var semanticModel = compilation.GetSemanticModel(tree);

                var root = tree.GetRoot();

                foreach (var method in root.DescendantNodes().OfType<MethodDeclarationSyntax>())
                {
                    methodGlobalStoreList.Add(_methodParser.ParseMethod(method, semanticModel));
                }
            }

            return methodGlobalStoreList;
        }
    }
}
