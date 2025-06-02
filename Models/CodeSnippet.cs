using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models

{
    public class CodeSnippet
    {
        [Key]
        public Guid IDCodeSnippet { get; set; }
        public string? Title { get; set; }
        public Guid? ContentCode { get; set; }
        public Guid? IDMember { get; set; }
        public int? Revision { get; set; }
        public Guid? IDSnippetPreviousVersion { get; set; }
        public DateTime? DateTimeAdded { get; set; }
        public bool? IsPublished { get; set; }
    }
}