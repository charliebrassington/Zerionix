using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Parser.Interfaces;

namespace Zerionix.Parser.BlockParsers
{
    public class ConditionParser : IBlockParser
    {
        private readonly ILogger<ConditionParser> _logger;

        public ConditionParser(ILogger<ConditionParser> logger)
        {
            _logger = logger;
        }

        public bool CanHandle(BasicBlock block) => block.ConditionalSuccessor != null;

        public void ParseBlock(BasicBlock block, MethodGlobalStore store)
        {
            store.BlockConstraints.Add(new BlockConstraint
            {
                BlockID = block.ConditionalSuccessor?.Destination?.Ordinal ?? 0,
                Condition = block.BranchValue,
                HasToPassExpression = true,
            });

            if (block.FallThroughSuccessor != null)
            {
                store.BlockConstraints.Add(new BlockConstraint
                {
                    BlockID = block.FallThroughSuccessor?.Destination?.Ordinal ?? 0,
                    Condition = block.BranchValue,
                    HasToPassExpression = false,
                });
            }

            _logger.LogDebug("Parsed block for conditions, current block constraint count: {store.BlockConstraints.Count}", store.BlockConstraints.Count);
        }
    }
}
