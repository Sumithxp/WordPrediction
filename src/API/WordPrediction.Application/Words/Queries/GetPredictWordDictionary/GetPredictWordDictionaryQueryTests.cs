using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using WordPrediction.Application.Interfaces;

namespace WordPrediction.Application.Words.Queries.GetPredictWordDictionary
{

    [TestFixture]
    public class GetPredictWordDictionaryQueryTests
    {
        private GetPredictWordDictionaryQuery _query;
        private AutoMocker _mocker;

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMocker();
            _query = _mocker.CreateInstance<GetPredictWordDictionaryQuery>();
        }

        [Test]
        [TestCase(@"test")]
        [TestCase(@"")]
        public async Task Execute_GetcustomDictionary_Should_Return_ListOfWords(string text)
        {
            // Arrange
            string[] words = { "test1", "test2" };
            _mocker.GetMock<IPredictedWordsService>()
            .Setup(w => w.GetPredictions(text))
                .ReturnsAsync(() => words);

            // Act
            var result = await _query.Execute(text);

            // Assert
            _mocker.GetMock<IPredictedWordsService>()
               .Verify(x => x.GetPredictions(text), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(result.Count, 2);
            Assert.That(result.First().Value, Is.EqualTo("test1"));


        }
    }
}
