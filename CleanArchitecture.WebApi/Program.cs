using Microsoft.EntityFrameworkCore;
using Persistance.Context;

var builder = WebApplication.CreateBuilder(args);

// appsettings.json dosyasýndaki connection stringi alýp DbContext'e ekliyorum.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddControllers()
    // AddApplicationPart ve Assembly ile farklý bir katmanda yönetilen controller ý Api katmanýna çaðýrmýþ oldum.
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

// MediatR ve CQRS için gerekli olan kütüphaneleri ekliyorum.
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
