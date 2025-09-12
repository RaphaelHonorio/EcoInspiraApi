using EcoInspira.Application.Services.AutoMapper;
using EcoInspira.Application.Services.Cryptography;
using EcoInspira.Application.UseCases.Login.DoLogin;
using EcoInspira.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoInspira.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddPasswordEncrypter(services, configuration);
            AddAutoMapper(services);
            AddUserCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services) {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUserCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        }
        private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
        {
           var additionalKey = configuration.GetValue<String>("Settings:Password:AdditionalKey");

            services.AddScoped(option => new PasswordEncripter(additionalKey!));
        }
    }
}
