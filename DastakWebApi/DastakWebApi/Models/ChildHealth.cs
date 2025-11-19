using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class ChildHealth
{
    public int Id { get; set; }

    public string? ChildReferenceNo { get; set; }

    public string? Hygiene { get; set; }

    public short? SoughtMedicalTreatment { get; set; }

    public short? UnderPhysicalVoilence { get; set; }

    public short? UnderSexualVoilence { get; set; }

    public short? RequireMedicalOrPsychologicalAssisstance { get; set; }

    public short? SpecialChild { get; set; }

    public string? Residence { get; set; }
}
