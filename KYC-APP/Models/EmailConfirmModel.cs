namespace KYC_APP.Models
{
    public class EmailConfirmModel
    {
        public string Email { get; set; }
        public bool isConfirmed { get; set; }

        public bool EmailSent { get; set; }

        public bool EmailVerified { get; set; }
    }
}
