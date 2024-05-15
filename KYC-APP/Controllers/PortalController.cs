using KYC_APP.Data;
using KYC_APP.Data.Services;
using KYC_APP.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace KYC_APP.Controllers
{
    public class PortalController : Controller
    {
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IFileManagerService _fileservice;
        private readonly Data.Services.IServiceProvider _serviceproviderservice;

        public PortalController(IDocumentTypeService documentTypeService, IFileManagerService fileManagerService, Data.Services.IServiceProvider serviceproviderservice)
        {
            _documentTypeService = documentTypeService;
            _fileservice = fileManagerService;
            _serviceproviderservice = serviceproviderservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        public async Task<IActionResult> DocumentTypeManagerAsync()
        {
            // Retrieve the list of documents from the service


            var documentTypesList = await _documentTypeService.GetDocumentTypesAsync();

            // Create an instance of the combined view model
            var viewModel = new DocumentTypeViewModel
            {
                DocumentTypesVM = new DocumentTypesVM(), // You can initialize this with appropriate values if needed
                DocumentTypesList = documentTypesList
            };

            return View(viewModel);
        }
        public async Task<IActionResult> DocumentManager()
        {

            var model = await _documentTypeService.GetDocumentRequirementsAsync();

            var availabledocuments = await _fileservice.GetFilePathsPerCategory();


            //Get filepath of latest file per category from db 
            //put into model and pass to view 

            var viewModel = new DocumentManagerViewModel
            {
                UploadKYCVM = new UploadKYCVM(),
                KYCRequirementsVM = model,
                availableDocumentsVM = availabledocuments
                

            };

            // Pass the view model to the view
            return View(viewModel);

           
        }

       

        public async Task<IActionResult> AddServiceProvider(string id)
        {
            var isCreated = await _serviceproviderservice.AddServiceProvider(id);
            if (isCreated)
            {
                TempData["ServiceProviderAdded"] = "success";
                return RedirectToAction("ProviderManager");
            }
            else
            {
                TempData["ServiceProviderAdded"] = "failure";
                return RedirectToAction("ProviderManager");
            }
        }

        [HttpPost]

        public async Task<IActionResult> SaveDocument (UploadKYCVM model, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {

            //// Check if model state is valid
            //if (!ModelState.IsValid)
            //{
            //    // Handle invalid model state, return error response or redirect to another view
            //    return BadRequest(ModelState);
            //}

            //// Ensure that the file is not null
            //if (model.File != null)
            //{
            //    // Construct the file path using the hosting environment
            //    string filePath = Path.Combine(hostingEnvironment.WebRootPath, "KYCDocuments", model.File.FileName);

            //    // Copy the uploaded file to the specified file path
            //    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        model.File.CopyTo(fileStream);
            //    }

            //    // Now you can access both DocumentType and the uploaded file
            //    // You can process further as needed
            //}
            //else
            //{
            //    // Handle the case where the file is null
            //    return BadRequest("No file uploaded.");
            //}

            //// Optionally, redirect to another action or return a success response
            //return RedirectToAction("Index");




            var filesaved = await _fileservice.SaveFiles(model);
            if (filesaved != null)
            {

                TempData["Status"] = "success";
                return RedirectToAction("DocumentManager");
            }
            else
            {
                TempData["Status"] = "failure";
                return RedirectToAction("DocumentManager");
            }


        }

        public async Task<IActionResult> ProviderManager()
        {
            var availableserviceproviders = await _serviceproviderservice.GetServiceProvidersAvailable();
            var addedserviceproviders = await _serviceproviderservice.GetServiceProvidersAdded();

            ProviderManagerViewModel providerManagerViewModel = new ProviderManagerViewModel
            {
ServiceProvidersAdded = addedserviceproviders,
ServiceProvidersAvailable = availableserviceproviders
            };

            return View(providerManagerViewModel);
        }

        public async Task<IActionResult> DatabaseManager()
        {

            var kycrequirments = await _serviceproviderservice.GetKYCRequirements();
            var getavailabledocumenttypes = await _serviceproviderservice.GetKYCDocumentsTypesAvailable();
            var getcustomdocumenttypes = await _serviceproviderservice.GetCustomDocumentTypes();
            var databaselist = await _serviceproviderservice.GetDatabaseList();

            DatabaseManagerViewModel databaseManagerViewModel = new DatabaseManagerViewModel
            {
               RequirementsVM = kycrequirments,
                AvailableDocumentsTypes = getavailabledocumenttypes,
                CustomDocumentTypes = getcustomdocumenttypes,
               AddCustomDocumentTypeVM = new AddCustomDocumentTypeVM(),
              AddKYCRequirementsVM = new AddKYCRequirementsVM(),
              DatabaseListVM = databaselist
              

            };

            return View(databaseManagerViewModel);
           
        }

        public IActionResult UserManager()
        {
            return View();
        }

        

        public async Task<IActionResult> AddKYCRequirement(AddKYCRequirementsVM model)
        {

            bool isDocumentTypeCreated = await _serviceproviderservice.AddKYCRequirements(model);
            if (isDocumentTypeCreated)
            {
                TempData["Status"] = "success";
            }
            else
            {
                TempData["Status"] = "failure";
            }



            return RedirectToAction("DatabaseManager"); ;
        }

        public async Task<IActionResult> CreateCustomDocumentTypes(AddCustomDocumentTypeVM model)
        {

            bool isDocumentTypeCreated = await _serviceproviderservice.AddCustomDocumentType(model);
            if (isDocumentTypeCreated)
            {
                TempData["Status"] = "success";
            }
            else
            {
                TempData["Status"] = "failure";
            }



            return RedirectToAction("DatabaseManager"); ;
        }

       

        [HttpPost]

        public async Task<IActionResult> CreateDocumentType(DocumentTypeViewModel model)
        {

            bool isDocumentTypeCreated = await _documentTypeService.CreateDocumentType(model.DocumentTypesVM);
            if (isDocumentTypeCreated)
            {
                TempData["Status"] = "success";
            }
            else
            {
                TempData["Status"] = "failure";
            }



            return  RedirectToAction("DocumentTypeManager"); ;
        }
    }
}
