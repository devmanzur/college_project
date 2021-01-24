using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Infrastructure.Data
{
    public interface ISnapQueryRepository : ICrudRepository<SnapQuery>
    {
    }

    public class SnapQueryRepository : EfRepository<SnapQuery>, ISnapQueryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public Task<SnapQuery> FindById(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.SnapQueries.Include(x => x.Bids).ThenInclude(b => b.Maker)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public SnapQueryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}