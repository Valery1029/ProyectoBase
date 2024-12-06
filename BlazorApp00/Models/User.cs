namespace BlazorApp00.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty; 
        public string Username { get; set; } = string.Empty; 
        public bool IsActive { get; set; }
    }

}
