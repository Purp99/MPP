using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestGenerator;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ParserTests
    {
        internal static CompilationUnitSyntax inputTree = null;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var code = @"
            namespace TestingNamespace {
                public class FirstClass
                {
                    public void Generate(int type) { }
                    public void GetGeneratingType() { }
                }
    
                public class SecondClass
                {
                    private void Hash(string password) { }
                    public void Calculate() { }
                }

                public class ThirdClass
                {
                    private static void Decipher(string password) { }
                    private void Encipher(string coded) { }
                }
            }";

            inputTree = Parser.Parse(code);
        }


        [TestMethod]
        public void TestMethod01()
        {
            TestParsedClassesDeclarations();
            TestParsedMethodsDeclarations();
        }

        public void TestParsedClassesDeclarations()
        {
            List<ClassDeclarationSyntax> classDeclarations = (List<ClassDeclarationSyntax>) Parser.GetClassDeclarations(inputTree);
            
            Assert.AreEqual(3, classDeclarations.Count);
            Assert.AreEqual("FirstClass", classDeclarations[0].Identifier.ValueText);
            Assert.AreEqual("SecondClass", classDeclarations[1].Identifier.ValueText);
            Assert.AreEqual("ThirdClass", classDeclarations[2].Identifier.ValueText);
        }

        public void TestParsedMethodsDeclarations()
        {
            List<ClassDeclarationSyntax> classDeclarations = (List<ClassDeclarationSyntax>) Parser.GetClassDeclarations(inputTree);
            List<MethodDeclarationSyntax> methodsDeclarations = (List<MethodDeclarationSyntax>)Parser.GetPublicMethodsDeclarations(classDeclarations[0]);

            Assert.AreEqual(2, methodsDeclarations.Count);
            Assert.AreEqual("Generate", methodsDeclarations[0].Identifier.ValueText);
            Assert.AreEqual("GetGeneratingType", methodsDeclarations[1].Identifier.ValueText);

            methodsDeclarations = (List<MethodDeclarationSyntax>)Parser.GetPublicMethodsDeclarations(classDeclarations[1]);

            Assert.AreEqual(1, methodsDeclarations.Count);
            Assert.AreEqual("Calculate", methodsDeclarations[0].Identifier.ValueText);

            methodsDeclarations = (List<MethodDeclarationSyntax>)Parser.GetPublicMethodsDeclarations(classDeclarations[2]);

            Assert.AreEqual(0, methodsDeclarations.Count);
        }
    }

    [TestClass]
    public class GeneratorTests
    {
        internal static List<CompilationUnitSyntax> outputTrees = new List<CompilationUnitSyntax>();

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            var generatedCode = TestGenerator.TestGenerator.GenerateTestCode(ParserTests.inputTree);
            foreach (var code in generatedCode)
            {
                outputTrees.Add(Parser.Parse(code));
            }
        }

        [TestMethod]
        public void TestMethod06()
        {
            TestGeneratedClassesDeclarations();
            TestGeneratedMethodsDeclarations();
        }

        public void TestGeneratedClassesDeclarations()
        {
            foreach (var tree in outputTrees)
            {
                IEnumerable<ClassDeclarationSyntax> r = Parser.GetClassDeclarations(tree);
                
                List<ClassDeclarationSyntax> classDeclarations = (List<ClassDeclarationSyntax>) Parser.GetClassDeclarations(tree);
                Assert.AreEqual(1, classDeclarations.Count);
            }
        }

        public void TestGeneratedMethodsDeclarations()
        {
            List<ClassDeclarationSyntax> classDeclarations = (List<ClassDeclarationSyntax>)Parser.GetClassDeclarations(outputTrees[0]);
            List<MethodDeclarationSyntax> methodsDeclarations = (List<MethodDeclarationSyntax>)Parser.GetPublicMethodsDeclarations(classDeclarations[0]);

            
            Assert.AreEqual("Test_FirstClass_Generate_Method", methodsDeclarations[0].Identifier.ValueText);
            Assert.AreEqual("Test_FirstClass_GetGeneratingType_Method", methodsDeclarations[1].Identifier.ValueText);
        }
    }
}
