using Domain.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Parser.BlockOperationParsers;
using Parser.Interfaces;
using Parser.Interfaces.BlockOperationParserInterfaces;
using Parser.ParserExecutors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    public class MethodParser : IMethodParser
    {
        private readonly IBlockOperationParserExecutor _blockOperationParserExecutor;

        public MethodParser(IBlockOperationParserExecutor blockOperationParserExecutor)
        {
            _blockOperationParserExecutor = blockOperationParserExecutor;
        }

        public MethodGlobalStore ParseMethod(MethodDeclarationSyntax method, SemanticModel semanticModel)
        {
            var methodStore = new MethodGlobalStore();

            var cfg = ControlFlowGraph.Create(method, semanticModel);

            if (cfg == null)
                return methodStore;

            foreach (var block in cfg.Blocks)
            {
                foreach (var op in block.Operations)
                {
                    Console.WriteLine($"Parsing -> {method.Identifier}, Block: {block.Ordinal}, Op: {op.Kind}, Code: {op.Syntax}");

                    _blockOperationParserExecutor.Execute(op, methodStore);
                }
            }

            return methodStore;
        }
    }
}
