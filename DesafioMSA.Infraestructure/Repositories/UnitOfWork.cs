using DesafioMSA.Application.Services;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITransaction? _transaction;
        private readonly ISession _session;
        public UnitOfWork(ISession session)
        {
            _session = session;
        }

        public void BeingTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            if (_transaction is null)
                throw new InvalidOperationException("A transação não foi iniciada!");
            await _transaction.CommitAsync();
        }

        public bool TransactionHasStarted()
        {
            return _transaction != null;
        }
    }
}
