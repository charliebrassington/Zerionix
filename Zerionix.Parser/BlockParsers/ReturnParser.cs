using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Parser.Interfaces;
using Zerionix.Parser.Interfaces.ConditionParserInterfaces;
using Zerionix.Parser.Interfaces.HelperParserInterfaces;

namespace Zerionix.Parser.BlockParsers
{
    public class ReturnParser : IBlockParser
    {
        private readonly ILogger<ReturnParser> _logger;
        private readonly IConstValueHelper _constValueHelper;
        private readonly IVariableNameHelper _variableNameHelper;

        public ReturnParser(ILogger<ReturnParser> logger, IConstValueHelper constValueHelper, IVariableNameHelper variableNameHelper)
        {
            _logger = logger;
            _constValueHelper = constValueHelper;
            _variableNameHelper = variableNameHelper;
        }

        public bool CanHandle(BasicBlock block) => block.BranchValue?.Syntax?.Parent is ReturnStatementSyntax;

        public void ParseBlock(BasicBlock block, MethodGlobalStore store)
        {
            if (block.BranchValue == null)
                return;

            store.ReturnValues.Value = _constValueHelper.GetConstValue(block.BranchValue, store);
            store.ReturnValues.ObjectName = _variableNameHelper.GetVariableName(block.BranchValue);
            Console.WriteLine("d");
        }
    }
}
