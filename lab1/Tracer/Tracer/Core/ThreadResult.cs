using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [DataContract, Serializable]
    public class ThreadResult
    {
        public int Id { get; set; }
        public long Time { get; set; }

        [DataMember]
        public List<MethodResult> MethodsResult { get; set; }

        public ThreadResult(int id)
        {
            Id = id;
            MethodsResult = new List<MethodResult>();
        }

        public ThreadResult() { }

        public void PushMethod(string methodName, string className, string path)
        {
            MethodsResult.Add(new MethodResult(methodName, className, path));
        }

        public void PopMethod(string allMethodPath)
        {
            var index = MethodsResult.FindLastIndex(item => item.GetPath() == allMethodPath);

            if (index != MethodsResult.Count - 1)
            {
                var size = MethodsResult.Count - index - 1;
                var childMethods = MethodsResult.GetRange(index + 1, size);

                for (var i = 0; i < size; i++)
                    MethodsResult.RemoveAt(MethodsResult.Count - 1);

                MethodsResult[index].SetChilds(childMethods);
                MethodsResult[index].StopTime();
            }

            Time += MethodsResult[index].GetTime();
            MethodsResult[index].StopTime();
        }


    }
}
