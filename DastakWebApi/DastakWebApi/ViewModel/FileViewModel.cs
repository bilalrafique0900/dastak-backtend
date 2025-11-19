using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class FileViewModel
    {
        // File Properties
        public int Id { get; set; }
        public string? FileNo { get; set; }
        public string? FileNo2 { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public short? Active { get; set; }

        // Parent Properties
        public int ParentId { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public short? Discharged { get; set; }
        public string? AssessmentRisk { get; set; }
        public short? IsAdmitted { get; set; }
        public short? ParentActive { get; set; } // To distinguish from File.Active

        public string? City { get; set; }
        public string Status { get; set; }
    }

}
