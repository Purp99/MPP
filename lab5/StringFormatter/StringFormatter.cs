using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace StringFormatter
{
    public class StringFormatter : IStringFormatter
    {
        public static readonly StringFormatter Shared = new StringFormatter();

        private static readonly ConcurrentDictionary<Expression<Func<object, string>>, Func<object, string>> Cache = new ConcurrentDictionary<Expression<Func<object, string>>, Func<object, string>>();
        public string Format(string template, object target)
        {
            bool isBalance = checkBalance(template);
            if (!isBalance)
            {
                try
                {
                    throw new Exception();
                }
                catch (Exception e){
                    Console.WriteLine("error");
                    return null;
                }
               
            }

        
            int index = 0, secondIndex = 0;
            string res = "";
            while ((index = template.IndexOf("{", index)) != -1)
            {
                if (template[index+1]!='{') {
                    res += template.Substring(secondIndex, index - secondIndex);
                    secondIndex = template.IndexOf("}", index);
                    index++;
                    string temp = template.Substring(index, secondIndex - index);
                    secondIndex++;
                   
                    res += GetCachedFunc(temp, target).Invoke(target);
                }
                else
                {
                    template = template.Remove(index, 1);
                    int tempIndex = template.IndexOf("}", index);
                    template = template.Remove(tempIndex, 1);
                    index = tempIndex;
                }
            }

            return res;
        }

        private bool checkBalance(string template)
        {

            int index = 0, count = 0;
            while((index = template.IndexOf("{", index)) != -1)
            {
                index++;
                count++;
            }
            index = 0;
            while((index = template.IndexOf("}", index)) != -1)
            {
                index++;
                count--;
            }
            return count == 0;
        }

        private Func<object, string> GetCachedFunc(string fieldName, object target)
        {
            var param = Expression.Parameter(typeof(object));
            var cast = Expression.Convert(param, target.GetType());
            var prop = Expression.PropertyOrField(cast, fieldName);
            var methodCall = Expression.Call(prop, "ToString", null);
            var expr = Expression.Lambda<Func<object, string>>(methodCall, param);
            return Cache.GetOrAdd(expr, k => k.Compile());
        }
    }
}
