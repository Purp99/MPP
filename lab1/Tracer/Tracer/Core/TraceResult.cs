using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [DataContract, Serializable]
    public class TraceResult
    {
        [DataMember]
        public ConcurrentDictionary<int, ThreadResult> ThreadsResult { get; }
        //public IReadOnlyDictionary<int, ThreadResult> Threads { get; }

        public TraceResult(ConcurrentDictionary<int, ThreadResult> threadsResult)
        {
            ThreadsResult = threadsResult;
        }

        public TraceResult() { }

        

        public ThreadResult GetThreadResult(int id)
        {
            return ThreadsResult.GetOrAdd(id, new ThreadResult(id));
        }
    }
}
