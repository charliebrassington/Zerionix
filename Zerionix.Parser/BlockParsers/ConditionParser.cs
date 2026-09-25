using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Parser.ConditionParsers;
using Zerionix.Parser.Interfaces;
using Zerionix.Parser.Interfaces.ConditionParserInterfaces;

namespace Zerionix.Parser.BlockParsers
{
    public class ConditionParser : IBlockParser
    {
        private readonly IConditionEvaluatorParser _conditionEvaluatorParser;
        private readonly ILogger<ConditionParser> _logger;

        public ConditionParser(IConditionEvaluatorParser conditionEvaluatorParser, ILogger<ConditionParser> logger)
        {
            _conditionEvaluatorParser = conditionEvaluatorParser;
            _logger = logger;
        }

        public bool CanHandle(BasicBlock block) => block.ConditionalSuccessor != null && block.BranchValue != null;

        public void ParseBlock(BasicBlock block, MethodGlobalStore store)
        {
            var result = _conditionEvaluatorParser.Evaluate(block.BranchValue, store);

            var conditionalBlock = block.ConditionalSuccessor?.Destination;
            var fallThroughBlock = block.FallThroughSuccessor?.Destination;

            var conditionKindEvalDict = new Dictionary<ControlFlowConditionKind, EvaluationResult>
            {
                { ControlFlowConditionKind.WhenTrue, EvaluationResult.True },
                { ControlFlowConditionKind.WhenFalse, EvaluationResult.False }
            };

            conditionKindEvalDict.TryGetValue(block.ConditionKind, out var evalResult);

            if (conditionalBlock != null)
            {
                store.BlockConstraints.Add(new BlockConstraint
                {
                    BlockFrom = block,
                    BlockTo = conditionalBlock,
                    HasPassedConstaint = evalResult == result || result == EvaluationResult.Unknown
                });
            }

            if (fallThroughBlock != null)
            {
                store.BlockConstraints.Add(new BlockConstraint
                {
                    BlockFrom = block,
                    BlockTo = fallThroughBlock,
                    HasPassedConstaint = evalResult != result || result == EvaluationResult.Unknown
                });
            }

            _logger.LogDebug(
                "Parsed block for conditions, current block constraint count: {Count}",
                store.BlockConstraints.Count);
        }
    }
}
