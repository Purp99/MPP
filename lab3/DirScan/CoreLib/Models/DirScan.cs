using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Drawing;
using CoreLib.Models;
using System.Diagnostics;



namespace CoreLib
{
    public class DirScan
    {
        public const string FILEICON = @"C:\Users\sasha\OneDrive\Рабочий стол\BSUIR\Сourse 3\Sem 1\СПП\lab3\DirScan\Application\images\fileicon.png";
        public const string DIRICON = @"C:\Users\sasha\OneDrive\Рабочий стол\BSUIR\Сourse 3\Sem 1\СПП\lab3\DirScan\Application\images\diricon.png";
        private string _path;

        private DirComponent _root;

        public DirComponent Root
        {
            get { return _root; }
            set { _root = value; }
        }

        private ConcurrentQueue<Task> _folderQueue;
        private CancellationToken _cancellationToken;

        private int _threadCount;
        private SemaphoreSlim _semaphore;


        public DirScan(int threadCount)
        {
            _threadCount = threadCount;
            _folderQueue = new ConcurrentQueue<Task>();
            _semaphore = new SemaphoreSlim(_threadCount, _threadCount);
        }

        public DirComponent StartScanner(string path, CancellationToken token)
        {
            _cancellationToken = token;
            _path = path;
            _root = new DirComponent(new DirectoryInfo(_path).Name, _path, ComponentType.Directory, 0, 100);

            _semaphore.Wait(_cancellationToken);
            _folderQueue.Enqueue(Task.Run(() => ScanDirectory(_root), _cancellationToken));

            try
            {
                while (_folderQueue.TryDequeue(out var task) && !_cancellationToken.IsCancellationRequested)
                {
                    if (task.Status.Equals(TaskStatus.Created) && !task.IsCompleted)
                        task.Start();

                    task.Wait(_cancellationToken);
                }
            }
            catch (OperationCanceledException e)
            {
               
                _folderQueue = new ConcurrentQueue<Task>();
            }
            Trace.WriteLine(_semaphore.CurrentCount);

            _root.Size = CountSize(_root);
            CountRelativeSize(_root);

            return Root;
        }

        private void ScanDirectory(DirComponent dir)
        {
            Trace.WriteLine(_semaphore.CurrentCount);
            //
            var dirInfo = new DirectoryInfo(dir.FullName);

            try
            {
                foreach (var dirPath in dirInfo.EnumerateDirectories())//.Where(dir1 => dir1.LinkTarget == null)
                {
                    if (_cancellationToken.IsCancellationRequested)
                        return;
                    var child = new DirComponent(dirPath.Name, dirPath.FullName, ComponentType.Directory);
                    dir.ChildNodes.Add(child);
                    if (_semaphore.CurrentCount != 0)
                    {
                        _folderQueue.Enqueue(Task.Run(() =>
                        {
                            _semaphore.Wait();
                            ScanDirectory(child);
                        }, _cancellationToken));
                    }
                    else
                    {
                        _folderQueue.Enqueue(new Task(() =>
                        {
                            _semaphore.Wait();
                            ScanDirectory(child);
                        }, _cancellationToken));
                    }
                }

                foreach (var dirPath in dirInfo.EnumerateFiles())//.Where(file => file.LinkTarget == null)
                {
                    if (_cancellationToken.IsCancellationRequested)
                        return;
                    dir.ChildNodes.Add(new DirComponent(dirPath.Name, dirPath.FullName, ComponentType.File, dirPath.Length));
                    dir.Size += dirPath.Length;
                }
            }
            catch (Exception e)
            {

            }

            //Trace.WriteLine(_semaphore.CurrentCount);
            _semaphore.Release();
        }

        private long CountSize(DirComponent parentNode)
        {
            long size = 0;

            foreach (var childNode in parentNode.ChildNodes.ToList())
            {
                if (childNode.Type == ComponentType.Directory)
                {
                    var childDirSize = CountSize(childNode);
                    size += childDirSize;
                    childNode.Size = childDirSize;
                }
                else
                {
                    size += childNode.Size;
                }
            }

            return size;
        }

        private void CountRelativeSize(DirComponent parentNode)
        {
            foreach (var childNode in parentNode.ChildNodes.ToList())
            {
                childNode.Percent = childNode.Percent == 0 ?
                    (double)childNode.Size / (double)parentNode.Size * 100 : childNode.Percent;

                if (childNode.Type == ComponentType.Directory)
                {
                    CountRelativeSize(childNode);
                }
            }
        }
    }
}
