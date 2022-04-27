using System.Text.Json.Serialization;
using BLL.Services;
using DAL;
using WebApiTasks.Middlewares;
using WebApiTasks.Utils.Json;

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
    x.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
});
builder.Services.AddDalServices();
builder.Services.AddScoped<MoviesService>();
builder.Services.AddScoped<CommentsService>();

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