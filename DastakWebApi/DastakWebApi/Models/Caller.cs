using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Caller
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public TimeSpan? Time { get; set; }

    public string? Name { get; set; }

    public string? Designation { get; set; }

    public string? Organisation { get; set; }

    public string? DetailOfCaller { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? ContactNo { get; set; }

    public string? ReasonForCall { get; set; }

    public string? DetailOfCall { get; set; }

    public int? NoOfPreviousCalls { get; set; }

    public int? NoOfPlannedCalls { get; set; }

    public short? Active { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Outcome { get; set; }
}
