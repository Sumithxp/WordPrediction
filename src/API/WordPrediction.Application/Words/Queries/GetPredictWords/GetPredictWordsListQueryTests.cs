using Moq;
using Moq.AutoMock;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using WordPrediction.Application.Interfaces;
using WordPrediction.Domain;

namespace WordPrediction.Application.Words.Queries.GetPredictWords
{
    [TestFixture]
    public class GetPredictWordsListQueryTests
    {
        private IGetPredictWordsListQuery _query;
        private AutoMocker _mocker;
        private Word _word;

        private const int Id = 1;
        private const string Value = "Test";

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMocker();            
            _query = _mocker.CreateInstance<GetPredictWordsListQuery>();
        }

        [Test]
        [TestCase(@"Test")]
        [TestCase(@"")]
        public async Task Execute_FindPrediction_Should_Return_ListOfWords(string text)
        {
            // Arrange
            _word = new Word() { Id = Id, Value = Value };

            _mocker.GetMock<IDatabaseService>()
                .Setup(p => p.Words)
                .ReturnsDbSet(new List<Word> { _word });
            // Act
            var result = await _query.Execute(text);

            // Assert

            _mocker.GetMock<IDatabaseService>()
                .Verify(x => x.Words, Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(result.Count, 1);
            Assert.That(result.First().Value, Is.EqualTo("Test"));
           
        }
    }
}
