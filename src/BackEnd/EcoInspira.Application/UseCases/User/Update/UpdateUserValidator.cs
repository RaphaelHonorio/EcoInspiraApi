using EcoInspira.Communication.Requests;
using EcoInspira.Exceptions;
using FluentValidation;

namespace EcoInspira.Application.UseCases.User.Update
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateUserValidator() 
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        }
    }
}
