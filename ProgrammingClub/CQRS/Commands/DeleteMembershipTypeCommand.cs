using MediatR;

public class DeleteMembershipTypeCommand : IRequest<bool>
{
    public Guid IdMembershipType { get; set; }
    public DeleteMembershipTypeCommand(Guid idMembershipType)
    {
        IdMembershipType = idMembershipType;
    }
}