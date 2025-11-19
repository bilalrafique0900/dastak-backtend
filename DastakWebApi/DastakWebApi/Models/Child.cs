using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Child
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? ChildReferenceNo { get; set; }

    public string? MotherName { get; set; }

    public string? ChildReferenceNo2 { get; set; }

    public string? Name { get; set; }

    public string? Age { get; set; }

    public string? Gender { get; set; }

    public DateTime? DischargeDate { get; set; }

    public short? HasBeenReferred { get; set; }

    public string? WhereHasBeenReferred { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }
}
