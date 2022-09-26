using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using YamlDotNet.Serialization;

namespace Core
{
    [DataContract, Serializable]
    public class MethodResult
    {

        [DataMember]
        public long Time { get; set; }
        
        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string MethodName { get; set; }

        private readonly string _path;

        [XmlIgnore, NonSerialized, YamlIgnore]
        private readonly Stopwatch _stopwatch = new Stopwatch();

        [DataMember]
        public List<MethodResult> ChildMethods { get; set; }

        public MethodResult(string methodName, string className, string path)
        {
            this._path = path;
            this.MethodName = methodName;
            this.ChildMethods = new List<MethodResult>();
            this.ClassName = className;
            _stopwatch.Start();
        }
        public MethodResult() { }

        public long GetTime()
        {
            return this.Time;
        }

        public void StopTime()
        {
            _stopwatch.Stop();
            this.Time = _stopwatch.ElapsedMilliseconds;
        }

        public string GetPath()
        {
            return _path;
        }

        public void SetChilds(List<MethodResult> childs)
        {
            ChildMethods = childs;
        }
    }
}
