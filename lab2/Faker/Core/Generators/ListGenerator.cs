using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class ListGenerator : IGenerator
    {
        private const int MinListLength = 1;
        private const int MaxListLength = 2;
        public object Generate(Type typeToGenerate, GeneratorTerm term)
        {
            System.Collections.IList list = (System.Collections.IList)Activator.CreateInstance(typeToGenerate);
            var length = term.Random.Next(MinListLength, MaxListLength);

            for (int i = 0; i < length; i++)
            {
                list.Add(term.Faker.Create(typeToGenerate.GetGenericArguments().First()));
            }

            return list;
        }
        public bool CanGenerate(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

    }
}
