using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Parser.Interfaces
{
    public interface IMethodParserManager
    {
        List<MethodGlobalStore> HandleMethodParsing(List<MethodSemantic> methodSemanticList);
    }
}
