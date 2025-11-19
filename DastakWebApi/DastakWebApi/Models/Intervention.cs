using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Intervention
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? NatureOfIntervention { get; set; }

    public string? DetailOfIntervention { get; set; }

    public string? AdditionalDetailsOfIntervention { get; set; }

    public string? Complications { get; set; }

    public string? AdditionalDetailsOfComplications { get; set; }

    public string? Outcome { get; set; }

    public short? Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? InterventionDate { get; set; }
}
