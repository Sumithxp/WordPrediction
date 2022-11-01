using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading;

namespace WordPrediction.Infrastructure.Tests
{
    public static class MockExtension
    {

        public static void Verify(this Mock<HttpMessageHandler> mock, Func<HttpRequestMessage, bool> match)
        {
            mock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), // we expected a single external request
                ItExpr.Is<HttpRequestMessage>(req => match(req)
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
