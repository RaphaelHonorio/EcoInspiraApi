using CommonTestUtilities.Requests;
using FluentAssertions;
using EcoInspira.Application.UseCases.User.Register;
using EcoInspira.Exceptions;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Sucess()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Name_Empty()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
        }

        [Fact]
        public void Error_Email_Empty()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = "Email.com";

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void Error_Passoword_Invalid(int passwordLength)
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);
            request.Password = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();

            result.Errors.Should().ContainSingle()
                .And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_INVALID));
        }
    }
}
