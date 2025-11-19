using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class EditViewModel
    {
        public EntityViewModel Data { get; set; }
        public string UserEmail { get; set; }
    }

    public class EntityViewModel
    {
        public string FileNo { get; set; }
        public string ReferenceNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ChildViewModel ChildData { get; set; }
    }

    public class ChildViewModel
    {
        public string? MotherName { get; set; }
        public string? Name { get; set; }
        public string? Age { get; set; }
        public string ?Gender { get; set; }
        public DateTime? DischargeDate { get; set; }
        public short? HasBeenReferred { get; set; }
        public string? WhereHasBeenReferred { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public HealthViewModel Health { get; set; }
        public SchoolingViewModel Schooling { get; set; }
        public OrientationViewModel Orientation { get; set; }
    }

    public class HealthViewModel
    {
        public string? Hygiene { get; set; }
        public short? SoughtMedicalTreatment { get; set; }
        public short? UnderPhysicalViolence { get; set; }
        public short? RequireMedicalOrPsychologicalAssistance { get; set; }
        public short? SpecialChild { get; set; }
        public string? Residence { get; set; }
    }

    public class SchoolingViewModel
    {
        public string? GradeAssigned { get; set; }
        public DateTime? ShelterSchoolEntryDate { get; set; }
        public DateTime? ShelterSchoolLeavingDate { get; set; }
        public string? ImpactOnReadingAbility { get; set; }
        public string? ImpactOnWritingAbility { get; set; }
        public string? ImpactOnMathsAbility { get; set; }
        public string? ImpactOnSocialAbility { get; set; }
        public string? ImpactOnExtraCuricularAbility { get; set; }
    }

    public class OrientationViewModel
    {
        public string? AttendedTraining { get; set; }
        public string? NatureOfTraining { get; set; }
        public short? Vaccinated { get; set; }
        public string? TypeOfVaccination { get; set; }
        public DateTime? NextDateOfVaccination { get; set; }
        public short? IsChildMaleAbove10 { get; set; }
        public string? WhereMaleChildBeenSent { get; set; }
    }



}
