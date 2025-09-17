using EcoInspira.Communication.Requests;

namespace EcoInspira.Application.UseCases.User.Update
{
    public interface IUpdateUserUseCase
    {
        public Task Execute(RequestUpdateUserJson request);
    }
}
