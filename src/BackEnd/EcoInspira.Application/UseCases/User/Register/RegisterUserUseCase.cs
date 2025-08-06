
using EcoInspira.Application.Services.AutoMapper;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using EcoInspira.Exceptions.ExceptionsBase;

namespace EcoInspira.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            //--== Validar Request
            Validade(request);

            //--== mapear a request em um entidade
            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            var user = autoMapper.Map<Domain.Entiities.User>(request);

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
