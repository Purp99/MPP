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
            return term.Random.NextInt64(long.MinValue, long.MaxValue);
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(long);
        }

    }
}
