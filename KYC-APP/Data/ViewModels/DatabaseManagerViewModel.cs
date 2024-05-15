namespace KYC_APP.Data.ViewModels
{
    public class DatabaseManagerViewModel
    {
        public List<RequirementsVM> RequirementsVM { get; set; }
        public List<CustomDocumentsVM> CustomDocumentTypes { get; set; }

        public List<AvailableDocumentsTypeVM> AvailableDocumentsTypes {get; set;}

        public AddCustomDocumentTypeVM AddCustomDocumentTypeVM { get; set; }

        public AddKYCRequirementsVM AddKYCRequirementsVM { get; set; }

        public List<DatabaseListVM> DatabaseListVM { get; set; }

        


    }
}
