namespace KYC_APP.Data.ViewModels
{
    public class CustomDocumentsVM
    {

        public string Documentname { get; set;  }
        public string type { get; set; }



        public string RenewalPeriod { get; set; }
        public DateTime DateCreated { get; set; }
        public bool status { get; set; }
        public bool deleted { get; set; }
    }
}
