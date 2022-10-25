using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class IntGenerator : IGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorTerm term)
        {
            return term.Random.Next(int.MinValue, int.MaxValue);
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }

    }
}
