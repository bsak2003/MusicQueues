using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicQueues.Application.Common.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> ReadById(Guid id);
        Task<IEnumerable<T>> ReadAll();

        Task Create(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}