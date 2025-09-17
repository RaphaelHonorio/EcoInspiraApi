using EcoInspira.Domain.Entities;

namespace EcoInspira.Domain.Services.LoggedUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
