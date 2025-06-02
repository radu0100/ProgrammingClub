using ProgrammingClub.Models;

namespace ProgrammingClub.Repositories
{
    public interface iMembersRepository
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(Guid id);
        Task AddMemberAsync(Member member);
        Task<bool> UsernameExistsAsync(string username);
        Task<Member> UpdateMemberAsync(Member member);
        Task<Member> UpdateMemberPartiallyAsync(Member member);
        Task<bool> MemberExistAsync(Guid id);
        Task<bool> DeleteMemberAsync(Guid id);

    }
}