using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class ReferencesRecord
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public short? IsReferencial { get; set; }

    public string? ReferencialDetails { get; set; }

    public string? TypeOfReference { get; set; }

    public string? ReferencialName { get; set; }

    public string? ReferencialCity { get; set; }
}
