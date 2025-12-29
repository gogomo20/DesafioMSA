using DesafioMSA.Application.Services;
using DesafioMSA.Domain.Entities;
using DesafioMSA.Domain.Repositories;
using DesafioMSA.Domain.Shared;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DesafioMSA.Infraestructure.Repositories
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ISession _session;
        public ReadRepository(
                ISession session
            )
        {
            _session = session;
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _session.Query<TEntity>().Where(x => !x.Deleted).AnyAsync(expression);
        }

        public async Task<TEntity?> Get(long id, CancellationToken cancellation = default)
        {
            var response = await _session.GetAsync<TEntity>(id);
            return response.Deleted ? null : response;
        }
    }
}
