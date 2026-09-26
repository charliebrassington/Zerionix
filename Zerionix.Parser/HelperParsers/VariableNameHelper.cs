using Microsoft.CodeAnalysis;
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

            return null;
        }
    }
}
