using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? NameOfResident { get; set; }

    public string? OverAllExperience { get; set; }

    public string? SecurityArrangements { get; set; }

    public string? ProvisionOfFood { get; set; }

    public string? ProvisionOfClothingAndAccessories { get; set; }

    public string? MedicalOrPsychologicalFacilities { get; set; }

    public string? ProvisionOfLegalAssisstance { get; set; }

    public string? ProvisionForFamilyMeetings { get; set; }

    public string? CrisisManagementAndAttitude { get; set; }

    public string? ServicesProvidedToHerChildren { get; set; }

    public short? RightsWereRespected { get; set; }

    public short? GivenOpportunitiesOfParticipating { get; set; }

    public string? PracticesKeepingChildrenSafe { get; set; }

    public short? PrivacyEnsuredDuringMeeting { get; set; }

    public string? AwarenessProgramsAndWorkshop { get; set; }

    public short? HasSuggestionsOrComplaints { get; set; }

    public string? SuggestionsOrComplaints { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }
}
