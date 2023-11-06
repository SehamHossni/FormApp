using System.ComponentModel.DataAnnotations;

namespace FormAPP.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
    }
}
