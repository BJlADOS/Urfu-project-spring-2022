namespace Workshop.Core.Domain.Model.UrfuData
{
    public class UrfuUserData
    {
        public string Email { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string AcademicGroup { get; }

        public UrfuUserData(string email, string lastName, string firstName, string middleName, string academicGroup)
        {
            Email = email;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            AcademicGroup = academicGroup;
        }
    }
}