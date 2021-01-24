using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Infrastructure.Data
{
    public class EfRepository<T> : ICrudRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<T> FindById(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<List<T>> ListAll(CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<T>().AsQueryable().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<T> Create(T query, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(query, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return query;
        }

        public async Task Update(T query, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Update(query);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(T query, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(query);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }
    }
}