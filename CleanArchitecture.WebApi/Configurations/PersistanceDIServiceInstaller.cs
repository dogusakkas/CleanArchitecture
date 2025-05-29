
using Application.Abstractions;
using Application.Services;
using Domain.Repositories;
using GenericRepository;
using Infrastructure.Authentication;
using Infrastructure;
using Persistance.Context;
using Persistance.Repositories;
using Persistance.Services;

namespace CleanArchitecture.WebApi.Configurations
{
    public class PersistanceDIServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
        {
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IUnitOfWork>(cfr => cfr.GetRequiredService<AppDbContext>());
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();
        }
    }
}
