using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Discharge
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? NameOfResident { get; set; }

    public DateTime? AdmissionDate { get; set; }

    public DateTime? DischargeDate { get; set; }

    public short? SurvivorInformedShelter { get; set; }

    public short? HasPoliceBeenInformed { get; set; }

    public DateTime? PoliceInformedAt { get; set; }

    public string? OriginalPossessionsReturned { get; set; }

    public short? FamilySignedRazinama { get; set; }

    public string? ReasonForLeaving { get; set; }

    public short? GivenResourcesList { get; set; }

    public string? ResidenceAfterDischarge { get; set; }

    public short? ConsentFollowUps { get; set; }

    public string? FrequencyOfFollowUps { get; set; }

    public string? ForwardingAddress { get; set; }

    public string? LevelOfRiskAtDeparture { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }
}
