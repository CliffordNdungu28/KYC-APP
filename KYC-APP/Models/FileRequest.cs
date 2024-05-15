using Azure.Core;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace KYC_APP.Models
{
    public class FileRequest
    {
        [Key]
        public int id { get; set; }

        public string serviceproviderid { get; set; }

        public string documentid { get; set; }

        public string userid { get; set; }


    }
}
