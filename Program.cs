using Config;
using Controllers.Authentication;
using Services.Token;

var builder = WebApplication.CreateBuilder(args);

//Settings
UltiminerSettings settings = new ();
builder.Configuration.GetSection(UltiminerSettings.ULTIMINER_SECTION).Bind(settings);

builder.Services.AddSingleton(settings);

//Services
builder.Services.AddTransient<JwtTokenFactory>();

//Controllers
builder.Services.AddTransient<AuthController>();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Jwt

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Controllers
app.MapPost("/auth", async (string authCode) => await app.Services.GetService<AuthController>()!.PostAuthCode(authCode))
    .WithName("PostAuthCode");

app.Run();