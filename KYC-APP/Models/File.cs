using Azure.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KYC_APP.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        public string filepath { get; set; }
        public bool status { get; set; }

        public bool deleted { get; set; }

        public DateTime DateAdded { get; set; }

        public string name { get; set; }
        public string DocumentType { get; set; }

        public string? type { get; set; }
       
        public string userid { get; set; }

        public DateTime Renewaldate { get; set; }

        public string? TargetProvider { get; set; }

       
    }
}
