using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMSA.Domain.Repositories
{
    public interface IWriteRepository<T>
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
