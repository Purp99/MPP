using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [KnownType(typeof(TraceResultDto))]
    [DataContract, Serializable]
    public class TraceResultDto
    {
        [DataMember]
        public ConcurrentDictionary<int, ThreadResultDto> Threads;

        public TraceResultDto(ConcurrentDictionary<int, ThreadResultDto> threads)
        {
            Threads = threads;
        }
        public TraceResultDto(){}
    }
}
