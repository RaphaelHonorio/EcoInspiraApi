using EcoInspira.Application.Services.Cryptography;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using EcoInspira.Domain.Repositories.User;
using EcoInspira.Domain.Security.Tokens;
using EcoInspira.Exceptions.ExceptionsBase;

namespace EcoInspira.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public DoLoginUseCase(
            IUserReadOnlyRepository repository,
            IAccessTokenGenerator accessTokenGenerator,
            PasswordEncripter passwordEncripter
            )
        {
            _repository = repository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            var encriptedPassword = _passwordEncripter.Encrypyt(request.Password);

            var user = await _repository.GetByCpfAndPassword(request.Cpf, encriptedPassword) ?? throw new InvalidLoginException();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokensJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier),
                }
            };
        }
    }
}