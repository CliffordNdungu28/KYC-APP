using System.ComponentModel.DataAnnotations;

namespace KYC_APP.Models
{
    public class AddedServiceProviders
    {
        [Key]
        public int Id { get; set; }
        public string userid { get; set; }
        public string serviceproviderid { get; set; }


    }
}
