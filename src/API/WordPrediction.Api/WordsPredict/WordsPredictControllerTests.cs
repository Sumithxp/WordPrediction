using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using WordPrediction.Application.Words.Queries.GetPredictWords;

namespace WordPrediction.Api.WordsPredict
{
    [TestFixture]
    public class WordsPredictControllerTests
    {
        private WordsPredictController _controller;
        private AutoMocker _mocker;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<WordsPredictController>();
        }

        [Test]
        [TestCase("te")]
        public async Task GetPredicted_Words_Should_Return_ListOfWords(string text)
        {
            var predictWordModels = new List<PredictWordModel> { new PredictWordModel() };
            _mocker.GetMock<IGetPredictWordsListQuery>()
                .Setup(q => q.Execute(text))
                .ReturnsAsync(predictWordModels);
            var results = await _controller.Get(text);

            Assert.That(results, Is.EqualTo(predictWordModels));
        }
    }
}
