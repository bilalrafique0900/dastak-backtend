using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Possession
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Items { get; set; }

    public string? Quantities { get; set; }

    public short? HasSignedAuthorizationLetter { get; set; }

    public string? InPossesstionOf { get; set; }
}
