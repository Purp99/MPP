using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class Foo
    {
        private Bar _bar;
        private Core.ITracer _tracer;

        internal Foo(Core.ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            //
            _bar.InnerMethod();
            //
            _tracer.StopTrace();
        }
    }

    
}
