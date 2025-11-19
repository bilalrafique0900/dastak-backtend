using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class AdmissionRecord
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? ReasonForAdmission { get; set; }

    public string? NatureOfAssisstance { get; set; }

    public short? IsAbused { get; set; }

    public DateTime? InterviewDate { get; set; }

    public DateTime? AdmissionDate { get; set; }

    public string? ReasonOfRefuse { get; set; }

    public short? IsReferedToOtherShelter { get; set; }

    public string? WhereHasSheRefered { get; set; }
}
