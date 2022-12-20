using NUnit.Framework;
using System.Threading.Tasks;
using System.Threading;
using System;
using CoreLib.Models;
using CoreLib.ViewModels;
using CoreLib;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        private CancellationTokenSource _cancelTokenSource;

        private DirScan _scanner;

        [SetUp]
        public void Setup()
        {
            _scanner = new DirScan(10);
            _cancelTokenSource = new CancellationTokenSource();
        }

        [Test]
        public void Test_RootSize()
        {
            string path = @"C:\\Users\\sasha\\OneDrive\\Рабочий стол\\BSUIR\\Сourse 3";
            long size = 0;

            DirComponent root = _scanner.StartScanner(path, _cancelTokenSource.Token);
            foreach (var dir in root.ChildNodes.Where(child => child.Type == ComponentType.Directory))
            {
                size += dir.Size;
            }

            Assert.That(size, Is.EqualTo(root.Size));
        }

        [Test]
        public void Test_SymLink()
        {
            string path = @"C:\\Users\\sasha\\OneDrive\\Рабочий стол\\BSUIR\\Сourse 3\\Projects";

            DirComponent root = _scanner.StartScanner(path, _cancelTokenSource.Token);

            Assert.That(root.Size, Is.EqualTo(171181477));
        }

        [Test]
        public void Test_WindowsFolderFileCount()
        {
            string path = @"C:\\Users\\sasha\\OneDrive\\Рабочий стол\\BSUIR\\Сourse 3\\Sem 1";

            DirComponent root = _scanner.StartScanner(path, _cancelTokenSource.Token);
            Assert.That(root.ChildNodes.Where(node => node.Type == ComponentType.File).Count, Is.EqualTo(1));
        }

        [Test]
        public void Test_Cancellation()
        {
            string path = @"C:\\Users\\sasha\\OneDrive\\Рабочий стол\\BSUIR\\Сourse 3\\Sem 1";

            var task = Task<DirComponent>.Factory.StartNew(() =>
                (DirComponent)_scanner.StartScanner(path, _cancelTokenSource.Token));

            for (int i = 0; i < 15000; i++)
            {
                Console.WriteLine(i);
            }
            _cancelTokenSource.Cancel();

            var result = task.Result;
            Assert.That(result.Size, Is.LessThan(2638340096));
        }
    }
}
