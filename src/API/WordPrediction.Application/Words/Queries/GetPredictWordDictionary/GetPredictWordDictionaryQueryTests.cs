using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using WordPrediction.Application.Interfaces;

namespace WordPrediction.Application.Words.Queries.GetPredictWordDictionary
{

    [TestFixture]
    public class GetPredictWordDictionaryQueryTests
    {
        private Mock<IPredictedWordsService> _predictedWordsServiceMock;
        private GetPredictWordDictionaryQuery _query;


        [SetUp]
        public void SetUp()
        {
            _predictedWordsServiceMock = new Mock<IPredictedWordsService>();
            _query = new GetPredictWordDictionaryQuery(_predictedWordsServiceMock.Object);
        }

        [Test]
        [TestCase(@"Test")]
        [TestCase(@"")]
        public async Task Execute_GetcustomDictionary_Should_Return_ListOfWords(string text)
        {
            var value = new List<PredictWordDictionaryModel>();
            var results = await _query.Execute(text);
            Assert.That(results, Is.EqualTo(value));
        }
    }
}
