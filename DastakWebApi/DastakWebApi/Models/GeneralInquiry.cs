using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class GeneralInquiry
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public TimeSpan? Time { get; set; }

    public string? ModeOfInquiry { get; set; }

    public short? Active { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

   
}
