using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Application.Services
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void BeingTransaction();
        bool TransactionHasStarted();
    }
}
