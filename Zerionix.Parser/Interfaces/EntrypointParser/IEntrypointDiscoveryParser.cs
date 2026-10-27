using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Parser.Interfaces.EntrypointParser
{
    public interface IEntrypointDiscoveryParser
    {
        List<MethodDeclarationSyntax> GetEntryPoints(List<MethodSemantic> methodSemantics);
    }
}
