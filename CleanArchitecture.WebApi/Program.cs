using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using Persistance.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddAutoMapper(typeof(Persistance.AssemblyReference).Assembly);

// appsettings.json dosyas�ndaki connection stringi al�p DbContext'e ekliyorum.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddControllers()
    // AddApplicationPart ve Assembly ile farkl� bir katmanda y�netilen controller � Api katman�na �a��rm�� oldum.
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

// MediatR ve CQRS i�in gerekli olan k�t�phaneleri ekliyorum.
builder.Services.AddMediatR(cfr =>
cfr.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
