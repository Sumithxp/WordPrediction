using NUnit.Framework;

namespace WordPrediction.Domain
{
    [TestFixture]
    internal class WordTests
    {
        private Word _word;
        private const int Id = 1;
        private const string Value = "Test";

        [SetUp]
        public void Setup()
        {
            _word = new Word();
        }

        [Test]
        public void TestSetAndGetId()
        {
            _word.Id = Id;
            Assert.That(_word.Id, Is.EqualTo(Id));
        }

        [Test]
        public void TestSetAndGetValue()
        {
            _word.Value = Value;
            Assert.That(_word.Value, Is.EqualTo(Value));
        }
    }
}
