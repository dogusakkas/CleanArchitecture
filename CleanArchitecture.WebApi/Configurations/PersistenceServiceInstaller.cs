using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using Serilog;

namespace CleanArchitecture.WebApi.Configurations
{
    public class PersistenceServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
        {

            services.AddAutoMapper(typeof(Persistance.AssemblyReference).Assembly);

            // appsettings.json dosyasındaki connection stringi alıp DbContext'e ekliyorum.
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                connectionString: connectionString,
                tableName: "Logs",
                autoCreateSqlTable: true)
                .CreateLogger();

            hostBuilder.UseSerilog();

        }
    }
}
