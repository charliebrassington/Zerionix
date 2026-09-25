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
        private readonly IBlockParserExecutor _blockParserExecutor;
        private readonly ILogger<MethodParser> _logger;

        public MethodParser(
            IBlockOperationParserExecutor blockOperationParserExecutor,
            IBlockParserExecutor blockParserExecutor,
            ILogger<MethodParser> logger)
        {
            _blockOperationParserExecutor = blockOperationParserExecutor;
            _blockParserExecutor = blockParserExecutor;
            _logger = logger;
        }

        public MethodGlobalStore ParseMethod(MethodDeclarationSyntax method, SemanticModel semanticModel)
        {

            var methodStore = new MethodGlobalStore { TracebackReference = method.SyntaxTree.FilePath };

            var cfg = ControlFlowGraph.Create(method, semanticModel);

            if (cfg == null)
                return methodStore;

            _logger.LogInformation("Parsing method: {method.Identifier}", method.Identifier);

            BasicBlock? blockToVisit = null;

            foreach (var block in cfg.Blocks)
            {
                if (blockToVisit != null && block != blockToVisit)
                {
                    continue;
                }

                foreach (var op in block.Operations)
                {
                    _logger.LogDebug("Parsing -> {method.Identifier}, Block: {block.Ordinal}, Op: {op.Kind}, Code: {op.Syntax}", method.Identifier, block.Ordinal, op.Kind, op.Syntax);

                    _blockOperationParserExecutor.Execute(op, methodStore);
                }

                _blockParserExecutor.Execute(block, methodStore);

                blockToVisit = GetNextBlockToVisit(block, methodStore);
            }

            return methodStore;
        }

        private BasicBlock? GetNextBlockToVisit(BasicBlock currentBlock, MethodGlobalStore methodGlobalStore)
        {
            var blocksToVisitNext = methodGlobalStore.BlockConstraints.Where(c => c.BlockFrom == currentBlock && c.HasPassedConstaint);

            return blocksToVisitNext.Count() == 1 ? blocksToVisitNext.First().BlockTo : currentBlock.FallThroughSuccessor?.Destination;
        }
    }
}
