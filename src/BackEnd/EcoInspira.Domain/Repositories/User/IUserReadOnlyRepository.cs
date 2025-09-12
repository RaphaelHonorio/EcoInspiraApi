namespace EcoInspira.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);

        public Task<Entities.User?> GetByCpfAndPassword(string cpf, string password);
    }
}
