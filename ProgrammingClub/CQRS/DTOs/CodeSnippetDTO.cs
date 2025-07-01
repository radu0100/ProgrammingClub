namespace ProgrammingClub.CQRS.DTOs
{
    public class CodeSnippetDto
    {
        public string? Title { get; set; }
        public Guid? ContentCode { get; set; }
        public Guid? IDMember { get; set; }
        public int? Revision { get; set; }
        public Guid? IDSnippetPreviousVersion { get; set; }
        public DateTime? DateTimeAdded { get; set; }
        public bool? IsPublished { get; set; }
    }
}
