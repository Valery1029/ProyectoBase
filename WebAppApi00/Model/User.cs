namespace WebAppApi00.Model
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string Lastname { get; set; } = string.Empty; 
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool IsActive { get; set; }
    }

}
