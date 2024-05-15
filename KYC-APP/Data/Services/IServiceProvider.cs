using KYC_APP.Data.ViewModels;

namespace KYC_APP.Data.Services
{
    public interface IServiceProvider
    {

        Task<bool> AddServiceProvider(string id);
        Task<bool> RemoveServiceProvider(string serviceproviderid);

        
        Task <List<AvailableServiceProvidersVM>> GetServiceProvidersAvailable();

        Task<List<AddedServiceProviderVM>> GetServiceProvidersAdded();

        Task<List<RequirementsVM>> GetKYCRequirements();

        Task<List<AvailableDocumentsTypeVM>> GetKYCDocumentsTypesAvailable();

        Task<List<CustomDocumentsVM>> GetCustomDocumentTypes();

        Task<bool> AddCustomDocumentType(AddCustomDocumentTypeVM model);

        Task<bool> AddKYCRequirements(AddKYCRequirementsVM model);

        Task<List<DatabaseListVM>> GetDatabaseList();

        //GetKYCComplianceStatus 


    }
}
