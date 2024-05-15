using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KYC_APP.Models
{
    public class AccessSettings
    {
        [Key]

        public int id { get; set; }

        public int fileid { get; set; }

        public string owenerid { get; set; }

        public string userid { get; set; }
        
        public DateTime date { get; set; }


    }
}
