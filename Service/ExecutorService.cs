using Domain.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Parser.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ExecutorService : IExecutorService
    {
        private readonly ITreeParser _treeParser;
        private readonly IRulesService _rulesService;

        public ExecutorService(ITreeParser treeParser, IRulesService rulesService)
        {
            _treeParser = treeParser;
            _rulesService = rulesService;
        }

        public async Task Execute(List<SyntaxTree> treeList, List<string> ruleList)
        {
            var methodGlobalStoreList = _treeParser.ParseTrees(treeList);
            var symbolicValuesList = methodGlobalStoreList.SelectMany(m => m.SymbolicValueList).ToList();

            var ruleCheckResults = await _rulesService.CheckRulesForViolations(ruleList, symbolicValuesList);

            if (ruleCheckResults == null)
                return;

            foreach (var ruleResultPair in ruleCheckResults)
            {
                Console.WriteLine($"Rule: {ruleResultPair.Key} Rule Passed: {ruleResultPair.Value}");
            }
        }
    }
}
