using Microsoft.AspNetCore.Identity;

namespace LoginFormAttempts.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; }
        public int DeviceCount { get; set; }
        public int LoginAttempts { get; set; }
        public bool IsBlocked { get; set; }
    }
}
