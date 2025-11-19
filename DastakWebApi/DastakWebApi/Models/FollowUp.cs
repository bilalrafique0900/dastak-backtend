using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class FollowUp
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? NameOfResident { get; set; }

    public string? FileNo { get; set; }

    public string? ContactNo { get; set; }

    public DateTime? FollowupDate { get; set; }

    public DateTime? DischargeDate { get; set; }

    public string? CurrentResidence { get; set; }

    public string? StatusOfOriginalConcern { get; set; }

    public string? BehaviourOfFamilyTowardsHer { get; set; }

    public short? CurrentlyEmployed { get; set; }

    public short? RecommendedSomeoneElseToShelter { get; set; }

    public short? ConsentToFurtherFollowup { get; set; }

    public string? FrequencyOfFollowUps { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }
}
