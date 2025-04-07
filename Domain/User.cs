namespace Domain
{
    public class User : PersistableEntity
    {

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; }   = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;


    }
}
