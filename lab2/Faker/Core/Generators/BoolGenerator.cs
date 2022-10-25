using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class BoolGenerator : IGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorTerm term)
        {
            return term.Random.Next(2) == 1;
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(bool);
        }

    }
}
