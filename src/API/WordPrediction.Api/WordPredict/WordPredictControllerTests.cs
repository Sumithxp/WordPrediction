using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using WordPrediction.Application.Words.Queries.GetPredictWords;

namespace WordPrediction.Api.WordPredict
{
    [TestFixture]
    public class WordPredictControllerTests
    {
        private WordPredictController _controller;
        private AutoMocker _mocker;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<WordPredictController>();
        }

        [Test]
        [TestCase("te")]
        public void TestGetPredictedWordsShouldReturnListOfWordsByTerm(string term)
        {
            var predictWordModels = new List<PredictWordModel> { new PredictWordModel() };
            _mocker.GetMock<IGetPredictWordsListQuery>()
                .Setup(q => q.Execute(term))
                .Returns(predictWordModels);
            var results = _controller.Get(term);

            Assert.That(results, Is.EqualTo(predictWordModels));
        }
    }
}
