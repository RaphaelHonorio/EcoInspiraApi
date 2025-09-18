using EcoInspira.Application.Services.AutoMapper;
using EcoInspira.Application.UseCases.Login.DoLogin;
using EcoInspira.Application.UseCases.Post.Register;
using EcoInspira.Application.UseCases.User.ChangePassword;
using EcoInspira.Application.UseCases.User.Profile;
using EcoInspira.Application.UseCases.User.Register;
using EcoInspira.Application.UseCases.User.Update;
using FirebirdSql.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqids;

namespace EcoInspira.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services, configuration);
            AddUserCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services, IConfiguration configuration)
        {
            var sqids = new SqidsEncoder<long>(new()
            {
                MinLength = 10,
                Alphabet = configuration.GetValue<string>("Settings:IdCryptographyAlphabet")!
            });

            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping(sqids));
            }).CreateMapper());
        }

        private static void AddUserCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            services.AddScoped<IRegisterPostUserCase, RegisterPostUserCase>();
        }
    }
}
