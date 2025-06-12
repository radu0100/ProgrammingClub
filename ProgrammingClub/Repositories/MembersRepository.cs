using Microsoft.EntityFrameworkCore;
using ProgrammingClub.DataContext;
using ProgrammingClub.Models;

namespace ProgrammingClub.Repositories
{
    public class MembersRepository : iMembersRepository
    {
        private readonly ProgrammingClubDataContext _context;
        public MembersRepository(ProgrammingClubDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetMemberByIdAsync(Guid id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.IDMember == id);
        }

        public async Task AddMemberAsync(Member member)
        {
            _context.Entry(member).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Members.AnyAsync(m => m.Username == username);
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            _context.Update(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<bool> MemberExistAsync(Guid id)
        {
            return await _context.Members.AnyAsync(m => m.IDMember == id);
        }

        public async Task<Member> UpdateMemberPartiallyAsync(Member member)
        {
            Member memberFromDb = await GetMemberByIdAsync(member.IDMember);

            if (memberFromDb == null)
            {
                return null;
            }

            UpdateIfNullOrEmpty(member.Username, value => memberFromDb.Username = value);
            UpdateIfNullOrEmpty(member.Password, value => memberFromDb.Password = value);
            UpdateIfNullOrEmpty(member.Name, value => memberFromDb.Name = value);
            UpdateIfNullOrEmpty(member.Title, value => memberFromDb.Title = value);
            UpdateIfNullOrEmpty(member.Description, value => memberFromDb.Description = value);
            UpdateIfNullOrEmpty(member.Resume, value => memberFromDb.Resume = value);

            _context.Update(memberFromDb);
            await _context.SaveChangesAsync();
            return memberFromDb;
        }

        public async Task<bool> DeleteMemberAsync(Guid id)
        {
            if (!await MemberExistAsync(id))
            {
                return false;
            }

            _context.Members.Remove(new Member { IDMember = id });
            await _context.SaveChangesAsync();
            return true;
        }

        private void UpdateIfNullOrEmpty(string newValue, Action<string> setter)
        {
            if (!string.IsNullOrEmpty(newValue))
            {
                setter(newValue);
            }
        }
    }
}