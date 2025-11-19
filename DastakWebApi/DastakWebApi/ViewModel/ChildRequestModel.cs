using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class ChildRequestModel
    {
        // Child-related properties
        public string? ReferenceNo { get; set; }
        public string? ChildReferenceNo { get; set; }
        public string? ChildReferenceNo2 { get; set; }
        public string? Name { get; set; }
        public string? MotherName { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
        public DateTime? DischargeDate { get; set; }
        public short? HasBeenReferred { get; set; }
        public string? WhereHasBeenReferred { get; set; }

        // Health-related properties
        public string? Hygiene { get; set; }
        public short? SoughtMedicalTreatment { get; set; }
        public short? UnderPhysicalViolence { get; set; }
        public short? RequireMedicalOrPsychologicalAssistance { get; set; }
        public short? SpecialChild { get; set; }
        public string? Residence { get; set; }

        // Schooling-related properties
        public string? GradeAssigned { get; set; }
        public DateTime? ShelterSchoolEntryDate { get; set; }
        public DateTime? ShelterSchoolLeavingDate { get; set; }
        public string? ImpactOnReadingAbility { get; set; }
        public string? ImpactOnWritingAbility { get; set; }
        public string? ImpactOnMathsAbility { get; set; }
        public string? ImpactOnSocialAbility { get; set; }
        public string? ImpactOnExtraCurricularAbility { get; set; }

        // Orientation-related properties
        public string? AttendedTraining { get; set; }
        public string? NatureOfTraining { get; set; }
        public short? Vaccinated { get; set; }
        public string? TypeOfVaccination { get; set; }
        public DateTime? NextDateOfVaccination { get; set; }
        public short? IsChildMaleAbove10 { get; set; }
        public string? WhereMaleChildBeenSent { get; set; }

        // Other View-specific properties
        public string? FileNo { get; set; }
        public string? ReferenceNoEntity { get; set; }
    }


}
