using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        private Core.IFaker _faker;

        [SetUp]
        public void Setup()
        {
            _faker = new Core.MyFaker();
        }

       

        [Test]
        public void TestGeneratedType_Equals()
        {
            TestClasses.Person person = _faker.Create<TestClasses.Person>();
            DateTime date = _faker.Create<DateTime>();
            Assert.That(_faker.Create<int>().GetType(), Is.EqualTo(typeof(int)));
            Assert.That(_faker.Create<long>().GetType(), Is.EqualTo(typeof(long)));
            Assert.That(_faker.Create<char>().GetType(), Is.EqualTo(typeof(char)));
            Assert.That(date.GetType(), Is.EqualTo(typeof(DateTime)));
            Assert.That(_faker.Create<bool>().GetType(), Is.EqualTo(typeof(bool)));
            Assert.That(_faker.Create<double>().GetType(), Is.EqualTo(typeof(double)));
            Assert.That(_faker.Create<float>().GetType(), Is.EqualTo(typeof(float)));
            Assert.That(_faker.Create<string>().GetType(), Is.EqualTo(typeof(string)));
            Assert.That(person.GetType(), Is.EqualTo(typeof(TestClasses.Person)));
            Assert.That(_faker.Create<List<List<int>>>().GetType(), Is.EqualTo(typeof(List<List<int>>)));
            Assert.That(_faker.Create<List<int>>().GetType(), Is.EqualTo(typeof(List<int>)));
            Assert.That(_faker.Create<TestClasses.User>().GetType(), Is.EqualTo(typeof(TestClasses.User)));
            Assert.That(_faker.Create<string>().GetType(), Is.EqualTo(typeof(string)));
        }

        [Test]
        public void TestCircularLoop_PersonDepth1()
        {
            TestClasses.Person p1 = _faker.Create<TestClasses.Person>();
            Assert.IsNotNull(p1.Parent);
            Assert.IsNull(p1.Parent.Parent);
        }

        [Test]
        public void TestCircularLoop_ABCDepth1()
        {
            TestClasses.A a = _faker.Create<TestClasses.A>();
            Assert.IsNotNull(a.B);
            Assert.IsNotNull(a.B.C);
            Assert.IsNull(a.B.C.A.B.C.A);
        }

        // --------- maxTypeDepth = 0-----------------
        /*[Test]//+
        public void TestCircularLoop_PersonDepth0()
        {
            Person p1 = _faker.Create<Person>();
            Assert.IsNotNull(p1.Parent);
            Assert.IsNull(p1.Parent.Parent);
        }
        [Test]//+
        public void TestCircularLoop_ABCDepth0()
        {
            A a = _faker.Create<A>();
            Assert.IsNotNull(a.B);
            Assert.IsNotNull(a.B.C);
            Assert.IsNotNull(a.B.C.A.B.C);
            Assert.IsNull(a.B.C.A.B.C.A);
        }*/

        [Test]
        public void TestPropertiesSetter_ABC()
        {
            TestClasses.User user2 = _faker.Create<TestClasses.User>();
            Assert.IsNotNull(user2.Id);
        }

        [Test]
        public void Test_StructConstructor()
        {
            TestClasses.Human person = _faker.Create<TestClasses.Human>();
            Assert.IsNotNull(person.name);
            Assert.That(person.GetType(), Is.EqualTo(typeof(TestClasses.Human)));
        }

    }
}
