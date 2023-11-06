using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FormAPP.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName(" Remember Me")]
        public bool RememberMe { get; set; }
        public int LoginAttempts { get; set; }
        public bool IsBlocked { get; set; }
    }
}
