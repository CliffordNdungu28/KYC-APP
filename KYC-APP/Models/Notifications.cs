using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KYC_APP.Models
{
    public class Notifications
    {

        [Key]
        public int Id { get; set; }

        public bool isRead { get; set; }

        public string header { get; set; }

        public string body { get; set; }

        public string footer { get; set; }

        public DateTime DateCreated { get; set; }

        public string userid { get; set; }

       
    }
}
