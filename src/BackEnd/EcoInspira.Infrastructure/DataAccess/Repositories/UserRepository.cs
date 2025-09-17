using EcoInspira.Domain.Entities;
using EcoInspira.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace EcoInspira.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
    {
        private readonly EcoInspiraDbContext _dbContext;

        public UserRepository (EcoInspiraDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(User user) => await _dbContext.User.AddAsync(user);


        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.User.AnyAsync(user => user.Email.Equals(email) && user.Active);

        public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) => await _dbContext.User.AnyAsync( user => user.UserIdentifier.Equals(userIdentifier) && user.Active);

        public async Task<User?> GetByCpfAndPassword(string cpf, string password)
        {
          return await _dbContext
                .User
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Active && user.Cpf.Equals(cpf) && user.Password.Equals(password));
        }
    
        public async Task<User> GetById(long id)
        {
           return await _dbContext
                .User
                .FirstAsync(user => user.Id == id);
        }
        public void Update(User user) => _dbContext.User.Update(user);
    }
}