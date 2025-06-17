using AutoMapper;

namespace ProgrammingClub.Mapping
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Models.Member, v1.DTOs.MembersV1DTO>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<Models.Member, v2.DTOs.MembersV2DTO>()
                .ForMember(dest => dest.NumeComplet, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Descriere, opt => opt.MapFrom(src => src.Description));
        }
    }
}
