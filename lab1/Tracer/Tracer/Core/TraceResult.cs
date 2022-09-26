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

        public TraceResult(ConcurrentDictionary<int, ThreadResult> threadResults)
        {
            ThreadsResult = threadResults;
        }

        public TraceResult() { }

        public ConcurrentDictionary<int, ThreadResult> GetThreadResults()
        {
            return ThreadsResult;
        }

        public ThreadResult GetThreadResult(int id)
        {
            return ThreadsResult.GetOrAdd(id, new ThreadResult(id));
        }
    }
}
