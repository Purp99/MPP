using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example
{
    public class Bar
    {
        private Core.ITracer _tracer;

        internal Bar(Core.ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(50);
            _tracer.StopTrace();
        }
    }
}
