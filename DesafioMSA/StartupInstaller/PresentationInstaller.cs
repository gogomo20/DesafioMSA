using DesafioMSA.Infraestructure.Extensions;
namespace DesafioMSA.Presentation.StartupInstaller
{
    public class PresentationInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            InfraServiceExtension.AddInfraestructure(services, configuration);
        }
    }
}
