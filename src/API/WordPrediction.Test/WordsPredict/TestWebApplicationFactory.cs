using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WordPrediction.Api.Tests.Integration.WordsPredict.MockServices;
using WordPrediction.Application.Words.Queries.GetPredictWordDictionary;
using WordPrediction.Application.Words.Queries.GetPredictWords;
using WordPrediction.Persistence;

namespace WordPrediction.Api.Tests.Integration
{
    internal class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        public TestWebApplicationFactory()
        {
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {
                services.AddScoped<IGetPredictWordsListQuery, GetPredictWordsListQueryMock>();
                services.AddScoped<IGetPredictWordDictionaryQuery, GetPredictWordDictionaryQueryMock>();

            });

            return base.CreateHost(builder);
        }
    }
}
