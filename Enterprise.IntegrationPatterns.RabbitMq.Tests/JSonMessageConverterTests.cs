using Enterprise.IntegrationPatterns.Converters;
using NUnit.Framework;

namespace Enterprise.IntegrationPatterns.RabbitMq.Tests
{
    [TestFixture]
    public class JSonMessageConverterTests
    {
        class TestClass { public int Id { get; set; } public string TestProperty { get; set; } };

        [Test]
        public void ConvertBackAndForward_SimpleClass_PropertiesAreEqual()
        {
            var source = new TestClass()  { Id = 1, TestProperty = "foo" };
            var converter = new JsonMessageConverter();

            var result = converter.Serialize(source);
            var copy = converter.Deserialize<TestClass>(result);

            Assert.IsNotNull(copy);
            Assert.AreEqual(source.Id, copy.Id);
            Assert.AreEqual(source.TestProperty, copy.TestProperty);
        }
    }
}
