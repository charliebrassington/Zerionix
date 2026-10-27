using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Domain.Models
{
    public class MethodSemantic
    {
        public SemanticModel? SemanticTreeModel {  get; set; }
        public required List<MethodDeclarationSyntax> MethodDeclarationList { get; set; }
    }
}
