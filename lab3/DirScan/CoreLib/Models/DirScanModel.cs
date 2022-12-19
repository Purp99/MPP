using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Models
{
    class DirScanModel : INotifyPropertyChanged
    {

        private DirScan _scanner;

        public DirScanModel(DirScan scanner)
        {
            _scanner = scanner;
        }

        public DirComponent Root
        {
            get { return _scanner.Root; }
            set
            {
                _scanner.Root = value;
                OnPropertyChanged(nameof(Root));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
