using AutoMapper;
using EcoInspira.Communication.Responses;
using EcoInspira.Domain.Repositories.Post;
using EcoInspira.Domain.Services.LoggedUser;
using EcoInspira.Exceptions;

namespace EcoInspira.Application.UseCases.Post.GetPostById
{
    public class GetPostByIdUseCase : IGetPostByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IPostReadOnlyRepository _repository;

        public GetPostByIdUseCase(IMapper mapper, ILoggedUser loggedUser, IPostReadOnlyRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
            _loggedUser = loggedUser; 
        }

        public async Task<ResponsePostJson> Execute(long postId)
        {
            var loggedUser = await _loggedUser.User();

            var post = await _repository.GetById(loggedUser, postId);

            if (post is null)
                throw new Exception(ResourceMessagesException.POST_TITLE_EMPTY);

            return _mapper.Map<ResponsePostJson>(post);
        }

    }
}
