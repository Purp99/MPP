using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Models
{
    public class DirComponent
    {
        public string Name { get; set; }

        public long Size { get; set; }

        public double Percent { get; set; }

        public string FullName { get; set; }

        public ObservableCollection<DirComponent> ChildNodes { get; set; }

        public ComponentType Type { get; set; }

        public DirComponent(string name, string fullName, ComponentType type, long size = 0, double percent = 0)
        {
            Name = name;
            FullName = fullName;
            Size = size;
            Percent = percent;
            Type = type;
            ChildNodes = new ObservableCollection<DirComponent>();
        }
    }
}
