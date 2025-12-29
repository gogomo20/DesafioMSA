using DesafioMSA.Domain.Entities;
using DesafioMSA.Domain.Repositories;
using DesafioMSA.Domain.Repositories.Dtos;
using DesafioMSA.Domain.Repositories.Views;
using DesafioMSA.Domain.Shared;
using DesafioMSA.Infraestructure.Extensions;
using NHibernate;
using NHibernate.Linq;
using System.Linq.Dynamic.Core;

namespace DesafioMSA.Infraestructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ISession _session;
        public ClientRepository(
                ISession session
            )
        {
            _session = session;
        }
        public async Task<ClientView?> GetClientAsync(long Id, CancellationToken cancellationToken = default)
        {
            var query = _session.Query<Client>().Where(x => !x.Deleted).Select(x => new ClientView
            {
                Id = x.Id,
                FantasyName = x.FantasyName,
                Cnpj = x.Cnpj.ToString(),
                Active = x.Active
            });
            var cliente = await query.SingleOrDefaultAsync();
            if (cliente != null)
                cliente.Cnpj = new Cnpj(cliente.Cnpj).ToString();
            return cliente;
        }

        public async Task<PagedListedView<ClientView>> GetPagedAsync(ListQueryDto<ClientView> queryDto, CancellationToken cancellationToken = default)
        {
            var query = _session.Query<Client>().Where(x => !x.Deleted).Select(x => new ClientView
            {
                Id = x.Id,
                FantasyName = x.FantasyName,
                Cnpj = x.Cnpj.ToString(),
                Active = x.Active
            });
            if (!string.IsNullOrEmpty(queryDto.OrderBy))
                query = query.OrderBy($"{queryDto.OrderBy} {(queryDto.Ascending ? "ascending" : "descending")}");
            query.ApplyListFilters(queryDto.WhereFunctions);
            var count = await query.CountAsync();
            if (count > 0)
                query = query.Skip((queryDto.Page - 1) * queryDto.Size).Take(queryDto.Size);
            var data = await query.ToListAsync();
            data.ForEach(x => x.Cnpj = new Cnpj(x.Cnpj).ToString());

            return new PagedListedView<ClientView>
            {
                Data = data,
                Size = queryDto.Size,
                TotalRegisters = count,
                Page = queryDto.Page,
            };
         }
    }
}
