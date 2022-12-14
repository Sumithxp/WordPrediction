
using Microsoft.Data.Sqlite;
using WordPrediction.Api.Middlewares;
using WordPrediction.Application.Interfaces;
using WordPrediction.Application.Words.Queries.GetPredictWords;
using WordPrediction.Infrastructure;
using WordPrediction.Persistence;
using Microsoft.EntityFrameworkCore;
using WordPrediction.Application.Words.Queries.GetPredictWordDictionary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddDbContext<DatabaseService>((options, context) =>
{
    var connection = new SqliteConnection(builder.Configuration.GetConnectionString("WordDatabase"));
    context.UseSqlite(connection);

});




builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IGetPredictWordsListQuery, GetPredictWordsListQuery>();
builder.Services.AddScoped<IGetPredictWordDictionaryQuery, GetPredictWordDictionaryQuery>();
builder.Services.AddScoped<IPredictedWordsService, PredictedWordsService>();
builder.Services.AddHttpClient("Wizkids", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://services.lingapps.dk");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom basic auth middleware
    app.UseMiddleware<BasicAuthMiddleware>();
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}

app.Run();
