using Domain.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Interfaces
{
    public interface IMethodParser
    {
        MethodGlobalStore ParseMethod(MethodDeclarationSyntax method, SemanticModel semanticModel);
    }
}
