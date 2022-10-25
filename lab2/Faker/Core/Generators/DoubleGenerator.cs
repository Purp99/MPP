using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class DoubleGenerator : IGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorTerm term)
        {
            return term.Random.NextDouble() * (double.MaxValue - double.MinValue) + double.MinValue;
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(double);
        }

    }
}
