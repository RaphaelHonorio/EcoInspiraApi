using AutoMapper;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;

namespace EcoInspira.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<RequestPostJson, Domain.Entities.Post>()
                .ForMember(dest => dest.Comments, opt => opt.Ignore());

            CreateMap<RequestCommentJson, Domain.Entities.Comment>();
            
                
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
        }
    }
}
