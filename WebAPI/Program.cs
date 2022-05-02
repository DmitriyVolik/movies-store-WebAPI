using BLL.Services;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using WebAPI.Middlewares;
using WebAPI.Utils.Errors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ContractResolver = new DefaultContractResolver()
    {
        NamingStrategy = new SnakeCaseNamingStrategy()
    };
    x.SerializerSettings.Converters.Add(new StringEnumConverter());
});
builder.Services
     .Configure<ApiBehaviorOptions>(x =>
     {
         x.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult();
     });

builder.Services.AddDalServices(builder.Configuration);
builder.Services.AddScoped<MoviesService>();
builder.Services.AddScoped<CommentsService>();
builder.Services.AddScoped<DirectorsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionsMiddleware>();

app.UseMiddleware<ApiKeyMiddleware>(builder.Configuration["api_key"]);

app.UseAuthorization();

app.MapControllers();

app.Run();