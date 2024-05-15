using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;

namespace KYC_APP.Models
{
    public class DocumentTypes
    {
        [Key]
        public int id { get; set; }
        public string Category { get; set; }

        public string RenewalPeriod { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string type { get; set; }

        public bool status { get; set; }

        public bool deleted { get; set; }

        public DateTime DateCreated { get; set; }

        public string ownerid { get; set; }

        public bool generic { get; set; }

    }
}
