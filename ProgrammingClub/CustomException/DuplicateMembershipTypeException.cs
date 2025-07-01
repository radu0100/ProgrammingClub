namespace ProgrammingClub.CustomException
{
    public class DuplicateMembershipTypeException : Exception
    {

        public DuplicateMembershipTypeException(string name) : base($"A membership type with the '{name}' already exist")
        {



        }

    }
}
