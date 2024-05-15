using System.ComponentModel.DataAnnotations;

namespace KYC_APP.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string emailaddress { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
