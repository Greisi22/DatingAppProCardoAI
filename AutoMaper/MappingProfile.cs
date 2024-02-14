using AutoMapper;
using DatingAppProCardoAI.Dto;
namespace DatingAppProCardoAI.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<ProfileDto, Domain.Profile>();
            CreateMap<MessageDto, Domain.Message>();
            CreateMap<ImageDto, Domain.Image>()
           .ForMember(dest => dest.Profile, opt => opt.Ignore());

        }
    }
}
