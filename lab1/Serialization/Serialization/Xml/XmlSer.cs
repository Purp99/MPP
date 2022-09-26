using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Core;
using Serialization;
using System.Xml;
using System.Runtime.Serialization;

namespace Xml
{
    public class XmlSer : ITraceResultSerializer
    {
        public string Format => "xml";
        public void Serialize(TraceResultDto traceResult, string filename)
        {
            using (var xmlWriter = XmlWriter.Create(filename, new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "     "
            }))
            {
                DataContractSerializer _xmlConverter = new DataContractSerializer(typeof(TraceResultDto)); 
                _xmlConverter.WriteObject(xmlWriter, traceResult);
            }
        }
    }
}
