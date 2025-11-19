using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class CommunicableDisease
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public short? HasScreened { get; set; }

    public string? Diseases { get; set; }
}
