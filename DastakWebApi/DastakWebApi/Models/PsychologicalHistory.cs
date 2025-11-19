using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class PsychologicalHistory
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? CaseId { get; set; }

    public string? NameOfResident { get; set; }

    public int? Age { get; set; }

    public string? PsychologicalAssessment { get; set; }

    public short? HasSufferedVerbalAbuse { get; set; }

    public string? TypeOfVerbalAbuse { get; set; }

    public short? HasBeenThreatened { get; set; }

    public string? NatureOfThreat { get; set; }

    public string? WhatArrangementsMadeForImmidiateAssisstance { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }
}
