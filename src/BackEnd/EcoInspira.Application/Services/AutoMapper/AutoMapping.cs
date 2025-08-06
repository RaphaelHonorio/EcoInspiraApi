using AutoMapper;
using EcoInspira.Communication.Requests;

namespace EcoInspira.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            RequestToDomain();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entiities.User>();
        }
    }
}
