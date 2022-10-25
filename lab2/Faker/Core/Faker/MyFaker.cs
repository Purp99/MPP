using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class MyFaker : IFaker
    {
        private const int MaxTypeDepth = 1;
        private TypeTree _tree = new TypeTree();

        private Dictionary<Type, Generators.IGenerator> _generators = new Dictionary<Type, Generators.IGenerator>
        {
            { typeof(string), new Generators.StringGenerator() },
            { typeof(bool), new Generators.BoolGenerator()},
            { typeof(DateTime), new Generators.DateTimeGenerator() },
            { typeof(int), new Generators.IntGenerator() },
            { typeof(long), new Generators.LongGenerator() },
            { typeof(double), new Generators.DoubleGenerator() },
            { typeof(float), new Generators.FloatGenerator() },
            { typeof(char), new Generators.CharGenerator() },
            { typeof(IList), new Generators.ListGenerator() }
        };

        public MyFaker() { }

        public T Create<T>()
        {
            _tree = new TypeTree();

            return (T)Create(typeof(T));
        }

        public object Create(Type t)
        {
            if (null == _tree.Current)
                _tree.Clear();

            Node node = new Node(t);
            node.Parent = _tree.Current;
            _tree.Current.AddChild(node);
            _tree.Current = node;

            if (_tree.Current.Parent.GetRepetitions(t) > MaxTypeDepth)
                return null;

            var generator = _generators.FirstOrDefault(gen => gen.Value.CanGenerate(t)).Value;

            if (null == generator)
            {
                return CreateWithConstructor(t);
            }
            else
            {
                Random random = new Random();
                return generator.Generate(t, new Generators.GeneratorTerm(random, this));
            }
        }

        private static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            else
                return null;
        }

        private object CreateWithConstructor(Type type)
        {
            var constructors = type
                .GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                .OrderByDescending(ctor => ctor.GetParameters().Length);

            foreach (var ctor in constructors)
            {
                var parametersList = new List<object>();
                var ctorParameters = ctor.GetParameters();

                foreach (var parameter in ctorParameters)
                {
                    parametersList.Add(Create(parameter.ParameterType));
                    _tree.Current = _tree.Current.Parent;
                }

                try
                {
                    var obj = Activator.CreateInstance(type, args: parametersList.ToArray());

                    SetProperties(obj);
                    SetFields(obj);

                    return obj;
                }
                catch (Exception ex)
                {
                }
            }

            return GetDefaultValue(type);
        }

        private void SetProperties(Object obj)
        {
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                var value = propertyInfo.GetValue(obj, null);

                if (propertyInfo.GetSetMethod() != null
                    && (value == null
                    || string.IsNullOrEmpty(value.ToString())
                    || value.ToString().Equals("0")))
                {
                    propertyInfo.SetValue(obj, Create(propertyInfo.PropertyType));
                    _tree.Current = _tree.Current.Parent;
                }
            }
        }

        private void SetFields(Object obj)
        {
            foreach (FieldInfo fieldInfo in obj.GetType().GetFields())
            {
                var value = fieldInfo.GetValue(obj);

                if (!fieldInfo.IsInitOnly
                    && (value == null
                    || string.IsNullOrEmpty(value.ToString())
                    || value.ToString().Equals("0")))
                {
                    fieldInfo.SetValue(obj, Create(fieldInfo.FieldType));
                    _tree.Current = _tree.Current.Parent;
                }
            }
        }
    }
}

