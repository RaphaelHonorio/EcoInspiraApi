using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;

namespace EcoInspira.Application.UseCases.Post.Filter
{
    public interface IFilterPostUseCase
    {
        public Task<ResponseListPostsJson> Execute(RequestFilterPostJson request);
    }
}
