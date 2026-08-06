using Zerionix.Domain.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Zerionix.Parser.Interfaces;
using Zerionix.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Zerionix.Service
{
    public class ExecutorService : IExecutorService
    {
        private readonly ITreeParser _treeParser;
        private readonly IRulesService _rulesService;
        private readonly ILogger<ExecutorService> _logger;

        public ExecutorService(ITreeParser treeParser, IRulesService rulesService, ILogger<ExecutorService> logger)
        {
            _treeParser = treeParser;
            _rulesService = rulesService;
            _logger = logger;
        }

        public async Task Execute(List<SyntaxTree> treeList, List<string> ruleList)
        {
            var methodGlobalStoreList = _treeParser.ParseTrees(treeList);

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
