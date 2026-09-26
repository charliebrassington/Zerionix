using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zerionix.Domain.ParserResults;
using Zerionix.Entrypoint;
using Zerionix.GlobalStore.Interfaces;
using Zerionix.GlobalStore.ManagerGlobalStore;
using Zerionix.Parser;
using Zerionix.Parser.BlockOperationParsers;
using Zerionix.Parser.BlockParsers;
using Zerionix.Parser.ConditionParsers;
using Zerionix.Parser.ConditionParsers.BinaryOperationEvaluationParser;
using Zerionix.Parser.HelperParsers;
using Zerionix.Parser.Interfaces;
using Zerionix.Parser.Interfaces.BlockOperationParserInterfaces;
using Zerionix.Parser.Interfaces.ConditionParserInterfaces;
using Zerionix.Parser.Interfaces.HelperParserInterfaces;
using Zerionix.Parser.Interfaces.OperationParserInterfaces;
using Zerionix.Parser.OperationParsers;
using Zerionix.Parser.ParserExecutors;
using Zerionix.Service;
using Zerionix.Service.Interfaces;
using Zerionix.Service.RulesService;


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

// Global helpers to host logic in one place where duplication exists
services.AddTransient<IBinaryOperatorMathHelper, BinaryOperatorMathHelper>();


services.AddTransient<ICompareValueParser, CompareValueParser>();
services.AddTransient<IEvaluateValueParser, EvaluateValueParser>();

services.AddTransient<IBinaryOperationEvaluationParser, EvaluateBinaryParser>();
services.AddTransient<IBinaryOperationEvaluationParser, EvaluateLiteralParser>();
services.AddTransient<IBinaryOperationEvaluationParser, EvaluateLocalParser>();
services.AddTransient<IBinaryOperationEvaluationParser, EvaluateAndOrParser>();

services.AddTransient<IConditionEvaluatorParser, ConditionEvaluatorParser>();

services.AddTransient<IOperationParser<IBinaryOperation, BinaryOperationMathResult>, BinaryOperationMathParser>();
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
