using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace KYC_APP.Data.ViewModels
{
    public class UploadKYCVM
    {
       
        public UploadKYCVM()
        {
        }

        public string DocumentType { get; set; }
         public IFormFile File { get; set; }


    }
}
