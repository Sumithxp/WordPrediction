using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using WordPrediction.Api.Models;
using WordPrediction.Application.Words.Queries.GetPredictWordDictionary;
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
            var predictWordDictionaryModel = new List<PredictWordDictionaryModel> { new PredictWordDictionaryModel() };
            _mocker.GetMock<IGetPredictWordsListQuery>()
                .Setup(q => q.Execute(text))
                .ReturnsAsync(predictWordModels);

            _mocker.GetMock<IGetPredictWordDictionaryQuery>()
                .Setup(q => q.Execute(text))
                .ReturnsAsync(predictWordDictionaryModel);

            var expected = new PredictedWordsRespons
            {
                Local = predictWordModels.Select(x => x.Value).ToArray(),
                Custom = predictWordDictionaryModel.Select(x => x.Value).ToArray()
            };

            var results = await _controller.Get(text);

            Assert.That(results.Local, Is.EqualTo(expected.Local));
            Assert.That(results.Custom, Is.EqualTo(expected.Custom));
        }
    }
}
