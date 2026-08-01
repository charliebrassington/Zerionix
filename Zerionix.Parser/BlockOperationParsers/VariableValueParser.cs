using Domain.Models;
using Domain.ParserResults;
using GlobalStore.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Parser.Interfaces.BlockOperationParserInterfaces;
using Parser.Interfaces.OperationParserInterfaces;

namespace Parser.BlockOperationParsers
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

            var simpleAssignmentResult = _simpleAssignmentOperationParser.ParseOperation(simpleOp);

            if (simpleAssignmentResult.LocalReferenceNameList?.Count == 2)
            {
                _methodGlobalStore.AddSymbolicValue(methodGlobalStore.SymbolicValueList, simpleAssignmentResult.LocalReferenceNameList[1], simpleAssignmentResult.LocalReferenceNameList[0]);
            }

            var localReferenceName = simpleAssignmentResult.LocalReferenceNameList?.FirstOrDefault();

            if (simpleAssignmentResult.UnaryConstValue != null && localReferenceName != null)
            {
                methodGlobalStore.SymbolicValueList.Add(new SymbolicValue { VariableReference = localReferenceName, Constant = simpleAssignmentResult.UnaryConstValue });
            }

            if (simpleAssignmentResult.PropertyRefName != null && localReferenceName != null)
            {
                _methodGlobalStore.AddSymbolicValue(methodGlobalStore.SymbolicValueList, localReferenceName, simpleAssignmentResult.PropertyRefName);
            }
        }
    }
}
        