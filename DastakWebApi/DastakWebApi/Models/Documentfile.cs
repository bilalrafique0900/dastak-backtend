using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Documentfile
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Detail { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public short? Active { get; set; }
}
