using KYC_APP.Data.ViewModels;
using KYC_APP.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KYC_APP.Data.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly DataContext _dataContext;

        public DocumentTypeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> CreateDocumentType(DocumentTypesVM documentTypesVM)
        {
            try
            {
                // Mapping ViewModel data to Model
                var documentType = new DocumentTypes
                {
                    Category = documentTypesVM.Category,
                    RenewalPeriod = documentTypesVM.RenewalPeriod,
                    Name = documentTypesVM.Name,
                    type = documentTypesVM.Type,
                    Description = documentTypesVM.Description,
                    status = true, // Assuming newly created document type is active by default
                    DateCreated = DateTime.Now // Assuming DateCreated should be set to current datetime
                };

                // Add to DbContext
                _dataContext.DocumentTypes.Add(documentType);

                // Save changes to the database
                _dataContext.SaveChanges();

                return true; // DocumentType created successfully
            }
            catch (Exception ex)
            {
                // Log the exception here
                Console.WriteLine(ex.Message);

                //ADD MESSAGE TO LOGGER

                return false; // DocumentType creation failed
            }
        }

        public async Task<List<KYCRequirementsVM>> GetDocumentRequirementsAsync()
        {
            var Documenttypes = await GetDocumentTypesAsync();
            // Create a list to store KYC requirements
            var kycRequirements = new List<KYCRequirementsVM>();

            // Iterate through each document type
            foreach (var documentType in Documenttypes)
            {
                // Create a new KYC requirement based on the document type
                var kycRequirement = new KYCRequirementsVM
                {
                    DocumentType = documentType.Name,
                    Description = documentType.Description,
                    Category = documentType.Category,
                    RenewalPeriod = documentType.RenewalPeriod,
                    type = documentType.type
                };

                // Add the KYC requirement to the list
                kycRequirements.Add(kycRequirement);
            }

            return kycRequirements;
        }

        public async Task<List<DocumentTypes>> GetDocumentTypesAsync()
        {
            return await _dataContext.DocumentTypes.ToListAsync();
        }
    }
}
