
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Config;
using Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

//Settings
UltiminerSettings settings = new ();
builder.Configuration.GetSection(UltiminerSettings.ULTIMINER_SECTION).Bind(settings);
builder.Services.AddSingleton(settings);

//Setup
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.WithOrigins("*").AllowAnyHeader();
    });
});

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
builder.Services.AddSwaggerGen(config => {
    
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JSON Web Token based security",
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
                new string[] {}
            }
        });
});

//Authentication
builder.Services.AddAuthentication(auth => {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
     .AddJwtBearer(bearer => {
        bearer.RequireHttpsMetadata = false;
        bearer.SaveToken = true;
        bearer.TokenValidationParameters = new TokenValidationParameters
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

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers()
    .RequireAuthorization();

app.Run();