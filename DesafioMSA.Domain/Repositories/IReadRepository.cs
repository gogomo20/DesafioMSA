using DesafioMSA.Domain.Repositories.Dtos;
using DesafioMSA.Domain.Shared;
using System.Linq.Expressions;

namespace DesafioMSA.Domain.Repositories
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        Task<T?> Get(long id, CancellationToken cancellation = default);
        Task<bool> Any(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    }
}
