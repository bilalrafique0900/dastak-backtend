using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class MedicalAssisstance
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? CaseId { get; set; }

    public string? NameOfResident { get; set; }

    public int? Age { get; set; }

    public DateTime? DateWhenSought { get; set; }

    public string? NameOfDoctorAssisting { get; set; }

    public string? NameOfClinicAssisting { get; set; }
    public string? MedicalAssistanceProvidedTo { get; set; }

    public string? Complaint { get; set; }

    public string? Diagnosis { get; set; }

    public string? TreatmentSuggested { get; set; }

    public string? ShelterAgreedToConductTests { get; set; }

    public string? DetailOfTest { get; set; }

    public string? Notes { get; set; }

    public string? ContactNo { get; set; }

    public string? ContactNo2 { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }
}
