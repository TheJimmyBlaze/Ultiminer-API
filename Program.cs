using Config;
using Services.Token;

var builder = WebApplication.CreateBuilder(args);

//Settings
UltiminerSettings settings = new ();
builder.Configuration.GetSection(UltiminerSettings.ULTIMINER_SECTION).Bind(settings);

builder.Services.AddSingleton(settings);

//Services
builder.Services.AddTransient<JwtTokenFactory>();

//Controllers
builder.Services.AddControllers();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();