using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class DocumentViewModel
    {
        
        public EntityViewModel Data { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
    }

    public class DocumentUploadModel
    {

        public string? ReferenceNo { get; set; }
        public string? Name { get; set; }
        public string? Detail { get; set; }
    }
}
