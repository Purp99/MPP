using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            User user = new User("WW", "11");
            string res = user.GetGreeting("Name: {Name}\nTel: {Tel}");
            Assert.AreEqual("Name: WW\nTel: 11", res);
        }

        [Test]
        public void Test2()
        {
            User user = new User("WW", "11");
            string res = user.GetGreeting("Name: {{Name}\nTel: {Tel}");
            Assert.AreEqual(null, res);
        }

        [Test]
        public void Test3()
        {
            User user = new User("WW", "11");
            string res = user.GetGreeting("Name: {{Name}}\nTel: {Tel}");
            Assert.AreEqual("Name: {Name}\nTel: 11", res);
        }
    }
}