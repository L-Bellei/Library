using Library.Domain.Repositories.UserRepo;
using Library.Domain.Services.TokenServices;
using Library.Domain.Services.UserServices;
using Library.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["ConnectionStrings:LibraryDb"]);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var key = Encoding.ASCII.GetBytes(builder.Configuration["Secret"]);

builder.Services.AddAuthentication(tk =>
{
    tk.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    tk.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(tk =>
    {
        tk.RequireHttpsMetadata = false;
        tk.SaveToken = true;
        tk.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
