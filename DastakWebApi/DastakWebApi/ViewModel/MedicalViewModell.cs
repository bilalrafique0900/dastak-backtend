using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class MedicalViewModell
    {
        // Properties related to History
        public string? ReferenceNo { get; set; }
        public string? NameOfResident { get; set; }
        public string? BriefOfHistory { get; set; }
        public string? NatureOfChronicIllness { get; set; }
        public string? SubstancesInDrugAbused { get; set; }
        public string? IntensityOfAbuse { get; set; }
        public short? IsCurrentlySubstanceAbuser { get; set; }
        public string? IntensityOfCurrentAbuse { get; set; }
        public string? CurrentMedicalPrescription { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }

        // Properties related to Assistance
        public DateTime? DateWhenSought { get; set; }
        public string? NameOfDoctorAssisting { get; set; }
        public string? NameOfClinicAssisting { get; set; }
        public string? Complaint { get; set; }
        public string? Diagnosis { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string?ContactNo2 { get; set; }
        public string? TreatmentSuggested { get; set; }
        public string? Notes { get; set; }
        public string? ShelterAgreedToConductTests { get; set; }
        public string? DetailOfTest { get; set; }

        // Additional fields for the form
        public string? CaseId { get; set; }
        // Properties related to childs
        public string? Name { get; set; }
        public string? Age { get; set; }
        // Properties related to marital
        public string? MedicalAssistanceProvidedTo { get; set; }
        public List<ChildMedicalAssistance>? ChildMedicalAssistances { get; set; }
    }


}
