using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract, Serializable]
    public class MethodResultDto
    {
        [DataMember(Name = "class", Order = 0)]
        public string ClassName { get; set; }

        [DataMember(Name = "method", Order = 1)]
        public string MethodName { get; set; }

        [DataMember(Name = "time", Order = 2)]
        public long Time { get; set; }

        [DataMember(Name = "childs", Order = 3)]
        public List<MethodResultDto> ChildMethods { get; set; }

        public MethodResultDto(string className, string methodName, long time, List<MethodResultDto> childs)
        {
            ClassName = className;
            MethodName = methodName;
            Time = time;
            ChildMethods = childs;
        }
    }
}
