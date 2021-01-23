using System.Collections.Generic;
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
            return _dbContext.Set<T>().FirstOrDefaultAsync(x=>x.Id == id, cancellationToken);
        }

        public Task<List<T>> ListAll(CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<T>().AsQueryable().ToListAsync(cancellationToken);
        }

        public async Task<T> Create(T applicant, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(applicant, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return applicant;
        }

        public async Task Update(T applicant, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Update(applicant);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(T applicant, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(applicant);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}