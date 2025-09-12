using EcoInspira.Domain.Entities;
using EcoInspira.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace EcoInspira.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly EcoInspiraDbContext _dbContext;
        public UserRepository (EcoInspiraDbContext dbContext) => _dbContext = dbContext;

        //--== Adiciona o Usuario ao Banco de dados
        public async Task Add(User user) => await _dbContext.User.AddAsync(user);

        //--== Verifica no banco de dados se já tem o email cadastrado
        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.User.AnyAsync(user => user.Email.Equals(email) && user.Active);

        public async Task<User?> GetByCpfAndPassword(string cpf, string password)
        {
          return await _dbContext
                .User
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Active && user.Cpf.Equals(cpf) && user.Password.Equals(password));
        }
    }
}