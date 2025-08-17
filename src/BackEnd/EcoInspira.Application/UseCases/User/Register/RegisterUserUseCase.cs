using AutoMapper;
using EcoInspira.Application.Services.Cryptography;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using EcoInspira.Domain.Repositories;
using EcoInspira.Domain.Repositories.User;
using EcoInspira.Exceptions.ExceptionsBase;

namespace EcoInspira.Application.UseCases.User.Register
{
    public class RegisterUserUseCase :IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PasswordEncripter _passowordEncripter;

        public RegisterUserUseCase(
            IUserWriteOnlyRepository writeOnlyRepository, 
            IUserReadOnlyRepository ReadOnlyRepository,
            IUnitOfWork unitOfWork,
            PasswordEncripter passowordEncripter,
            IMapper mapper
            )
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = ReadOnlyRepository;
            _mapper = mapper;
            _passowordEncripter = passowordEncripter;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            //--== Validar Request
            Validade(request);

            //--== mapear a request em um entidade
            var user = _mapper.Map<Domain.Entities.User>(request);
            user.Password = _passowordEncripter.Encrypyt(request.Password);

            await _writeOnlyRepository.Add(user);


            await _unitOfWork.Commit();

            // salvar no banco de dados
            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
            };
        }

        private void Validade(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidador();

            var result = validator.Validate(request);

            if ( result.IsValid == false)
            {
                var errorMenssages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMenssages);
            }
        }
    }
}
