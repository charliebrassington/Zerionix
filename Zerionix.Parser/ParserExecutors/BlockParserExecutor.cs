using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Parser.Interfaces;
using Zerionix.Parser.Interfaces.BlockOperationParserInterfaces;

namespace Zerionix.Parser.ParserExecutors
{
    public class BlockParserExecutor : IBlockParserExecutor
    {
        private readonly IEnumerable<IBlockParser> _blockParsers;

        public BlockParserExecutor(IEnumerable<IBlockParser> blockParsers)
        {
            _blockParsers = blockParsers;
        }

        public void Execute(BasicBlock block, MethodGlobalStore methodGlobalStore)
        {
            foreach (var parser in _blockParsers.Where(p => p.CanHandle(block)))
            {
                parser.ParseBlock(block, methodGlobalStore);
            }
        }
    }
}
