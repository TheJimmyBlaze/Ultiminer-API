
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Config;
using Services.Authentication;
using Services.Loot;
using Services.Resources;
using Services.Users;
using Services.Stats;
using Services.Experience;
using Database;

var builder = WebApplication.CreateBuilder(args);

//Settings
UltiminerSettings settings = new();
builder.Configuration.GetSection(UltiminerSettings.ULTIMINER_SECTION).Bind(settings);
builder.Services.AddSingleton(settings);

//Setup
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.WithOrigins("https://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddHttpClient();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//Database
string dbConnectionString = builder.Configuration.GetConnectionString("UltiminerDB");
builder.Services.AddDbContextFactory<UltiminerContext>(config => config.UseSqlServer(dbConnectionString));

//Services
builder.Services.AddTransient<UltiminerAuthentication>();
builder.Services.AddTransient<DiscordAuthentication>();
builder.Services.AddTransient<UserManager>();
builder.Services.AddTransient<LootMiner>();
builder.Services.AddTransient<ResourceManager>();
builder.Services.AddTransient<ExperienceManager>();
builder.Services.AddTransient<MiningStatsManager>();

Random random = new();
builder.Services.AddSingleton(random);
builder.Services.AddSingleton<LootTableIndex>();
builder.Services.AddSingleton<ResourceExperienceIndex>();

//Controllers
builder.Services.AddControllers();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config => {
    
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Add 'Bearer' before pasting the token or it wont work >:(",
        });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement(){
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});

//Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config => {
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(settings.Cryptography.GetSecret()),
            ValidateIssuer = false,
            ValidateAudience = false
        };
     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {

    endpoints.MapControllers()
        .RequireAuthorization();
});

app.Run();