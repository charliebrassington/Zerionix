using Domain.ParserResults;
using Entrypoint;
using GlobalStore.Interfaces;
using GlobalStore.ManagerGlobalStore;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.DependencyInjection;
using Parser;
using Parser.BlockOperationParsers;
using Parser.Interfaces;
using Parser.Interfaces.BlockOperationParserInterfaces;
using Parser.Interfaces.OperationParserInterfaces;
using Parser.OperationParsers;
using Parser.ParserExecutors;
using Service;
using Service.Interfaces;
using Service.RulesService;


var services = new ServiceCollection();

services.AddTransient<IOperationParser<ISimpleAssignmentOperation, SimpleAssignmentResult>, SimpleAssignmentParser>();
services.AddTransient<IMethodGlobalStoreManager, MethodGlobalStoreManager>();

services.AddTransient<IBlockOperationParser, VariableValueParser>();
services.AddTransient<IBlockOperationParserExecutor, BlockOperationParserExecutor>();

services.AddTransient<IMethodParser, MethodParser>();

services.AddTransient<IRuleExpressionBuilderService, RuleExpressionBuilderService>();
services.AddTransient<IRuleValidatorParserService, RuleValidatorParserService>();
services.AddTransient<IRuleRunnerCompilerService, RuleRunnerCompilerService>();
services.AddTransient<IRulesService, RulesService>();

services.AddTransient<ITreeParser, TreeParser>();
services.AddTransient<IExecutorService, ExecutorService>();

services.AddTransient<AppRunner>();

var serviceProvider = services.BuildServiceProvider();
var appRunner = serviceProvider.GetRequiredService<AppRunner>();

await appRunner.Run(args);
