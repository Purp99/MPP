using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Core;
using Serialization;
namespace Yaml
{
    public class YamlSer : ITraceResultSerializer
    {
        public string Format => "yaml";
        public async void Serialize(TraceResultDto traceResult, string filename)
        {

            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var str = serializer.Serialize(traceResult);


            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                await writer.WriteLineAsync(str);
            }

        }
    }
}
