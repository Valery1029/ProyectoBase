namespace WebAppApi00.Model
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty; 
        public string Lastname { get; set; } = string.Empty; 
        public string Username { get; set; } = string.Empty; 
        public string Token { get; set; } = string.Empty;
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName; 
            Lastname = user.Lastname; 
            Username = user.Username; 
            Token = token;
        }
    }
}
