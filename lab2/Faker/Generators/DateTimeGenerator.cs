using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public class DateTimeGenerator : IGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorTerm term)
        {
            int range = (DateTime.Now - DateTime.MinValue).Days;

            return DateTime.MinValue
                .AddDays(term.Random.Next(range))
                .AddHours(term.Random.Next(24))
                .AddMinutes(term.Random.Next(60));
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }

    }
}
