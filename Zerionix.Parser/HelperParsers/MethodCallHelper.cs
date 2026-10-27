using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Parser.Interfaces.HelperParserInterfaces;

namespace Zerionix.Parser.HelperParsers
{
    public class MethodCallHelper : IMethodCallHelper
    {
        private readonly IConstValueHelper _constValueHelper;

        public MethodCallHelper(IConstValueHelper constValueHelper)
        {
            _constValueHelper = constValueHelper;
        }

        public MethodCall? GetMethodCall(IOperation operation, MethodGlobalStore methodGlobalStore)
        {
            if (operation is not IInvocationOperation invocationOperation)
                return null;

            return new MethodCall() 
            { 
                MethodSymbol = invocationOperation.TargetMethod,
                ArgumentList = invocationOperation.Arguments.Select(arg => new MethodCallArgument() 
                {
                    ParameterName = arg.Parameter?.Name,
                    ParamterValue = _constValueHelper.GetConstValue(arg.Value, methodGlobalStore)

                }).ToList()
            };
        }
    }
}
