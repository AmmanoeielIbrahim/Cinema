using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(CinemaDbContext _dbContext) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}