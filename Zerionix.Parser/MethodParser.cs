using Zerionix.Domain.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Zerionix.Parser.BlockOperationParsers;
using Zerionix.Parser.Interfaces;
using Zerionix.Parser.Interfaces.BlockOperationParserInterfaces;
using Zerionix.Parser.ParserExecutors;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Zerionix.Parser
{
    public class MethodParser : IMethodParser
    {
        private readonly IBlockOperationParserExecutor _blockOperationParserExecutor;
        private readonly ILogger<MethodParser> _logger;

        public MethodParser(IBlockOperationParserExecutor blockOperationParserExecutor, ILogger<MethodParser> logger)
        {
            _blockOperationParserExecutor = blockOperationParserExecutor;
            _logger = logger;
        }

        public MethodGlobalStore ParseMethod(MethodDeclarationSyntax method, SemanticModel semanticModel)
        {
            var methodStore = new MethodGlobalStore { TracebackReference = method.SyntaxTree.FilePath };

            var cfg = ControlFlowGraph.Create(method, semanticModel);

            if (cfg == null)
                return methodStore;

            _logger.LogInformation("Parsing method: {method.Identifier}", method.Identifier);

            foreach (var block in cfg.Blocks)
            {
                foreach (var op in block.Operations)
                {
                    _logger.LogDebug("Parsing -> {method.Identifier}, Block: {block.Ordinal}, Op: {op.Kind}, Code: {op.Syntax}", method.Identifier, block.Ordinal, op.Kind, op.Syntax);

                    _blockOperationParserExecutor.Execute(op, methodStore);
                }
            }

            return methodStore;
        }
    }
}
