using Azure.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;

namespace KYC_APP.Models
{
    public class KYCRequirements
    {
        [Key]
        public int id { get; set; }

        public string serviceproviderid { get; set; }

        public int Documentid { get; set; }

        public bool status { get; set; }

        public bool deleted { get; set; }

        public DateTime DateCreated { get; set; }



       

    }
}
