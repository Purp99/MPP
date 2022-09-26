using System;
using System.Collections.Generic;
using System.IO;

namespace Example
{
    class Program
    {
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

        static void Main(string[] args)
        {
            PluginLoader.PluginLoader pluginLoader = new PluginLoader.PluginLoader();
            pluginLoader.LoadPlugins();

            Core.Tracer tracer = new Core.Tracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();


            serializeUsingPlugins(pluginLoader.plugins, Serialization.Dto.CreateTraceResultDto(tracer.GetTraceResult()));
        }
    }
}
