namespace DesafioMSA.Presentation.StartupInstaller
{
    public interface IInstaller
    {
        void InstallService(IServiceCollection services, IConfiguration configuration);
    }
}
