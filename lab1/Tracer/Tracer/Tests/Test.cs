using NUnit.Framework;
using Core;
using System.Threading;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        private ITracer _tracer;

        [SetUp]
        public void Setup()
        {
            _tracer = new Tracer();
        }

        private void M1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
        private void M2()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            M1();
            _tracer.StopTrace();
        }

        [Test]
        public void Test1()
        {
            M1();
            Assert.That(_tracer.GetTraceResult().ThreadsResult.Count, Is.EqualTo(1));
            Assert.GreaterOrEqual(_tracer.GetTraceResult().ThreadsResult.First().Value.MethodsResult[0].Time, 100);
        }
    }
}