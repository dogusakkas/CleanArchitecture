
using FluentValidation;
using MediatR;

namespace CleanArchitecture.WebApi.Configurations
{
    public class ApplicationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
        {
            // MediatR ve CQRS için gerekli olan kütüphaneleri ekliyorum.
            services.AddMediatR(cfr =>
            cfr.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly));



            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Application.Behaviors.ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);
        }
    }
}
