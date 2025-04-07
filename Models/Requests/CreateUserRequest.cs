namespace Models.Requests
{
    public sealed record CreateUserRequest(string FirstName, string LastName, string Email, string Password);
   
   
}
