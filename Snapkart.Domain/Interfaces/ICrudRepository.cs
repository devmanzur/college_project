using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Interfaces
{
    public interface ICrudRepository<T> where T : BaseEntity
    {
        Task<T> FindById(int id, CancellationToken cancellationToken = default);
        Task<List<T>> ListAll(CancellationToken cancellationToken = default);
        Task<T> Create(T query, CancellationToken cancellationToken = default);
        Task Update(T item, CancellationToken cancellationToken = default);
        Task Delete(T item, CancellationToken cancellationToken = default);
        IQueryable<T> Query();
    }
}