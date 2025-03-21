var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    // AddApplicationPart ve Assembly ile farklý bir katmanda yönetilen controller ý Api katmanýna çaðýrmýþ oldum.
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

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
