using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Meeting
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? NameOfResident { get; set; }

    public DateTime? DateOfMeeting { get; set; }

    public string? Guests { get; set; }

    public string? GuestName { get; set; }

    public string? GuestCnic { get; set; }

    public string? GuestRelation { get; set; }

    public short? ResidentConsent { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public short? AtShelter { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public TimeSpan? Duration { get; set; }

    public short? Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
