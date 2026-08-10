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
            var ruleList = File.ReadAllLines(arguments[1]).ToList();

            await _executorService.Execute(arguments[0], ruleList);
        }
    }
}
