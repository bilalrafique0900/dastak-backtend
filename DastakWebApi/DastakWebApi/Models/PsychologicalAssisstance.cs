using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class PsychologicalAssisstance
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? CaseId { get; set; }

    public string? NameOfResident { get; set; }

    public string? NatureOfAssistance { get; set; }
    public string? PsychologicalAssistanceProvidedTo { get; set; }

    public string? WhatArrangementsMadeForImmidiateAssisstance { get; set; }

    public string? PsychologicalAssessment { get; set; }

    public int? Age { get; set; }

    public DateTime? SoughtAt { get; set; }

    public DateTime? ProvidedAt { get; set; }

    public string? NameOfConsultant { get; set; }

    public string? LocationOfConsultant { get; set; }

    public string? Contact { get; set; }

    public DateTime? ConductedAt { get; set; }

    public TimeSpan? StartedAt { get; set; }

    public TimeSpan? EndedAt { get; set; }

    public string? Notes { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }
}
