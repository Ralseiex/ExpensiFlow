using ExpensiFlow.Ident;
using ExpensiFlow.Ident.Login;
using ExpensiFlow.Ident.Models;
using ExpensiFlow.Ident.Options;
using ExpensiFlow.Ident.Register;
using ExpensiFlow.Shared.Configurations;
using ExpensiFlow.Shared.Databases;
using ExpensiFlow.Shared.Messaging;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterOptions<JwtOptions>(builder.Configuration, "Jwt");
builder.Services.AddServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddDatabase(builder.Configuration);
builder.Services
    .AddIdentityCore<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = false;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
    })
    .AddEntityFrameworkStores<IdentContext>();
var options = builder.Configuration.GetOptions<MessageBusOptions>();
builder.Services.AddMessageBus(options, typeof(Program).Assembly);

var app = builder.Build();
app.ApplyMigrations<IdentContext>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();


app.MapPost("/register",
    async (string username, string password, IRegisterService registerService)
        => await registerService.Register(username, password));

app.MapGet("/token",
    async (string userName, string password, IAccessTokenService loginService)
        => await loginService.GetToken(userName, password));

app.MapGet("/validate-token",
    async (string token, IAccessTokenService loginService)
        => await loginService.ValidateToken(token));

app.Run();