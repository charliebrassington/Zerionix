using Zerionix.Domain.Models;
using Zerionix.Domain.ParserResults;
using Zerionix.GlobalStore.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Zerionix.Parser.Interfaces.BlockOperationParserInterfaces;
using Zerionix.Parser.Interfaces.OperationParserInterfaces;

namespace Zerionix.Parser.BlockOperationParsers
{
    public class VariableValueParser : IBlockOperationParser
    {
        private readonly IOperationParser<ISimpleAssignmentOperation, SimpleAssignmentResult> _simpleAssignmentOperationParser;
        private readonly IMethodGlobalStoreManager _methodGlobalStore;

        public VariableValueParser(
            IOperationParser<ISimpleAssignmentOperation, SimpleAssignmentResult> simpleAssignmentOperationParser,
            IMethodGlobalStoreManager methodGlobalStore)
        {
            _simpleAssignmentOperationParser = simpleAssignmentOperationParser;
            _methodGlobalStore = methodGlobalStore;
        }

        public bool CanHandle(IOperation operation) => operation is ISimpleAssignmentOperation;

        public void ParseBlockOperation(IOperation operation, MethodGlobalStore methodGlobalStore)
        {
            if (operation is not ISimpleAssignmentOperation simpleOp) 
                return;

            var simpleAssignmentResult = _simpleAssignmentOperationParser.ParseOperation(simpleOp, methodGlobalStore);

            if (simpleAssignmentResult.TargetName != null && simpleAssignmentResult.VariableName != null)
            {
                _methodGlobalStore.AddSymbolicValue(methodGlobalStore.SymbolicValueList, simpleAssignmentResult.VariableName, simpleAssignmentResult.TargetName);
            }

            if (simpleAssignmentResult.TargetName != null && simpleAssignmentResult.VariableValue != null)
            {
                _methodGlobalStore.AddNewSymbolicValue(methodGlobalStore.SymbolicValueList, simpleAssignmentResult.TargetName, simpleAssignmentResult.VariableValue);
            }
        }
    }
}
        