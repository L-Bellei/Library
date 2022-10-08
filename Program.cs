using Library.Domain.Repositories.BookRepo;
using Library.Domain.Repositories.InventoryRepo;
using Library.Domain.Repositories.UserRepo;
using Library.Domain.Services.BookServices;
using Library.Domain.Services.TokenServices;
using Library.Domain.Services.UserServices;
using Library.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["ConnectionStrings:LibraryDb"]);

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
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

builder.Services.AddSwaggerGen(s =>
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    })
);

builder.Services.AddSwaggerGen(s =>
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    })
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(c =>
        c.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
