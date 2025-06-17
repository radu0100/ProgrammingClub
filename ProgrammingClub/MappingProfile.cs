using AutoMapper;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateOrUpdateModels;

namespace ProgrammingClub
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Member, UpdateMemberPartially>().ReverseMap();
        }
    }
}