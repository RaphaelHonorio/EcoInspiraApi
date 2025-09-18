using AutoMapper;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using EcoInspira.Domain.Repositories;
using EcoInspira.Domain.Repositories.Post;
using EcoInspira.Domain.Services.LoggedUser;
using EcoInspira.Exceptions.ExceptionsBase;

namespace EcoInspira.Application.UseCases.Post.Register
{
    public class RegisterPostUserCase : IRegisterPostUserCase
    {
        private readonly IPostWriteOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterPostUserCase(
            IPostWriteOnlyRepository repository, 
            ILoggedUser loggedUser, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsePostJson> Execute(RequestPostJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var post = _mapper.Map<Domain.Entities.Post>(request);
            post.UserId = loggedUser.Id;

            var comments = request.Comments.ToList();

            post.Comments = _mapper.Map<IList<Domain.Entities.Comment>>(comments);

            await _repository.Add(post);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponsePostJson>(post);
           
        }

        private static void Validate(RequestPostJson response) {
            var result = new PostValidator().Validate(response);

            if (result.IsValid == false) 
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }

    }
}
