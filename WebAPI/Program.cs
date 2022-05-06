using System.Text;
using BLL.Services;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using WebAPI.Authorization.Handlers;
using WebAPI.Authorization.Requirements;
using WebAPI.Extensions;
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

var authConfig = builder.Configuration.GetAuthConfiguration();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authConfig.Issuer,
            ValidateAudience = true,
            ValidAudience = authConfig.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.Key))
        };
    });

var permissions = builder.Configuration.GetAllRolePermissions();

builder.Services.AddAuthorization(options =>
{
    foreach (var permission in permissions)
    {
        options.AddPolicy(permission, builder => 
            builder.AddRequirements(new ClaimAuthorizationRequirement(permission)));
    }
});
builder.Services.AddSingleton<IAuthorizationHandler, ClaimAuthorizationHandler>();
builder.Services.AddDalServices(builder.Configuration);
builder.Services.AddScoped<MoviesService>();
builder.Services.AddScoped<CommentsService>();
builder.Services.AddScoped<DirectorsService>();
builder.Services.AddScoped<UsersService>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();