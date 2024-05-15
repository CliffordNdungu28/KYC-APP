using System.ComponentModel.DataAnnotations;

namespace KYC_APP.Data.ViewModels
{
    public class DocumentTypesVM
    {


        [Required]
        public string Category { get; set; }

        [Required]
        public string RenewalPeriod { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]

        public string Type { get; set; }



       
    }
}
