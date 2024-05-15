using Azure.Core;
using KYC_APP.Data.ViewModels;
using KYC_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static KYC_APP.Data.Services.FileManagerService;
using System.Security.Claims;
using File = KYC_APP.Models.File;
using KYC_APP.Data.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace KYC_APP.Data.Services
{
    public class FileManagerService : IFileManagerService
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpcontext;
        
           

        public FileManagerService(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, DataContext dataContext, IHttpContextAccessor httpContext)
        {
            _environment = environment;
            _dataContext = dataContext;
            _httpcontext = httpContext;
        }

        public string GetUserId()
        {
            return _httpcontext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        }

        public async Task<List<AvailableDocumentsVM>> GetFilePathsPerCategory()
        {
            var userid = GetUserId(); // Assuming you have a method to get the user ID

            var latestFilesPerCategory =  _dataContext.Files
                .Where(f => f.userid == userid && !f.deleted) // Filter files by userid and not deleted
                .GroupBy(f => f.DocumentType) // Group files by DocumentType (category)
                .Select(group => group.OrderByDescending(f => f.DateAdded).FirstOrDefault()) // Select the latest file for each category
                .ToList();

            var availableDocuments = latestFilesPerCategory.Select(file => new AvailableDocumentsVM
            {
                filepath = file.filepath,
                Name = file.name,
               
                Documenttype = file.DocumentType,
                Type = file.type, // Assuming you have a property called 'type' in your File entity
                Renewaldate = file.Renewaldate.ToString() // Convert Renewaldate to string
            }).ToList();

            return availableDocuments; // Wrap the result in Task
        }

        public async Task<bool> SaveFiles(UploadKYCVM model)
        {
            var filepath = await UploadFile(model.File);
            var userid = GetUserId();

        
            if (filepath != null)
            {


                var documentType = _dataContext.DocumentTypes.FirstOrDefault(d => d.Name == model.DocumentType);

                if (documentType != null)
                {
                    // Get renewal period and calculate renewal date
                    var renewalPeriod = ParseRenewalPeriod(documentType.RenewalPeriod);
                    var renewalDate = DateTime.Now.AddMonths((int)renewalPeriod);
                    File fileUpload = new File()
                    {

                        filepath = filepath,
                        name = model.File.Name,
                        DocumentType = model.DocumentType,
                        userid = userid,
                        status = true,
                        deleted = false,
                        DateAdded = DateTime.Now,
                        Renewaldate = renewalDate,


                        // Assuming you have other properties to assign
                    };

                    _dataContext.Files.Add(fileUpload);
                    await _dataContext.SaveChangesAsync();

                    return true; // Assuming successful save
                }
            }

            return false; // Failed to upload file

        }

        public async Task<string> UploadFile(IFormFile file)
        {
            

            // Ensure that the file is not null
            if (file != null)
            {
                // Construct the file path using the hosting environment
                string filePath = Path.Combine(_environment.WebRootPath, "KYCDocuments", file.FileName);

                // Copy the uploaded file to the specified file path
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return filePath;

                // Now you can access both DocumentType and the uploaded file
                // You can process further as needed
            }
            else
            {
                // Handle the case where the file is null
                string error = "file is null";
                return error;
            }

        }


        // Helper method to parse renewal period enum value to months
        private RenewalPeriod ParseRenewalPeriod(string renewalPeriod)
            {
                switch (renewalPeriod)
                {
                    case "ThreeMonths":
                        return RenewalPeriod.ThreeMonths;
                    case "SixMonths":
                        return RenewalPeriod.SixMonths;
                    case "OneYear":
                        return RenewalPeriod.OneYear;
                    case "ThreeYears":
                        return RenewalPeriod.ThreeYears;
                    case "FiveYears":
                        return RenewalPeriod.FiveYears;
                    default:
                        return RenewalPeriod.OneYear; // Default to 1 year if renewal period is not recognized
                }
            }


        }
}
