using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class MedicalDataViewModel
    {
        public string? Entity { get; set; }
        public short? Discharged { get; set; } // Assuming Discharged is a nullable boolean
        public string? File { get; set; }
        public List<MedicalViewModel> Medical { get; set; }
    }

    public class MedicalViewModel
    {
        public int? Id { get; set; }
        public string? CaseId { get; set; }
        public string? Name { get; set; }
        public string? Contact { get; set; }
        public string? Brief { get; set; }
        public DateTime? CreatedAt { get; set; }
    }



}
