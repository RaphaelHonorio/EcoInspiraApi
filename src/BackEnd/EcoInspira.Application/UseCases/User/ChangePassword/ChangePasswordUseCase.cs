using EcoInspira.Domain.Repositories.User;
using EcoInspira.Domain.Repositories;
using EcoInspira.Domain.Services.LoggedUser;
using EcoInspira.Domain.Security.Cryptography;
using EcoInspira.Communication.Requests;
using EcoInspira.Exceptions;
using EcoInspira.Exceptions.ExceptionsBase;

namespace EcoInspira.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordEncripter _passwordEncripter;

        public ChangePasswordUseCase(
           ILoggedUser loggedUser,
           IUserUpdateOnlyRepository repository,
           IPasswordEncripter userReadOnlyRepository,
           IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _passwordEncripter = userReadOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestChangePasswordJson request)
        {
            var loggedUser = await _loggedUser.User();

            Validate(request, loggedUser);

            var user = await _repository.GetById(loggedUser.Id);

            user.Password = _passwordEncripter.Encrypt(request.NewPassword);

            _repository.Update(user);

            await _unitOfWork.Commit();
        }

        
        private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
        {
            var result = new ChangePasswordValidator().Validate(request); 

            var currentPasswordEncripted = _passwordEncripter.Encrypt(request.Password);

            if (currentPasswordEncripted.Equals(loggedUser.Password) == false)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.PASSWORD_INVALID));

            if (result.IsValid == false)
                    throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList()); 
        }
    }
}
