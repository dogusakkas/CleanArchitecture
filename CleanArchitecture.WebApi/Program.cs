using Application.Abstractions;
using Application.Services;
using CleanArchitecture.WebApi.Middleware;
using CleanArchitecture.WebApi.OptionsSetup;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using GenericRepository;
using Infrastructure;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using Persistance.Repositories;
using Persistance.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IUnitOfWork>(cfr => cfr.GetRequiredService<AppDbContext>());
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services.AddAuthentication().AddJwtBearer();


builder.Services.AddAutoMapper(typeof(Persistance.AssemblyReference).Assembly);

// appsettings.json dosyasýndaki connection stringi alýp DbContext'e ekliyorum.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddControllers()
    // AddApplicationPart ve Assembly ile farklý bir katmanda yönetilen controller ý Api katmanýna çaðýrmýþ oldum.
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

// MediatR ve CQRS için gerekli olan kütüphaneleri ekliyorum.
builder.Services.AddMediatR(cfr =>
cfr.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly));



builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Application.Behaviors.ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
