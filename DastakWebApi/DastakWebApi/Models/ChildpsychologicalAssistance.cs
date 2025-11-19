using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class ChildpsychologicalAssistance
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }
    public int PsychologicalAssistanceId { get; set; }
    public string? Name { get; set; }

    public string? ChildAge { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

}
