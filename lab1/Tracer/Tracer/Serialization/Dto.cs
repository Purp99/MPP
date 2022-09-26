using Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public static class Dto
    {

        public static TraceResultDto CreateTraceResultDto(TraceResult traceResult)
        {
            return new TraceResultDto(AddThreadTraceResulstDto(traceResult.ThreadsResult));
        }

        private static ConcurrentDictionary<int, ThreadResultDto> AddThreadTraceResulstDto(
            ConcurrentDictionary<int, ThreadResult> threadResults)
        {
            var threads = new ConcurrentDictionary<int, ThreadResultDto>();
            foreach (var threadKey in threadResults.Keys)
            {
                threads.GetOrAdd(threadKey, new ThreadResultDto(AddMethodTraceResultDto(threadResults[threadKey].MethodsResult), threadResults[threadKey].Time));
            }

            return threads;
        }

        private static List<MethodResultDto> AddMethodTraceResultDto(List<MethodResult> methodList)
        {
            var methodResultDtoList = new List<MethodResultDto>();
            foreach (var methodResult in methodList)
            {
                var nestedMethodResultDtoList = new List<MethodResultDto>();
                if (methodResult.ChildMethods.Count != 0)
                {
                    nestedMethodResultDtoList = AddMethodTraceResultDto(methodResult.ChildMethods);
                }

                methodResultDtoList.Add(new MethodResultDto(
                        methodResult.MethodName,
                        methodResult.ClassName,
                        methodResult.Time,
                        nestedMethodResultDtoList
                    )
                );
            }

            return methodResultDtoList;
        }
    }
}
