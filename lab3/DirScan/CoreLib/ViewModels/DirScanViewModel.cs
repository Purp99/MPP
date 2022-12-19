using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Input;
using System.Threading.Tasks;
using CoreLib.Models;
using System.Diagnostics;

namespace CoreLib.ViewModels
{
    public class DirScanViewModel : INotifyPropertyChanged
    {
        private CancellationTokenSource _cancelTokenSource;

        private DirScan _scanner;

        private DirComponent _root;

        public DirComponent Root
        {
            get { return _root; }
            set
            {
                _root = value;
                OnPropertyChanged("Root");
            }
        }

        private void StartScanner()
        {
            var fbd = new FolderBrowserForWPF.Dialog();
            string path = String.Empty;
            if (!fbd.ShowDialog().GetValueOrDefault())
                return;

            _cancelTokenSource = new CancellationTokenSource();
            var token = _cancelTokenSource.Token;
            Trace.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Trace.WriteLine("________________________________");

            Task.Run(() =>
            {
                Trace.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Trace.WriteLine("________________________________");
                var root = new DirComponent("", "", ComponentType.Directory);
                root.ChildNodes = new ObservableCollection<DirComponent> { _scanner.StartScanner(fbd.FileName, token) };
                Root = root;
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private BaseCommand _startScannerCommand;
        public BaseCommand StartScannerCommand
        {
            get { return _startScannerCommand ?? new BaseCommand(obj => StartScanner()); }
        }

        private BaseCommand _cancelScannerCommand;
        public BaseCommand CancelScannerCommand
        {
            get { return _cancelScannerCommand ?? new BaseCommand(obj => CancelScanner()); }
        }

        public void CancelScanner()
        {
            if (_cancelTokenSource != null && !_cancelTokenSource.IsCancellationRequested)
            {
                _cancelTokenSource.Cancel();
                _cancelTokenSource.Dispose();
            }
        }

        public DirScanViewModel()
        {
            var threadCount = 3;
            _scanner = new DirScan(threadCount);
        }
    }
}
