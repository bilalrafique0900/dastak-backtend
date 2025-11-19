using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class ChildMedicalAssistance
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }
    public int MedicalAssistanceId { get; set; }
    public string? Name { get; set; }

    public string? Age { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

}
