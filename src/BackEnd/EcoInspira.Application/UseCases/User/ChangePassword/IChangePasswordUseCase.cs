using EcoInspira.Communication.Requests;

namespace EcoInspira.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        public Task Execute(RequestChangePasswordJson request);
    }
}
