using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
                services.AddDbContext<DatabaseService>((options, context) =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    context.UseSqlite(connection);

                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DatabaseService>();
                db.Database.EnsureCreated();

            });

            return base.CreateHost(builder);
        }
    }
}
