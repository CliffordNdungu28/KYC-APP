using KYC_APP.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KYC_APP.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "Firstname")]
        public string? Firstname { get; set; }

        [Display(Name = "Lastname")]
        public string? Lastname { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "Institution")]
        public string? Institution { get; set; }


        


        [Display(Name = "Industry")]
        public InstitutionCategories? Category { get; set; } // Use the enum type


        public string? InstitutionVerified { get; set; }

        public bool Verified { get; set; }


    }
}
