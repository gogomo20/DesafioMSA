using DesafioMSA.Domain.Entities;
using FluentNHibernate.Mapping;

namespace DesafioMSA.Infraestructure.ConfigurationMapper
{
    public class ClientMap : ClassMap<Client>
    {
        public ClientMap()
        {
            Id(x => x.Id);
            Map(x => x.FantasyName)
                .Not
                .Nullable()
                .Length(255);
            Component(x => x.Cnpj, cpart =>
            {
                cpart.Map(x => x.Value).Column("Cnpj");
            });
            Map(x => x.Active)
                .Not
                .Nullable()
                .Default("true");
            Map(x => x.DateCreation)
                .Nullable();
            Map(x => x.Deleted)
                .Not.Nullable()
                .Default("false");
            Table("Clientes");
        }
    }
}
