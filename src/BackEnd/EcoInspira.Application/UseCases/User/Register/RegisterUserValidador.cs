using EcoInspira.Communication.Requests;
using EcoInspira.Exceptions;
using FluentValidation;

namespace EcoInspira.Application.UseCases.User.Register
{
    public class RegisterUserValidador : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidador()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_INVALID);
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID); 
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.PASSWORD_INVALID);
            RuleFor(user => user.Cpf).NotEmpty().WithMessage(ResourceMessagesException.CPF_EMPTY);
            RuleFor(user => user.DataNascimento).NotEmpty().WithMessage(ResourceMessagesException.DATANASCIMENO_EMPTY);
        }
    }
}
