using System.ComponentModel.DataAnnotations;

namespace KYC_APP.Data.ViewModels
{
    public class FileUploadVM
    {
       
        public string filepath { get; set; }
      
        public IFormFile file { get; set; }
        public string DocumentType { get; set; }

      

    }
}
