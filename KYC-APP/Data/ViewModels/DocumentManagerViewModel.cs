namespace KYC_APP.Data.ViewModels
{
    public class DocumentManagerViewModel
    {

        public List<KYCRequirementsVM> KYCRequirementsVM { get; set; }

      public List<AvailableDocumentsVM> availableDocumentsVM { get; set; }

        public UploadKYCVM UploadKYCVM { get; set; }
    }
}
