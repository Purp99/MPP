using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Core;
using Serialization;
using System.Runtime.Serialization.Json;


namespace Json
{
    public class JsonSer : ITraceResultSerializer
    {
        public string Format => "json";

        public void Serialize(TraceResultDto traceResult, string filename)
        {
            using (System.Xml.XmlDictionaryWriter jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(new FileStream(filename, FileMode.Create), Encoding.UTF8, ownsStream: true,
            indent: true, indentChars: "     "))
            {
                DataContractJsonSerializer _jsonFormatter = new DataContractJsonSerializer(typeof(TraceResultDto));
                _jsonFormatter.WriteObject(jsonWriter, traceResult);

            }
                
        }
    }
}
