using Library.Domain.Repositories.BookRepo;
using Library.Domain.Repositories.InventoryRepo;
using Library.Domain.Repositories.LoanRepo;
using Library.Domain.Repositories.MovimentationRepo;
using Library.Domain.Repositories.PenaltyRepo;
using Library.Domain.Repositories.UserRepo;
using Library.Domain.Services.BookServices;
using Library.Domain.Services.InventoryServices;
using Library.Domain.Services.LoanServices;
using Library.Domain.Services.PenaltyServices;
using Library.Domain.Services.ReportServices;
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

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

builder.Services.AddTransient<IPenaltyService, PenaltyService>();
builder.Services.AddScoped<IPenaltyRepository, PenaltyRepository>();

builder.Services.AddTransient<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

builder.Services.AddTransient<IReportService, ReportService>();

builder.Services.AddScoped<IMovimentationRepository, MovimentationRepository>();

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
