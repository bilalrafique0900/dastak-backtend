using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class ChildSchooling
{
    public int Id { get; set; }

    public string? ChildReferenceNo { get; set; }

    public string? GradeAssigned { get; set; }

    public DateTime? ShelterSchoolEntryDate { get; set; }

    public DateTime? ShelterSchoolLeavingDate { get; set; }

    public string? ImpactOnReadingAbility { get; set; }

    public string? ImpactOnWritingAbility { get; set; }

    public string? ImpactOnMathsAbility { get; set; }

    public string? ImpactOnSocialAbility { get; set; }

    public string? ImpactOnExtraCuricularAbility { get; set; }
}
