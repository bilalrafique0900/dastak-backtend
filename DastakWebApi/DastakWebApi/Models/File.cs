using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class File
{
    public int Id { get; set; }

    public string? FileNo { get; set; }

    public string? FileNo2 { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short Active { get; set; }
}
