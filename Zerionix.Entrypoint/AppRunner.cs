
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text;
using Zerionix.Service.Interfaces;

namespace Zerionix.Entrypoint
{
    public class AppRunner
    {
        private readonly IExecutorService _executorService;

        public AppRunner(IExecutorService executorService)
        {
            _executorService = executorService;
        }

        private void SetupCommandAction(RootCommand rootCommand, Option<FileInfo> workspaceOption, Option<FileInfo> ruleOption)
        {
            rootCommand.SetAction(async parseResult =>
            {
                var workspaceFile = parseResult.GetValue(workspaceOption)!;
                var ruleFile = parseResult.GetValue(ruleOption)!;

                var ruleList = await File.ReadAllLinesAsync(ruleFile.FullName);

                await _executorService.Execute(
                    workspaceFile.FullName,
                    ruleList.ToList());
            });
        }

        public async Task Run(string[] arguments)
        {
            var workspaceFileOption = new Option<FileInfo>("--workspace-file")
            {
                Description = "The path to the workspace file to load and parse (.sln/.csproj)",
                Required = true
            };

            var ruleFileOption = new Option<FileInfo>("--rule-file")
            {
                Description = "The path to the file containing domain rules (.txt)",
                Required = true
            };

            var rootCommand = new RootCommand("Zerionix - Roslyn semantic static analysis engine build for .NET")
            {
                workspaceFileOption,
                ruleFileOption
            };

            SetupCommandAction(rootCommand, workspaceFileOption, ruleFileOption);

            await rootCommand.Parse(arguments).InvokeAsync();
        }
    }
}
