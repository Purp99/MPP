using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    
    public class Tracer : ITracer
    {

        private StackTrace stackTrace;

        private readonly TraceResult _traceResult;

        public Tracer()
        {
            _traceResult = new TraceResult(new ConcurrentDictionary<int, ThreadResult>());
        }

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }

        public void StartTrace()
        {
            var thread = _traceResult.GetThreadResult(Thread.CurrentThread.ManagedThreadId);
            var stackTrace = new StackTrace();
           

            var path = stackTrace.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            path[0] = "";

            var methodName = stackTrace.GetFrames()[1].GetMethod().Name;
            var className = stackTrace.GetFrames()[1].GetMethod().ReflectedType.Name;

            thread.PushMethod(methodName, className, string.Join("", path));

        }

        public void StopTrace()
        {
            var threadTrace = _traceResult.GetThreadResult(Thread.CurrentThread.ManagedThreadId);

            var path = new StackTrace().ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            path[0] = "";

            threadTrace.PopMethod(string.Join("", path));
        }
    }
}
