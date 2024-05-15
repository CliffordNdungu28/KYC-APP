using System.ComponentModel.DataAnnotations;

namespace KYC_APP.Models
{
    public class RejectedDocuments
    {
        [Key]
        public int Id { get; set; }

        public string serviceproviderid { get; set; }

        public int fileid { get; set; }

        public string Reason { get; set; }

        public DateTime DateCreated { get; set; }


    }
}
