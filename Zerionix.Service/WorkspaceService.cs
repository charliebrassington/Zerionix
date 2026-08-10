using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Service.Interfaces;

namespace Zerionix.Service
{
    public class WorkspaceService : IWorkspaceService
    {
        public async Task<List<Document>> GetDocuments(string pathToWorkspace)
        {
            MSBuildLocator.RegisterDefaults();

            var workspace = MSBuildWorkspace.Create();

            var documents = Path.GetExtension(pathToWorkspace) == ".sln"
                ? (await workspace.OpenSolutionAsync(pathToWorkspace)).Projects.SelectMany(project => project.Documents)
                : (await workspace.OpenProjectAsync(pathToWorkspace)).Documents;

            return documents.ToList();
        }


        public async Task<List<SyntaxTree>> GetSyntaxTrees(List<Document> documents)
        {
            var trees = new List<SyntaxTree>();

            foreach (var document in documents)
            {
                var root = await document.GetSyntaxRootAsync();
                if (root != null && document.FilePath != null)
                {
                    trees.Add(root.SyntaxTree);
                }
            }

            return trees;
        }
    }
}
