using Domain.Models;
using Microsoft.CodeAnalysis;
using Parser.Interfaces;
using Parser.Interfaces.BlockOperationParserInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.ParserExecutors
{
    public class BlockOperationParserExecutor : IBlockOperationParserExecutor
    {
        private readonly IEnumerable<IBlockOperationParser> _blockOperationParsers;

        public BlockOperationParserExecutor(IEnumerable<IBlockOperationParser> blockOperationParsers)
        {
            _blockOperationParsers = blockOperationParsers;
        }

        public void Execute(IOperation operation, MethodGlobalStore methodGlobalStore)
        {
            foreach (var parser in _blockOperationParsers.Where(p => p.CanHandle(operation)))
            {
                parser.ParseBlockOperation(operation, methodGlobalStore);
            }
        }
    }
}
