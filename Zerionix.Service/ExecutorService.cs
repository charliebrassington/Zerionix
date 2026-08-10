using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Parser.Interfaces;
using Zerionix.Service.Interfaces;

namespace Zerionix.Service
{
    public class ExecutorService : IExecutorService
    {
        private readonly ITreeParser _treeParser;
        private readonly IRulesService _rulesService;
        private readonly IWorkspaceService _workspaceService;
        private readonly ILogger<ExecutorService> _logger;

        public ExecutorService(ITreeParser treeParser, IRulesService rulesService, IWorkspaceService workspaceService, ILogger<ExecutorService> logger)
        {
            _treeParser = treeParser;
            _rulesService = rulesService;
            _workspaceService = workspaceService;
            _logger = logger;
        }

        public async Task Execute(string workspacePath, List<string> ruleList)
        {
            var documents = await _workspaceService.GetDocuments(workspacePath);
            var trees = await _workspaceService.GetSyntaxTrees(documents);

            var methodGlobalStoreList = _treeParser.ParseTrees(trees);

            foreach (var methodGlobalStore in methodGlobalStoreList)
            {
                var ruleCheckResults = await _rulesService.CheckRulesForViolations(ruleList, methodGlobalStore.SymbolicValueList);

                if (ruleCheckResults == null)
                    continue;

                foreach (var ruleResultPair in ruleCheckResults.Where(kv => !kv.Value))
                {
                    _logger.LogInformation("Rule {ruleResultPair.Key} was violated in {methodGlobalStore.TracebackReference}", ruleResultPair.Key, methodGlobalStore.TracebackReference);
                }
            }
        }
    }
}
