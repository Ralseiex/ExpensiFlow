using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using ExpensiFlow.Api.AccountIdAccessor;
using ExpensiFlow.Api.Infrastructure;
using ExpensiFlow.Infrastructure.Database;
using ExpensiFlow.Infrastructure.Services;
using ExpensiFlow.Infrastructure.Services.Category;
using ExpensiFlow.Shared.Configurations;
using ExpensiFlow.Shared.CorrelationId;
using ExpensiFlow.Shared.Databases;
using ExpensiFlow.Shared.Messaging;
using ExpensiFlow.Shared.Swagger;
using ExpensiFlow.UseCases.Categories.Create;
using ExpensiFlow.UseCases.Categories.Delete;
using ExpensiFlow.UseCases.Categories.Get;
using ExpensiFlow.UseCases.Categories.Search;
using ExpensiFlow.UseCases.Categories.Update;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration)
    => configuration.ReadFrom.Configuration(context.Configuration));
var configuration = builder.Configuration;
var services = builder.Services;

{
    services.AddDbContext<ExpensiFlowContext>(options
        => options.UseNpgsql(configuration.GetConnectionString("ExpensiFlow")));

    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = configuration["AuthOptions:Audience"],
                ValidIssuer = configuration["AuthOptions:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["AuthOptions:Key"] ?? string.Empty)
                )
            };
        });
    services.AddAuthorization();
    services.AddEndpointsApiExplorer();
    services.AddSwagger();
    services.AddControllers();
    services.AddHttpContextAccessor();
    var options = configuration.GetOptions<MessageBusOptions>();
    services.AddMessageBus(options, typeof(Program).Assembly);
    services.RegisterOptions<CorrelationIdOptions>(configuration);
    services.AddScoped<UserService>();
    services.AddScoped<ICategoryCreator, CategoryCreator>();
    services.AddScoped<ICategoryGetter, CategoryGetter>();
    services.AddScoped<ICategoryUpdater, CategoryUpdater>();
    services.AddScoped<ICategoryDeleter, CategoryDeleter>();
    services.AddScoped<ICategorySearcher, CategorySearcher>();
    services.AddAccountIdAccessor();
    services.AddHttpLogging(o => { });
    services.AddApiVersioning(o =>
    {
        o.ApiVersionReader = new UrlSegmentApiVersionReader();
    });
}

var app = builder.Build();
app.ApplyMigrations<ExpensiFlowContext>();

{
    app.UseCorrelationId();
    app.UseSerilogRequestLogging();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpLogging();
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseAccountIdAccessor();

    var versionSet = app.NewApiVersionSet()
        .HasApiVersion(1.0)
        .Build();
    app.MapEndpoints(versionSet);
}

app.Run();