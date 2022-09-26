using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract, Serializable]
    public class ThreadResultDto
    {
        [DataMember]
        public List<MethodResultDto> Methods;

        [DataMember]
        public long Time { get; set; }

        public ThreadResultDto(List<MethodResultDto> methods, long time)
        {
            Time = time;
            Methods = methods;
        }
    }
}
