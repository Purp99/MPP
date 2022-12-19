using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace TestGenerator
{
    public class Parser
    {
        public static CompilationUnitSyntax Parse(string srcText)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(srcText);
            var tree = syntaxTree.GetCompilationUnitRoot();
            var diagnostics = tree.GetDiagnostics();

            if (0 != diagnostics.Count())
            {
                var acc = "";
                foreach (var d in diagnostics)
                {
                    acc += d.ToString() + "\n";
                }
                throw new InvalidSyntaxException(acc);
            }

            return tree;
        }

        public static IEnumerable<MethodDeclarationSyntax> GetPublicMethodsDeclarations(ClassDeclarationSyntax classDeclaration)
        {
            return from methodDeclaration in classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>()
                   where methodDeclaration.Modifiers.Any(methodModifier => methodModifier.Text == "public")
                   select methodDeclaration;
        }

        public static IEnumerable<ClassDeclarationSyntax> GetClassDeclarations(CompilationUnitSyntax baseNode)
        {
           
            return from classDeclaration in baseNode.DescendantNodes().OfType<ClassDeclarationSyntax>()
                   select classDeclaration;
        }
    }
}
