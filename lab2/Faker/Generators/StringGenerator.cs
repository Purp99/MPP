using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class StringGenerator : IGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorTerm term)
        {
            int length = term.Random.Next(1, 20);
            string str = String.Empty;

            for (int i = 0; i < length; i++)
            {
                str += (char)term.Random.Next(1, 256);
            }

            return str;
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

    }
}
