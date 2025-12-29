using DesafioMSA.Presentation.StartupInstaller;
using System.Reflection;

namespace DesafioMSA.Presentation.Helpers
{
    public static class InstallerHelper
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IInstaller).IsAssignableFrom(t) && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>().ToList();
            installers.ForEach(installer => installer.InstallService(services, configuration));
        }
    }
}
