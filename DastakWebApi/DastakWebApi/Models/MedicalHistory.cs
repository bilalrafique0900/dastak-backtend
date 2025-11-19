using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class MedicalHistory
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? CaseId { get; set; }

    public string? NameOfResident { get; set; }

    public int? Age { get; set; }

    public string? NameOfDoctor { get; set; }

    public string? AddressOfDoctor { get; set; }

    public string? Address2OfDoctor { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? ContactOfDoctor { get; set; }

    public string? BriefOfHistory { get; set; }

    public string? NatureOfChronicIllness { get; set; }

    public string? SubstancesInDrugAbused { get; set; }

    public string? IntensityOfAbuse { get; set; }

    public short? IsCurrentlySubstanceAbuser { get; set; }

    public string? IntensityOfCurrentAbuse { get; set; }

    public string? CurrentMedicalPrescription { get; set; }

    public DateTime? ExpectedDeliveryDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }
}
