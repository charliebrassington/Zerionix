using Microsoft.CodeAnalysis.CSharp;
using Zerionix.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Entrypoint
{
    public class AppRunner
    {
        private readonly IExecutorService _executorService;

        public AppRunner(IExecutorService executorService)
        {
            _executorService = executorService;
        }

        public async Task Run(string[] arguments)
        {
            var trees = Directory
                .GetFiles(arguments[0], "*.cs", SearchOption.AllDirectories)
                .Select(file =>
                    CSharpSyntaxTree.ParseText(File.ReadAllText(file), path: file))
                .ToList();

            var ruleList = File.ReadAllLines($"{arguments[0]}/RULES.txt").ToList();

            await _executorService.Execute(trees, ruleList);
        }
    }
}
