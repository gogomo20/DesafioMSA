using DesafioMSA.Application.Services;
using DesafioMSA.Domain.Repositories;
using DesafioMSA.Domain.Shared;
using NHibernate;

namespace DesafioMSA.Infraestructure.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISession _session;
        public WriteRepository(IUnitOfWork unitOfWork, ISession session)
        {
            _unitOfWork = unitOfWork;
            _session = session;
        }

        public async Task Create(TEntity entity)
        {
            if (!_unitOfWork.TransactionHasStarted())
                _unitOfWork.BeingTransaction();
            if (entity is BaseEntity)
                (entity as BaseEntity)!.Created();
            await _session.SaveAsync(entity);
        }

        public async Task Delete(TEntity entity)
        {
            if (!_unitOfWork.TransactionHasStarted()) _unitOfWork.BeingTransaction();
            _session.SetReadOnly(entity, false);
            if (entity is BaseEntity)
            {
                (entity as BaseEntity)!.Delete();
                await _session.UpdateAsync(entity);
                return;
            }
            await _session.DeleteAsync(entity);   
        }

        public async Task Update(TEntity entity)
        {
            if (!_unitOfWork.TransactionHasStarted()) _unitOfWork.BeingTransaction();
            _session.SetReadOnly(entity, false);
            await _session.UpdateAsync(entity);
        }
    }
}
