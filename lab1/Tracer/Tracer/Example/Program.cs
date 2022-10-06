using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Example
{
    class Program
    {
        private static Tracer _tracer;

        static void serializeUsingPlugins(List<Type> plugins, Serialization.TraceResultDto traceResult)
        {
            foreach (Type type in plugins)
            {
                var method = type?.GetMethod("Serialize");
                var obj = Activator.CreateInstance(type!);
                var format = type?.GetProperty("Format")?.GetValue(obj, null);
                method?.Invoke(obj, new object?[] {
                    traceResult,
                    "./test/result." + format
                });
            }
        }

        public static void M1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        static void Main(string[] args)
        {
            PluginLoader.PluginLoader pluginLoader = new PluginLoader.PluginLoader();
            pluginLoader.LoadPlugins();

            _tracer = new Tracer();
            Foo foo = new Foo(_tracer);
            foo.MyMethod();
            Thread th = new Thread(M1);
            th.Start();
            th.Join();
            serializeUsingPlugins(pluginLoader.plugins, Serialization.Dto.CreateTraceResultDto(_tracer.GetTraceResult()));
        }
    }
}
