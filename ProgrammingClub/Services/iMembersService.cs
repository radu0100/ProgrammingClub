using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateOrUpdateModels;

namespace ProgrammingClub.Services
{
    public interface iMembersService
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(Guid id);
        Task AddMemberAsync(Member member);
        Task<Member> UpdateMemberAsync(Guid id, Member member);
        Task<Member> UpdateMemberPartiallyAsync(Guid id, UpdateMemberPartially member);
        Task<bool> DeleteMemberAsync(Guid id);

    }
}