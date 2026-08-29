using Zerionix.Domain.ParserResults;
using Zerionix.Entrypoint;
using Zerionix.GlobalStore.Interfaces;
using Zerionix.GlobalStore.ManagerGlobalStore;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zerionix.Parser;
using Zerionix.Parser.BlockOperationParsers;
using Zerionix.Parser.Interfaces;
using Zerionix.Parser.Interfaces.BlockOperationParserInterfaces;
using Zerionix.Parser.Interfaces.OperationParserInterfaces;
using Zerionix.Parser.OperationParsers;
using Zerionix.Parser.ParserExecutors;
using Zerionix.Service;
using Zerionix.Service.Interfaces;
using Zerionix.Service.RulesService;
using Zerionix.Parser.BlockParsers;


var services = new ServiceCollection();

services.AddLogging(builder =>
{
    builder.AddConsole();

#if DEBUG
    builder.SetMinimumLevel(LogLevel.Debug);
#else
    builder.SetMinimumLevel(LogLevel.Information);
#endif
});

services.AddTransient<IOperationParser<ISimpleAssignmentOperation, SimpleAssignmentResult>, SimpleAssignmentParser>();
services.AddTransient<IMethodGlobalStoreManager, MethodGlobalStoreManager>();

services.AddTransient<IBlockOperationParser, VariableValueParser>();
services.AddTransient<IBlockOperationParserExecutor, BlockOperationParserExecutor>();

services.AddTransient<IBlockParser, ConditionParser>();
services.AddTransient<IBlockParserExecutor, BlockParserExecutor>();

services.AddTransient<IMethodParser, MethodParser>();

services.AddTransient<IRuleExpressionBuilderService, RuleExpressionBuilderService>();
services.AddTransient<IRuleValidatorParserService, RuleValidatorParserService>();
services.AddTransient<IRuleRunnerCompilerService, RuleRunnerCompilerService>();
services.AddTransient<IRulesService, RulesService>();

services.AddTransient<ITreeParser, TreeParser>();

services.AddTransient<IWorkspaceService, WorkspaceService>();
services.AddTransient<IExecutorService, ExecutorService>();

services.AddTransient<AppRunner>();

var serviceProvider = services.BuildServiceProvider();
var appRunner = serviceProvider.GetRequiredService<AppRunner>();

await appRunner.Run(args);
