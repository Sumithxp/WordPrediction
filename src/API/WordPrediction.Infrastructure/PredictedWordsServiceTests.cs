using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using WordPrediction.Application.Interfaces;

namespace WordPrediction.Infrastructure
{
    [TestFixture]
    public class PredictedWordsServiceTests
    {
        private AutoMocker _mocker;
        private PredictedWordsService _predictedWordsService;


        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMocker();
            _predictedWordsService = _mocker.CreateInstance<PredictedWordsService>();
        }

        [Test]
        public void Test()
        {
            _mocker.GetMock<IPredictedWordsService>()
                .Verify(x => x.GetPredictions("cat"), Times.Once);
        }
    }
}

