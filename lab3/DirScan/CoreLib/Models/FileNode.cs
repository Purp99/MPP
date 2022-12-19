using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Models
{
    public class FileNode
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public double Percent { get; set; }

        public string FullName { get; set; }

        public ObservableCollection<DirComponent> ChildNodes
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public FileNode(string name, string fullName, int size = 0, double percent = 0)
        {
            Name = name;
            FullName = fullName;
            Size = size;
            Percent = percent;
        }
    }
}
