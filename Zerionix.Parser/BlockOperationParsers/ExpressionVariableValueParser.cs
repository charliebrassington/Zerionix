using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.GlobalStore.Interfaces;
using Zerionix.Parser.HelperParsers;
using Zerionix.Parser.Interfaces.BlockOperationParserInterfaces;
using Zerionix.Parser.Interfaces.HelperParserInterfaces;

namespace Zerionix.Parser.BlockOperationParsers
{
    public class ExpressionVariableValueParser : IBlockOperationParser
    {
        private readonly IMathOperationValueHelper _mathOperationValueHelper;
        private readonly IVariableNameHelper _variableNameHelper;
        private readonly IMethodGlobalStoreManager _methodGlobalStore;

        public ExpressionVariableValueParser(
            IMathOperationValueHelper mathOperationValueHelper, 
            IVariableNameHelper variableNameHelper, 
            IMethodGlobalStoreManager methodGlobalStore)
        {
            _mathOperationValueHelper = mathOperationValueHelper;
            _variableNameHelper = variableNameHelper;
            _methodGlobalStore = methodGlobalStore;
        }

        public bool CanHandle(IOperation operation) => operation is IExpressionStatementOperation;

        public void ParseBlockOperation(IOperation operation, MethodGlobalStore store)
        {
            if (operation is not IExpressionStatementOperation expressionStatementOperation 
                ||  expressionStatementOperation.Operation is not ICompoundAssignmentOperation compoundAssignOp)
                return;

            var targetName = _variableNameHelper.GetVariableName(compoundAssignOp.Target);
            var calculatedValue = _mathOperationValueHelper.GetOperationValue(compoundAssignOp, store);

            if (targetName != null && calculatedValue != null)
                _methodGlobalStore.AddNewSymbolicValue(store.SymbolicValueList, targetName, calculatedValue);
        }
    }
}
