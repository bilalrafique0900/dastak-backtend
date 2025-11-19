using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class ProceedAdmissionViewModel
    {
        public string? FileNo { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public bool IsReadmission { get; set; }
        public bool? IsAdmitted { get; set; }
        public string? AdmissionDate { get; set; }
        public string? ReasonForAdmission { get; set; }
        public string? NatureOfAssistance { get; set; }
        public short? IsAbused { get; set; }
        public string? InterviewDate { get; set; }
        public string? ExpectedDeliveryDate { get; set; }
        public string? ReasonOfRefuse { get; set; }
        public short? IsReferredToOtherShelter { get; set; }
        public string? WhereHasSheReferred { get; set; }
        public string? FamilyPhone { get; set; }
        public string? FamilyName { get; set; }
        public string? FamilyRelation { get; set; }
        public string? EmergencyRelation { get; set; }
        public string? EmergencyPhone { get; set; }
        public string? EmergencyName { get; set; }
        public string? ListOfDocuments { get; set; }
        public short? Photocopied { get; set; }
        public short? HasScreened { get; set; }
        public string? Diseases { get; set; }
        public bool? HasBeenOriented { get; set; }
        public bool? GivenCopyOfRules { get; set; }
        public bool? GivenCopyOfRights { get; set; }
        public bool? EnsuredConfidentialityOfData { get; set; }
        public string? Items { get; set; }
        public string? Quantities { get; set; }
        public string? InPossessionOf { get; set; }
        public short? HasSignedAuthorizationLetter { get; set; }
        public string? Details { get; set; }
    }
}
