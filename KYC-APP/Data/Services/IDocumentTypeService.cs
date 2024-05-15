using KYC_APP.Data.ViewModels;
using KYC_APP.Models;

namespace KYC_APP.Data.Services
{
    public interface IDocumentTypeService
    {

       Task<bool> CreateDocumentType(DocumentTypesVM documentTypesVM);

       Task<List<DocumentTypes>> GetDocumentTypesAsync();

       Task<List<KYCRequirementsVM>> GetDocumentRequirementsAsync();

      

    }
}
