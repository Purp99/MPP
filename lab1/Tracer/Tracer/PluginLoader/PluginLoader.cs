using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginLoader
{
    public class PluginLoader
    {
        private const string Path = "../../../../../../Serialization/Serialization/Plugins";
        private const string FullPath = "C:/Users/sasha/OneDrive/Рабочий стол/BSUIR/Сourse 3/Sem 1/СПП/lab1/Serialization/Serialization/Plugins";
        //private const string AssemblyPath = "/bin/Debug/net5.0";
        private const string DllExt = "*.dll";
        public List<Type> plugins = new List<Type>();


        public void LoadPlugins()
        {
            var files = Directory.GetFiles(FullPath, DllExt, SearchOption.TopDirectoryOnly);

            foreach (var filePath in files)
            {
                var assembly = Assembly.LoadFrom(filePath);
                Type type = null;
                if (assembly.GetExportedTypes().Length != 0)
                {
                    type = assembly.GetExportedTypes()[0];
                }
                if (type != null && !type.IsInterface && type?.GetMethod("Serialize") != null)
                {
                    plugins.Add(type);
                }
            }


        }
    }
}
