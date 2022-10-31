using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
            var response2 = await _client.GetAsync($"WordsPredicts?text={text}");
            Assert.That(response2.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [TestCase(" ")]
        public async Task WhenGetWordsPredict_With_InValid_Param_ThenTheResultIsOk(string text)
        {
            var response2 = await _client.GetAsync($"WordsPredicts?text={text}");
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
