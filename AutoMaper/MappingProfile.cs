using AutoMapper;
using DatingAppProCardoAI.Dto;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace DatingAppProCardoAI.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<ProfileDto, Domain.Profile>();
            CreateMap<MessageDto, Domain.Message>();
            CreateMap<ImageDto, Domain.Image>()
           .ForMember(dest => dest.Profile, opt => opt.Ignore());
            CreateMap<Domain.Message, MessageResponseDto>()
           .ForMember(dest => dest.SenderName, opt => opt.Ignore())
           .ForMember(dest => dest.ReceiverName, opt => opt.Ignore())
           .ForMember(dest => dest.ContentOfMessage, opt => opt.MapFrom(src => src.ContentOfMessage))
           .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => src.timeSend));
            CreateMap<FriendDto, Domain.UserFriendship>()
           .ForMember(dest => dest.friendId, opt => opt.MapFrom(src => src.friendReceiver));
            CreateMap<Domain.UserFriendship, FriendshipsResponseDto>()
                .ForMember(dest => dest._userId, opt => opt.Ignore())
                .ForMember(dest => dest._friendId, opt => opt.Ignore());

        }

    }
}
