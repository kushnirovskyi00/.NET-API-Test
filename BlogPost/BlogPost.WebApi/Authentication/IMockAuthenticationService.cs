namespace BlogPost.WebApi.Authentication
{
    public interface IMockAuthenticationService
    {
        bool AuthenticateUser(string username, string password);
        string GenerateToken(string username);
    }
}
