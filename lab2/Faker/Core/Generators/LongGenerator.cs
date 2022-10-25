using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class LongGenerator : IGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorTerm term)
        {
            return (long) 1;
           
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(long);
        }

    }
}
