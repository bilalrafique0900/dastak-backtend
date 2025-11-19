using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class DischargeViewModel
    {
        // Reference and Resident Information
        public string?  entity { get; set; }
        public string? file { get; set; }
        public string? ReferenceNo { get; set; }
        public string? NameOfResident { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }

        // Discharge Information
        public short? SurvivorInformedShelter { get; set; }
        public short? HasPoliceBeenInformed { get; set; }
        public DateTime? PoliceInformedAt { get; set; }
        public string? OriginalPossessionsReturned { get; set; }
        public short? FamilySignedRazinama { get; set; }
        public string? ReasonForLeaving { get; set; }
        public string? ResidenceAfterDischarge { get; set; }
        public short? ConsentFollowUps { get; set; }
        public string? LevelOfRiskAtDeparture { get; set; }
        public string? ForwardingAddress { get; set; }
        public string? FrequencyOfFollowUps { get; set; }
        public short? GivenResourcesList { get; set; }

        // Feedback Information
        public string? OverAllExperience { get; set; }
        public string? SecurityArrangements { get; set; }
        public string? ProvisionOfFood { get; set; }
        public string? ProvisionOfClothingAndAccessories { get; set; }
        public string? MedicalOrPsychologicalFacilities { get; set; }
        public string? ProvisionOfLegalAssistance { get; set; }
        public string? ProvisionForFamilyMeetings { get; set; }
        public string? CrisisManagementAndAttitude { get; set; }
        public string? ServicesProvidedToHerChildren { get; set; }
        public string? AwarenessProgramsAndWorkshop { get; set; }
        public short? HasSuggestionsOrComplaints { get; set; }
        public string? SuggestionsOrComplaints { get; set; }
        public short? RightsWereRespected { get; set; }
        public short? PrivacyEnsuredDuringMeeting { get; set; }
        public string? PracticesKeepingChildrenSafe { get; set; }
        public short? GivenOpportunitiesOfParticipating { get; set; }

        // Metadata
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public short? Active { get; set; }
    }


}
