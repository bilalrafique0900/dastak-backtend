using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class ChildOrientation
{
    public int Id { get; set; }

    public string? ChildReferenceNo { get; set; }

    public string? AttendedTraining { get; set; }

    public string? NatureOfTraining { get; set; }

    public short? Vaccinated { get; set; }

    public string? TypeOfVaccination { get; set; }

    public DateTime? NextDateOfVaccination { get; set; }

    public short? IsChildMaleAbove10 { get; set; }

    public string? WhereMaleChildBeenSent { get; set; }
}
