using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WordPrediction.Api.Models;
using WordPrediction.Api.Tests.Integration.WordsPredict;

namespace WordPrediction.Api.Tests.Integration
{
    [TestFixture]
    public class WordsPredictControllerTests
    {
        private TestWebApplicationFactory _factory;
        private HttpClient _client;
        private const string _url = "https://localhost:7189/api/";      


        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new TestWebApplicationFactory();
            _client = _factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "YWRtOjIwNDA=");
            _client.BaseAddress = new System.Uri(_url);       
        }

        [Test]
        [TestCase("te")]
        public async Task WhenGetWordsPredict_With_Valid_Param_ThenTheResultIsOk(string text)
        {
            // Arrange


            // Act
            var response = await _client.GetAsync($"WordsPredicts?text={text}");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Assert 
            var result = await response.Content.ReadAsAsync<PredictedWordsRespons>();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Local, Is.Not.Null);
            Assert.That(result.Custom, Is.Not.Null);
            Assert.AreEqual(result.Local.First(), MockConstraint.LocalWord);
            Assert.AreEqual(result.Custom.First(), MockConstraint.CustomWord);
        }

        [Test]
        [TestCase(" ")]
        public async Task WhenGetWordsPredict_With_InValid_Param_ThenTheResultIsOk(string text)
        {
            // Arrange

            // Act
            var response2 = await _client.GetAsync($"WordsPredicts?text={text}");

            // Assert 
            Assert.That(response2.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
