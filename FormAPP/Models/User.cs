using Microsoft.AspNetCore.Identity;

namespace LoginFormAttempts.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int LoginAttempts { get; set; }
        public bool IsBlocked { get; set; }
    }
}
