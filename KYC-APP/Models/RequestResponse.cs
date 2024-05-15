using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KYC_APP.Models
{
    public class RequestResponse
    {
        [Key]

        public int Id { get; set; }

        public string userid { get; set; }

        public string response { get; set; }

        public int requestid { get; set; }

        public DateTime Date { get; set; }

    }
}
