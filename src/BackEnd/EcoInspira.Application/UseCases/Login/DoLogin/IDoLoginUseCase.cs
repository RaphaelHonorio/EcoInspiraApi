using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;

namespace EcoInspira.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
