using Zerionix.Domain.ParserResults;
using Microsoft.CodeAnalysis.Operations;
using Zerionix.Parser.Interfaces.OperationParserInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Zerionix.Parser.OperationParsers
{
    public class SimpleAssignmentParser : IOperationParser<ISimpleAssignmentOperation, SimpleAssignmentResult>
    {
        private readonly ILogger<SimpleAssignmentParser> _logger;

        public SimpleAssignmentParser(ILogger<SimpleAssignmentParser> logger)
        {
            _logger = logger;
        }

        public SimpleAssignmentResult ParseOperation(ISimpleAssignmentOperation operation)
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

                if (childOp is IUnaryOperation unaryOperation)
                {
                    result.UnaryConstValue = childOp.ConstantValue.Value;
                }

                if (childOp is IPropertyReferenceOperation propertyReferenceOperation)
                {
                    result.PropertyRefName = $"{propertyReferenceOperation.Property.ContainingType.Name}.{propertyReferenceOperation.Property.Name}";
                }
            }

            return result;
        }
    }
}
