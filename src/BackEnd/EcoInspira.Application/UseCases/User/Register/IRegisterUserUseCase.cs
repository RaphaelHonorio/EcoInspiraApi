using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;

namespace EcoInspira.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
