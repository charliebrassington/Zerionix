using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Domain.ParserResults;
using Zerionix.Parser.Interfaces.HelperParserInterfaces;
using Zerionix.Parser.Interfaces.OperationParserInterfaces;

namespace Zerionix.Parser.OperationParsers
{
    public class SimpleAssignmentParser : IOperationParser<ISimpleAssignmentOperation, SimpleAssignmentResult>
    {
        private readonly ILogger<SimpleAssignmentParser> _logger;
        private readonly IConstValueHelper _constValueHelper;
        private readonly IVariableNameHelper _variableNameHelper;

        public SimpleAssignmentParser(ILogger<SimpleAssignmentParser> logger, IConstValueHelper constValueHelper, IVariableNameHelper variableNameHelper)
        {
            _logger = logger;
            _constValueHelper = constValueHelper;
            _variableNameHelper = variableNameHelper;
        }

        public SimpleAssignmentResult ParseOperation(ISimpleAssignmentOperation operation, MethodGlobalStore methodGlobalStore)
        {
            var result = new SimpleAssignmentResult();

            _logger.LogDebug("Parsing SimpleAssignment Target: {operation.Target.Kind}, Value: {operation.Value.Kind}", operation.Target.Kind, operation.Value.Kind);

            result.TargetName = _variableNameHelper.GetVariableName(operation.Target);
            result.VariableName = _variableNameHelper.GetVariableName(operation.Value);
            result.VariableValue = _constValueHelper.GetConstValue(operation.Value, methodGlobalStore);

            return result;
        }
    }
}
