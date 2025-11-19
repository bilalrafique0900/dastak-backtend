using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Parent
{
    public int Id { get; set; }

    public string? FileNo { get; set; }

    public string? ReferenceNo { get; set; }

    public string? ReferenceNo2 { get; set; }

    public string? Title { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public short? Pending { get; set; }

    public string? DiscardedBy { get; set; }

    public short? IsAdmitted { get; set; }

    public DateTime? AdmissionAt { get; set; }

    public short? IsReadmission { get; set; }

    public string? AssessmentRisk { get; set; }

    public string? ResidenceBeforeReadmission { get; set; }

    public short? Discharged { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }

    public string? EnsurePrivacy { get; set; }
}
