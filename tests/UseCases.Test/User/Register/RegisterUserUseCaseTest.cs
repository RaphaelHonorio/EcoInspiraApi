using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;

using EcoInspira.Application.UseCases.User.Register;
using EcoInspira.Exceptions;
using EcoInspira.Exceptions.ExceptionsBase;
using FluentAssertions;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {

        [Fact]
        public async Task Success() 
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            request.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 && e.ErrorMensages.Contains(ResourceMessagesException.EMAIL_INVALID));
        }

        private RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unityOfWork = UnityOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            if(string.IsNullOrEmpty(email)== false)
                readRepositoryBuilder.ExistActiveUserWithEmail(email);

            return new RegisterUserUseCase(writeRepository, readRepositoryBuilder.Build(), unityOfWork, passwordEncripter, null ,mapper);

        }
    }
}
