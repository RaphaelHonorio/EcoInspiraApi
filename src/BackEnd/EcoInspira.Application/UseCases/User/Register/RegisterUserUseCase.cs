using AutoMapper;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using EcoInspira.Domain.Repositories;
using EcoInspira.Domain.Repositories.User;
using EcoInspira.Domain.Security.Cryptography;
using EcoInspira.Domain.Security.Tokens;
using EcoInspira.Exceptions;
using EcoInspira.Exceptions.ExceptionsBase;

namespace EcoInspira.Application.UseCases.User.Register
{
    public class RegisterUserUseCase :IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IPasswordEncripter _passowordEncripter;

        public RegisterUserUseCase(
            IUserWriteOnlyRepository writeOnlyRepository, 
            IUserReadOnlyRepository ReadOnlyRepository,
            IUnitOfWork unitOfWork,
            IPasswordEncripter passowordEncripter,
            IAccessTokenGenerator accessTokenGenerator,
            IMapper mapper
            )
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = ReadOnlyRepository;
            _mapper = mapper;
            _accessTokenGenerator = accessTokenGenerator;
            _passowordEncripter = passowordEncripter;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
           await Validade(request);

            var user = _mapper.Map<Domain.Entities.User>(request);
            user.Password = _passowordEncripter.Encrypt(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            await _writeOnlyRepository.Add(user);

            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokensJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier),
                }
            };
        }

        private async Task Validade(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidador();

            var result = validator.Validate(request);

            var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (emailExist)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
            }

            if ( result.IsValid == false)
            {
                var errorMenssages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMenssages);
            }
        }
    }
}
