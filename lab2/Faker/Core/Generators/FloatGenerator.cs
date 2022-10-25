using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class FloatGenerator : IGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorTerm term)
        {
            return (float)term.Random.NextDouble();
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(float);
        }

    }
}
