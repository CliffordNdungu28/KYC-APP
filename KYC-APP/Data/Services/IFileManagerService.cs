using KYC_APP.Data.ViewModels;

namespace KYC_APP.Data.Services
{
    public interface IFileManagerService
    {

 

        Task<bool> SaveFiles(UploadKYCVM uploadKYCVM);

        Task <List<AvailableDocumentsVM>> GetFilePathsPerCategory();

    }
}
