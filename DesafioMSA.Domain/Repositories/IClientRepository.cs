using DesafioMSA.Domain.Entities;
using DesafioMSA.Domain.Repositories.Dtos;
using DesafioMSA.Domain.Repositories.Views;

namespace DesafioMSA.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<PagedListedView<ClientView>> GetPagedAsync(ListQueryDto<ClientView> query, CancellationToken cancellationToken = default);
        Task<ClientView?> GetClientAsync(long Id, CancellationToken cancellationToken = default);
    }
}
