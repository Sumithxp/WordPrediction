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
            _word = new Word() { Id = Id, Value = Value };

            _mocker.GetMock<IDatabaseService>()
                .Setup(p => p.Words)
                .ReturnsDbSet(new List<Word> { _word });
            _query = _mocker.CreateInstance<GetPredictWordsListQuery>();
        }

        [Test]
        [TestCase(@"Test")]
        [TestCase(@"")]
        public async Task TestExecuteShouldReturnListOfWords(string term)
        {
            var results = await _query.Execute(term);
            var result = results.Single();
            Assert.That(result.Value, Is.EqualTo(Value));
        }
    }
}
