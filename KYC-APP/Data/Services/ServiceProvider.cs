using KYC_APP.Data.ViewModels;
using KYC_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace KYC_APP.Data.Services
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpcontext;
        public delegate void AddServiceProviderHandler(CheckKYCRequirementsVM checkKYCRequirementsVM);
        public event AddServiceProviderHandler OnServiceProviderAdded;

        public ServiceProvider(DataContext dataContext, IHttpContextAccessor httpContext)
        {
            _dataContext = dataContext;   
            _httpcontext = httpContext;
        }

        public string GetUserId()
        {
            return _httpcontext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        }
        public async Task<bool> AddServiceProvider(string id)
        {
          
            try
            {
                var userid = GetUserId();
                if (userid == null)
                {
                    throw new ApplicationException($"Unable to load user ID.");
                }

                var addedServiceProvider = new AddedServiceProviders
                {
                    userid = userid,
                    serviceproviderid = id
                };

                // Add the new service provider to the database
                _dataContext.AddedServiceProviders.Add(addedServiceProvider);
                await _dataContext.SaveChangesAsync();
                //var checkrequirements = new CheckKYCRequirementsVM
                //{
                //    serviceproviderid = id,
                //    // You may need to retrieve documentId from somewhere
                //    // For now, let's set it to null
                //    checker = userid
                //};
                //OnServiceProviderAdded?.Invoke(checkrequirements); // Trigger the event


                return true; // Indicate success
            }
            catch (Exception ex)
            {
                // Handle exception
                // Log or throw further if necessary
                return false; // Indicate failure
            }
        }

        public async Task<List<AddedServiceProviderVM>> GetServiceProvidersAdded()
        {
           
            
                try
                {
                // Check if the provided userId is valid
                var userid = GetUserId();
                    if (userid == null)
                    {
                        throw new ApplicationException($"Unable to load user with ID '{userid}'.");
                    }

                    // Retrieve added service providers for the user
                    var addedServiceProviders = await _dataContext.AddedServiceProviders
                        .Where(asp => asp.userid == userid)
                        .ToListAsync();

                    // Populate AddedServiceProviderVM model
                    var addedServiceProviderVMs = addedServiceProviders.Select(asp => new AddedServiceProviderVM
                    {
                        serviceprovidername = _dataContext.Users
                            .Where(u => u.Id == asp.serviceproviderid)
                            .Select(u => u.Institution)
                            .FirstOrDefault(),
                        Category = _dataContext.Users
                            .Where(u => u.Id == asp.serviceproviderid)
                            .Select(u => u.Category.ToString())
                            .FirstOrDefault()
                    }).ToList();

                    return addedServiceProviderVMs;
                }
                catch (Exception ex)
                {
                    // Handle exception
                    // Log or throw further if necessary
                    return null; // Indicate failure
                }
            

        }

        public async Task<List<AvailableServiceProvidersVM>> GetServiceProvidersAvailable()
        {
            List<AvailableServiceProvidersVM> availableProviders = new List<AvailableServiceProvidersVM>();

            // Get user's institution name
            var userid = GetUserId();
            if (userid == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userid}'.");
            }


            // Get all service providers where the institution is present
            var allServiceProviders = _dataContext.Users.Where(u => u.Institution != null).ToList();

            // Get already added service providers for the user
            var addedServiceProviders = _dataContext.AddedServiceProviders.Where(asp => asp.userid == userid).Select(asp => asp.serviceproviderid).ToList();

            // Filter service providers based on institution name and already added ones
            foreach (var serviceProvider in allServiceProviders)
            {
                if (!addedServiceProviders.Contains(serviceProvider.Id))
                {
                    availableProviders.Add(new AvailableServiceProvidersVM
                    {
                        ServiceProviderName = serviceProvider.Institution,
                        Industry = serviceProvider.Category.ToString(),
                        id = serviceProvider.Id
                    });
                }
            }

            return availableProviders;
            //list all service providers and their respective industry. 

        }

        public async Task<bool> RemoveServiceProvider(string serviceproviderid)
        {
            try
            {
                var userId = GetUserId();
                if (userId == null)
                {
                    throw new ApplicationException("Unable to retrieve user ID.");
                }

                // Find the added service provider entry to remove
                var serviceProviderToRemove = await _dataContext.AddedServiceProviders
                    .FirstOrDefaultAsync(asp => asp.userid == userId && asp.serviceproviderid == serviceproviderid);

                if (serviceProviderToRemove == null)
                {
                    // If the service provider is not found for the user, return false indicating failure
                    return false;
                }

                // Remove the service provider entry from the database
                _dataContext.AddedServiceProviders.Remove(serviceProviderToRemove);
                await _dataContext.SaveChangesAsync();

                return true; // Indicate success
            }
            catch (Exception ex)
            {
                // Handle exception
                // Log or throw further if necessary
                return false; // Indicate failure
            }
        }

        public async Task<List<RequirementsVM>> GetKYCRequirements()
        {
            var userid = GetUserId();

            var requirements = await _dataContext.KYCRequirements
                .Where(kyc => kyc.serviceproviderid == userid && !kyc.deleted)
                .Select(kyc => new RequirementsVM
                {
                    Documentname = _dataContext.DocumentTypes
                        .Where(doc => doc.id == kyc.Documentid)
                        .Select(doc => doc.Name)
                        .FirstOrDefault(),
                    Datecreated = kyc.DateCreated
                })
                .ToListAsync();

            return requirements;
        }

        public async Task<List<AvailableDocumentsTypeVM>> GetKYCDocumentsTypesAvailable()
        {
            var userid = GetUserId();
            var availableDocumentTypes = await _dataContext.DocumentTypes
                    .Where(doc => doc.generic)
                    .Select(doc => new AvailableDocumentsTypeVM
                    {
                        documentname = doc.Name
                    })
                    .ToListAsync();

            return availableDocumentTypes;
        }

        public async Task<List<CustomDocumentsVM>> GetCustomDocumentTypes()
        {

            var userid = GetUserId();
            var documentTypes = await _dataContext.DocumentTypes
       .Where(doc => !doc.deleted && doc.ownerid == userid)
       .Select(doc => new CustomDocumentsVM
       {

           
     Documentname = doc.Name,
     type = doc.type,
     
       
           RenewalPeriod = doc.RenewalPeriod,
      
           DateCreated = doc.DateCreated,
           status = doc.status


       })
       .ToListAsync();

            return documentTypes;

        }

        public async Task<bool> AddCustomDocumentType(AddCustomDocumentTypeVM model)
        {
            try
            {
                var userid = GetUserId();

                // Mapping ViewModel data to Model
                var documentType = new DocumentTypes
                {
                    Category = model.Category,
                    RenewalPeriod = model.RenewalPeriod,
                    Name = model.Name,
                    type = model.type,
                    Description = model.Description,
                    status = true, // Assuming newly created document type is active by default
                    DateCreated = DateTime.Now, // Assuming DateCreated should be set to current datetime
                    ownerid = userid,
                    generic = false
                
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

            throw new NotImplementedException();
        }

        public async Task<bool> AddKYCRequirements(AddKYCRequirementsVM model)
        {

            var userid = GetUserId();
                // Get the DocumentId corresponding to the provided document name
                var documentId = await _dataContext.DocumentTypes
                    .Where(doc => doc.Name == model.documentname)
                    .Select(doc => doc.id)
                    .FirstOrDefaultAsync();

                if (documentId == default(int))
                {
                    // Document with the provided name not found
                    return false;
                }

                // Create a new KYCRequirement object
                var kycRequirement = new KYCRequirements
                {
                    serviceproviderid = userid, // Assuming service provider ID is provided in the model
                    Documentid = documentId,
                    status = true, // Assuming status is provided in the model
                    deleted = false, // Assuming deleted flag is provided in the model
                    DateCreated = DateTime.Now // Assuming current UTC time is used for DateCreated
                };

                // Add the KYCRequirement to the DbContext and save changes
                _dataContext.KYCRequirements.Add(kycRequirement);
                await _dataContext.SaveChangesAsync();

                return true;
            
        }

        public async Task<List<DatabaseListVM>> GetDatabaseList()
        {

            var serviceproviderid = GetUserId();

            var usersWithServiceProvider = await _dataContext.Users
                .Join(
                    _dataContext.AddedServiceProviders,
                    user => user.Id,
                    serviceProvider => serviceProvider.userid,
                    (user, serviceProvider) => new { User = user, ServiceProvider = serviceProvider }
                )
                .Where(joined => joined.ServiceProvider.serviceproviderid == serviceproviderid)
                .Select(joined => new DatabaseListVM
                {
                    userid = joined.User.Id,
                    fullname = joined.User.FullName,
                    Emailaddress = joined.User.Email,
                    ContactNumber = joined.User.PhoneNumber
                })
                .ToListAsync();

            return usersWithServiceProvider;

        }
    }

}
