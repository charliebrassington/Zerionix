using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Service.Interfaces
{
    public interface IWorkspaceService
    {
        Task<List<Document>> GetDocuments(string pathToWorkspace);
        Task<List<SyntaxTree>> GetSyntaxTrees(List<Document> documents);
    }
}
