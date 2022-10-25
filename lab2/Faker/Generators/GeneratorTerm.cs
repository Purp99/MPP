using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class GeneratorTerm
    {
        public Random Random { get; }
        public Core.IFaker Faker { get; }

        public GeneratorTerm(Random random, Core.IFaker faker)
        {
            Random = random;
            Faker = faker;
        }


    }
}
