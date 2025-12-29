
using DesafioMSA.Application.MyMediator.Implementation;
using DesafioMSA.Application.MyMediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DesafioMSA.Application.Extensions
{
    public static class ApplicationServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Add mediator to Application
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.FullName))
                .ToArray();

            services.AddScoped<IMediator, Mediator>();
            RegisterHandles(services, assemblies, typeof(IRequestHandler<,>));

            return services;
        }
        private static void RegisterHandles(IServiceCollection services, Assembly[] assemblies, Type handleInterface)
        {
            var types = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToList();
            foreach(var type in types)
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handleInterface);
                foreach(var iface in interfaces)
                {
                    services.AddTransient(iface, type);
                }

            }
        }
    }
}
