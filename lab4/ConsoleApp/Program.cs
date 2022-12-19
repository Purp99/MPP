using System;
using System.Collections.Generic;
using TestGenerator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks.Dataflow;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    internal class Program
    {
        private const string srcDir = "./../../../SourceClass/";
        private const string outputDir = "./../../../../Tests/OutputTests/";
        static void Main(string[] args)
        {
            var parser = new Parser();
            var srcFileNames = Directory.GetFiles(srcDir, "*.cs").Select(Path.GetFileName);

            var blockOptions = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 8 };

            var loadSrcBlock = new TransformBlock<string, string>(async inputFilePath =>
            {
                return await File.ReadAllTextAsync(inputFilePath);
            }, blockOptions);

            var generateTestsBlock = new TransformBlock<string, IEnumerable<string>>(async classSrcCode =>
            {
                return await TestGenerator.TestGenerator.GetTestCode(classSrcCode);
            }, blockOptions);

            var writeTestsIntoFile = new ActionBlock<IEnumerable<string>>(async testClassSrcCode =>
            {
                foreach (var test in testClassSrcCode)
                {
                    var root = await CSharpSyntaxTree.ParseText(test).GetRootAsync();
                    var classDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>().First();
                    await File.WriteAllTextAsync(string.Format("{0}/{1}.cs", outputDir, classDeclaration.Identifier.Text), test);
                }
            }, blockOptions);

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            loadSrcBlock.LinkTo(generateTestsBlock, linkOptions);
            generateTestsBlock.LinkTo(writeTestsIntoFile, linkOptions);

            foreach (var fileName in srcFileNames)
            {
                loadSrcBlock.SendAsync(string.Format("{0}/{1}", srcDir, fileName));
            }
            loadSrcBlock.Complete();

            try
            {
                writeTestsIntoFile.Completion.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    if (e is FileNotFoundException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else if (e is InvalidSyntaxException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}
