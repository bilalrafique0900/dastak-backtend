using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Visitor
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public DateTime? ToDate { get; set; }

    public TimeSpan? Time { get; set; }

    public string? Name { get; set; }

    public string? Designation { get; set; }

    public string? Organisation { get; set; }

    public string? DetailOfVisitor { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? ContactNo { get; set; }

    public string? ReasonForVisit { get; set; }

    public string? DetailOfVisit { get; set; }

    public int? NoOfPreviousVisits { get; set; }

    public int? NoOfPlannedVisits { get; set; }

    public short? Active { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }
}
