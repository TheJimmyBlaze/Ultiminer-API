using Config;
using Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

//Settings
UltiminerSettings settings = new ();
builder.Configuration.GetSection(UltiminerSettings.ULTIMINER_SECTION).Bind(settings);
builder.Services.AddSingleton(settings);

//Setup
builder.Services.AddHttpClient();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//Services
builder.Services.AddTransient<UltiminerAuthentication>();
builder.Services.AddTransient<DiscordAuthentication>();

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