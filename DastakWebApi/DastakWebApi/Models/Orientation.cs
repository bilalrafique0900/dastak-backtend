using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Orientation
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public short? HasBeenOriented { get; set; }

    public short? GivenCopyOfRules { get; set; }

    public short? EnsuredConfidentialityOfData { get; set; }

    public short? GivenCopyOfRights { get; set; }
}
