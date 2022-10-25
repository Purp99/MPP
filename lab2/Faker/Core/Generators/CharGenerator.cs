using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class CharGenerator : IGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorTerm term)
        {
            return (char)term.Random.Next(1, 256);
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(char);
        }

    }
}
