using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class PsychologicalTherapySession
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? CaseId { get; set; }

    public string? NameOfResident { get; set; }

    public DateTime? ConductedAt { get; set; }

    public TimeSpan? LengthOfSession { get; set; }

    public TimeSpan? StartedAt { get; set; }

    public TimeSpan? EndedAt { get; set; }

    public string? PsychologicalAssessMentalHealth { get; set; }

    public string? Complaint { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }
}
