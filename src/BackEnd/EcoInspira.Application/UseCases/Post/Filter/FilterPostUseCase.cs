using AutoMapper;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using EcoInspira.Domain.Repositories.Post;
using EcoInspira.Domain.Services.LoggedUser;

namespace EcoInspira.Application.UseCases.Post.Filter
{
    public class FilterPostUseCase : IFilterPostUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IPostReadOnlyRepository _repository;

        public FilterPostUseCase(
            IMapper mapper,
            ILoggedUser loggedUser,
            IPostReadOnlyRepository repository
            )
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
        }

        public async Task<ResponseListPostsJson> Execute (RequestFilterPostJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var filters = new Domain.Dtos.FilterPostDto
            {

            };

            var post = await _repository.Filter( loggedUser, filters );

            return new ResponseListPostsJson
            {
                Post = _mapper.Map<List<ResponsePostJson>>(post)
            };
        }

        private static void Validate(RequestFilterPostJson request)
        {
           //--== Eu não quero mais fazer Validações pelo back
        }
    }
}
