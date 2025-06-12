using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models
{
    public class Membership
    {
        [Key]
        public Guid IDMembership { get; set; }
        public Guid IDMember { get; set; }
        public Guid IDMembershipType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Level { get; set; }
    }
}