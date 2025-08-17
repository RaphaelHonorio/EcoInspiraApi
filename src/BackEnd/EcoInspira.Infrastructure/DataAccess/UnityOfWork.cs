using EcoInspira.Domain.Repositories;

namespace EcoInspira.Infrastructure.DataAccess
{
    public class UnityOfWork : IUnitOfWork
    {
        private readonly EcoInspiraDbContext _dbContext;

        public UnityOfWork(EcoInspiraDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
