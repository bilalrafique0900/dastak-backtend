using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public class FilterRequest
{
    public string? City { get; set; }
    public string? Province { get; set; }
    public int? Age { get; set; }
    public string ?Reason { get; set; }
    public string? Nature { get; set; }
    public string? Status { get; set; }
    public string? Category { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
