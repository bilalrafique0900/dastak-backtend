using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class DastakVisit
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? ObjectiveOfVisit { get; set; }

    public string? DetailOfVisit { get; set; }

    public int? NumberOfPlannedVisits { get; set; }

    public int? NumberOfPreviousVisits { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public short? Active { get; set; }
}
