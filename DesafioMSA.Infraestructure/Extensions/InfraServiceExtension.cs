using DesafioMSA.Application.Extensions;
using DesafioMSA.Application.Services;
using DesafioMSA.Domain.Repositories;
using DesafioMSA.Infraestructure.ConfigurationMapper;
using DesafioMSA.Infraestructure.Repositories;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Tool.hbm2ddl;
using System.Data.SQLite;

namespace DesafioMSA.Infraestructure.Extensions
{
    public static class InfraServiceExtension
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            var conn = configuration.GetConnectionString("DefaultConnection");

            SQLiteConnection? connection = null;
            if(env == "Test")
            {
                connection = new SQLiteConnection("Data Source=:memory:;Version=3;New=True;");
                connection.Open();
            }
            var sessionFactory = Fluently.Configure()
                .Database(
                    env == "Test" ?
                    SQLiteConfiguration.Standard.InMemory().ShowSql()
                    : SQLiteConfiguration.Standard.ConnectionString(conn)
                )
                .Mappings(map =>
                {
                    map.FluentMappings.AddFromAssemblyOf<ClientMap>();
                })
                .ExposeConfiguration(cfg =>
                {
                    new SchemaUpdate(cfg)
                    .Execute(false, true);
                    if (connection != null)
                        new SchemaExport(cfg).Execute(false, true, false, connection, null);
                })
                .BuildSessionFactory();
            services.AddSingleton(sessionFactory);
            services.AddScoped(factory =>
            {
                var session = sessionFactory.OpenSession();
                return session;
            });
            services.AddRepositories();
            return services;
        }
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddTransient(typeof(IWriteRepository<>), typeof(WriteRepository<>));
        }
    }
}
