using WordPrediction.Api.Middlewares;
using WordPrediction.Application.Interfaces;
using WordPrediction.Application.Words.Queries.GetPredictWords;
using WordPrediction.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IGetPredictWordsListQuery, GetPredictWordsListQuery>();


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
