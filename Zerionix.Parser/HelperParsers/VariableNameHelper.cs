using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Zerionix.Parser.Interfaces.HelperParserInterfaces;

namespace Zerionix.Parser.HelperParsers
{
    public class VariableNameHelper : IVariableNameHelper
    {
        public string? GetVariableName(IOperation operation)
        {
            if (operation is ILocalReferenceOperation localReferenceOperation)
            {
                return localReferenceOperation.Local.Name;
            }

            if (operation is IPropertyReferenceOperation propertyReferenceOperation)
            {
                return $"{propertyReferenceOperation.Property.ContainingType.Name}.{propertyReferenceOperation.Property.Name}";
            }

            if (operation is IParameterReferenceOperation parameterReferenceOperation)
                return parameterReferenceOperation.Parameter.Name;

            if (operation is IFlowCaptureReferenceOperation)
                return $"{operation.Type?.Name}.";

            return null;
        }
    }
}
