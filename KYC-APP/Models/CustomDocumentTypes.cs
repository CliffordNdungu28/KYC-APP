using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;

namespace KYC_APP.Models
{
    public class CustomDocumentTypes
    {
        [Key] public int Id { get; set; }

        public string serviceproviderid { get; set; }

        public string type { get; set; }

        public DateTime DateCreated { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }

        public string status { get; set; }

        public bool deleted { get; set; }


    }
}
