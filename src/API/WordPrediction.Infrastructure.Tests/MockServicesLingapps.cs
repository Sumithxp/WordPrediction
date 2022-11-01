using AutoFixture;
using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WordPrediction.Infrastructure.Tests
{
    public class MockServiceslingapps
    {
        public Mock<HttpMessageHandler> handlerMock;
        public Mock<IHttpClientFactory> mockHttpClientFactory;

        public MockServiceslingapps()
        {
            SetUplingappsService();
        }

        private void SetUplingappsService()
        {
            var fixture = new Fixture();
            var testUri = fixture.Create<Uri>();

            handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            HttpResponseMessage result = new HttpResponseMessage();
            var mockContent = new string[] { "test1" , "test2"};



            handlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
              )
              .ReturnsAsync(new HttpResponseMessage
              {
                  StatusCode = System.Net.HttpStatusCode.OK,
                  Content = new StringContent(JsonSerializer.Serialize(mockContent)),
              })
              .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = testUri
            };

            mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);
        }

    }
}
