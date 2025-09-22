using AutoMapper;
using EcoInspira.Communication.Requests;
using EcoInspira.Communication.Responses;
using Sqids;

namespace EcoInspira.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {

        private readonly SqidsEncoder<long> _idEnconder;
        public AutoMapping(SqidsEncoder<long> idEncoder) 
        {
            _idEnconder = idEncoder;    
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<RequestPostJson, Domain.Entities.Post>();
          //      .ForMember(dest => dest.Comments, opt => opt.Ignore());

            CreateMap<RequestCommentJson, Domain.Entities.Comment>();
            
                
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
            CreateMap<Domain.Entities.Post, ResponsePostJson>()
                .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEnconder.Encode(source.Id)));

            CreateMap<Domain.Entities.Post, ResponsePostJson>()
                .ForMember(dest => dest.Id, config => config.MapFrom(source => _idEnconder.Encode(source.Id)));
                
         
        }
    }
}
