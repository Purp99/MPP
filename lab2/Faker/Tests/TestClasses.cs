using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class TestClasses
    {
        public class Person
        {
            public string Name { get; set; }
            public Person Parent { get; set; }

            public Person(Person parent, String name)
            {
                Parent = parent;
                Name = name;
            }
        }

        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int Id { get; set; }
            public User(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        public struct Human
        {
            public string name;
            public int age;

            public Human(string name = "Tommy", int age = 1)
            {
                this.name = name;
                this.age = age;
            }
            public void Print() => Console.WriteLine($"Имя: {name}  Возраст: {age}");
        }

        public class A
        {
            public B? B { get; set; }
        }

        public class B
        {
            public C? C { get; set; }
        }

        public class C
        {
            public A? A { get; set; }
        }
    }
}
