using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Domain.ParserResults;
using Zerionix.Parser.Interfaces.OperationParserInterfaces;

namespace Zerionix.Parser.OperationParsers
{
    public class SimpleAssignmentParser : IOperationParser<ISimpleAssignmentOperation, SimpleAssignmentResult>
    {
        private readonly ILogger<SimpleAssignmentParser> _logger;
        private readonly IOperationParser<IBinaryOperation, BinaryOperationMathResult> _binaryOperationMathParser;

        public SimpleAssignmentParser(
            ILogger<SimpleAssignmentParser> logger, 
            IOperationParser<IBinaryOperation, BinaryOperationMathResult> binaryOperationMathParser)
        {
            _logger = logger;
            _binaryOperationMathParser = binaryOperationMathParser;
        }

        public SimpleAssignmentResult ParseOperation(ISimpleAssignmentOperation operation, MethodGlobalStore methodGlobalStore)
        {
            var result = new SimpleAssignmentResult();
            result.LocalReferenceNameList = new List<string>();

            foreach (var childOp in operation.ChildOperations)
            {
                _logger.LogDebug("Parsing SimpleAssignment Child OP: {childOp.Kind}", childOp.Kind);

                if (childOp is ILocalReferenceOperation localReferenceOperation)
                {
                    result.LocalReferenceNameList.Add(localReferenceOperation.Local.Name);
                }

                if (childOp is IBinaryOperation binaryOperation)
                {
                    result.UnaryConstValue = _binaryOperationMathParser.ParseOperation(binaryOperation, methodGlobalStore).Value;
                }

                if (childOp is IUnaryOperation unaryOperation)
                {
                    result.UnaryConstValue = childOp.ConstantValue.Value;
                }

                if (childOp is IPropertyReferenceOperation propertyReferenceOperation)
                {
                    result.PropertyRefName = $"{propertyReferenceOperation.Property.ContainingType.Name}.{propertyReferenceOperation.Property.Name}";
                }

                if (childOp is ILiteralOperation literalOperation)
                {
                    result.UnaryConstValue = literalOperation.ConstantValue.Value;
                }
            }

            return result;
        }
    }
}
