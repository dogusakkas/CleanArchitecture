using System.Reflection;

namespace CleanArchitecture.WebApi.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection InstallService(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostBuilder hostBuilder,
            params Assembly[] assemblies)

        {
            IEnumerable<IServiceInstaller> serviceInstallers = assemblies.SelectMany(x => x.DefinedTypes)
                .Where(IsAssingnableToType<IServiceInstaller>)
                .Select(Activator.CreateInstance).Cast<IServiceInstaller>();

            foreach (IServiceInstaller serviceInstaller in serviceInstallers)
            {
                serviceInstaller.Install(services, configuration, hostBuilder);
            }

            return services;

            static bool IsAssingnableToType<T>(TypeInfo typeInfo)
                => typeof(T).IsAssignableFrom(typeInfo) &&
                !typeInfo.IsAbstract && !typeInfo.IsInterface;
        }
    }
}
