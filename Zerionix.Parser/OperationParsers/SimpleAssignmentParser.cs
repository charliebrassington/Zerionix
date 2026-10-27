using Microsoft.CodeAnalysis;
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
        private readonly IMethodCallHelper _methodCallHelper;

        public SimpleAssignmentParser(ILogger<SimpleAssignmentParser> logger, IConstValueHelper constValueHelper, IVariableNameHelper variableNameHelper, IMethodCallHelper methodCallHelper)
        {
            _logger = logger;
            _constValueHelper = constValueHelper;
            _variableNameHelper = variableNameHelper;
            _methodCallHelper = methodCallHelper;
        }

        public SimpleAssignmentResult ParseOperation(ISimpleAssignmentOperation operation, MethodGlobalStore methodGlobalStore)
        {
            _logger.LogDebug("Parsing SimpleAssignment Target: {operation.Target.Kind}, Value: {operation.Value.Kind}", operation.Target.Kind, operation.Value.Kind);

            var result = new SimpleAssignmentResult
            {
                TargetName = _variableNameHelper.GetVariableName(operation.Target),
                VariableName = _variableNameHelper.GetVariableName(operation.Value),
                VariableValue = _constValueHelper.GetConstValue(operation.Value, methodGlobalStore)
            };

            var methodCall = _methodCallHelper.GetMethodCall(operation.Value, methodGlobalStore);

            if (methodCall == null)
                return result;

            if (SymbolEqualityComparer.Default.Equals(methodCall.MethodSymbol, methodGlobalStore.CurrentMethodCall?.MethodSymbol))
            {

                if (methodGlobalStore.CurrentMethodCall.Return.Value != null)
                    result.VariableValue = methodGlobalStore.CurrentMethodCall.Return.Value;

                if (methodGlobalStore.CurrentMethodCall.Return.ObjectName != null)
                    result.VariableName = Convert.ToString(methodGlobalStore.CurrentMethodCall.Return.ObjectName);

                result.MethodCallRequired = null;
            }
            else
            {
                result.MethodCallRequired = methodCall;
            }

            return result;
        }
    }
}
