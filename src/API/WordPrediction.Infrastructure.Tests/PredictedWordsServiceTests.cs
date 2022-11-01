
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPrediction.Infrastructure.Tests
{
    public class PredictedWordsServiceTests
    {
        IConfiguration configuration;
        public Mock<HttpMessageHandler> handlerMock;
        private Mock<IHttpClientFactory> mockHttpClientFactory;
        private PredictedWordsService predictedWordsService;

        [SetUp]
        public void Setup()
        {
            var inMemorySettings = new Dictionary<string, string> {
             {"WizKidsToken", "abc"}};

            configuration = new ConfigurationBuilder()
               .AddInMemoryCollection(inMemorySettings)
               .Build();

            var mockServiceslingapps = new MockServiceslingapps();
            handlerMock = mockServiceslingapps.handlerMock;
            mockHttpClientFactory = mockServiceslingapps.mockHttpClientFactory;
            predictedWordsService = new PredictedWordsService(mockHttpClientFactory.Object, configuration);

        }
        [Test]
        public async Task Test1()
        {
            // Arrange

            //Act

            var result = await predictedWordsService.GetPredictions("test");

            //Asset
            handlerMock.Verify(r => r.RequestUri.Query.Contains("test"));

            Assert.That(result, Is.Not.Null);
            Assert.IsTrue(result.Length > 0);
        }
    }
}