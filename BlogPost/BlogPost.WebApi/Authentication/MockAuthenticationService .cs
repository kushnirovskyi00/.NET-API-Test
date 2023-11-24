namespace BlogPost.WebApi.Authentication
{
    public class MockAuthenticationService : IMockAuthenticationService
    {
        private readonly Dictionary<string, string> userCredentials = new Dictionary<string, string>
    {
        { "user1", "password1" },
        { "user2", "password2" },
       
    };

        public bool AuthenticateUser(string username, string password)
        {
            if (userCredentials.TryGetValue(username, out var expectedPassword))
            {
                return password == expectedPassword;
            }

            return false;
        }

        public string GenerateToken(string username)
        {
            // In a real-world scenario, you might use a JWT library to generate a secure token.
            // Here, we use a simple string concatenation for demonstration purposes.
            return $"MockToken_{username}_{DateTime.UtcNow.Ticks}";
        }
    }
}
