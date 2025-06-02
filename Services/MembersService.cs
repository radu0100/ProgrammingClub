using AutoMapper;
using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateOrUpdateModels;
using ProgrammingClub.Repositories;

namespace ProgrammingClub.Services
{
    public class MembersService : iMembersService
    {
        private readonly iMembersRepository _membersRepository;
        private readonly IMapper _mapper;
        public MembersService(iMembersRepository repository, IMapper mapper)
        {
            _membersRepository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _membersRepository.GetAllMembersAsync();
        }

        public async Task<Member> GetMemberByIdAsync(Guid id)
        {
            return await _membersRepository.GetMemberByIdAsync(id);
        }

        public async Task AddMemberAsync(Member member)
        {
            if (await _membersRepository.UsernameExistsAsync(member.Username))
                throw new ArgumentException("Username already exists.", nameof(member.Username));

            member.IDMember = Guid.NewGuid();
            await _membersRepository.AddMemberAsync(member);
        }

        public async Task<Member> UpdateMemberAsync(Guid id, Member member)
        {
            if (!await _membersRepository.MemberExistAsync(id))
            {
                return null;
            }
            member.IDMember = id;
            return await _membersRepository.UpdateMemberAsync(member);
        }

        public async Task<Member> UpdateMemberPartiallyAsync(Guid id, UpdateMemberPartially updateMember)
        {
            if (!await _membersRepository.MemberExistAsync(id))
            {
                return null;
            }
            Member member = _mapper.Map<Member>(updateMember);
            member.IDMember = id;
            return await _membersRepository.UpdateMemberPartiallyAsync(member);
        }

        public async Task<bool> DeleteMemberAsync(Guid id)
        {
            if (!await _membersRepository.MemberExistAsync(id))
            {
                return false;
            }
            return await _membersRepository.DeleteMemberAsync(id);
        }

    }
}