using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;

namespace EcoInspira.Application.UseCases.Post.Register
{
    public interface IRegisterPostUserCase
    {
        public Task<ResponseRegisteredPostJson> Execute(RequestPostJson request);
    }
}