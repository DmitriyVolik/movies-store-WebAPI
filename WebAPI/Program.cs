using System.Text.Json.Serialization;
using BLL.Services;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using WebAPI.Middlewares;
using WebAPI.Utils.Errors;
using WebAPI.Utils.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
     x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}).AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ContractResolver = new DefaultContractResolver()
    {
        NamingStrategy = new SnakeCaseNamingStrategy()
    };
});
builder.Services
     .Configure<ApiBehaviorOptions>(x =>
     {
         x.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult();
     });
builder.Services.AddDalServices();
builder.Services.AddScoped<MoviesService>();
builder.Services.AddScoped<CommentsService>();
builder.Services.AddScoped<DirectorsService>();
builder.Services.AddScoped<GenresService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>(builder.Configuration["api_key"]);

app.UseAuthorization();

app.MapControllers();

app.Run();