using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class MaritalInfo
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? MaritalStatus { get; set; }

    public DateTime? SeparatedSince { get; set; }

    public string? MaritalCategory { get; set; }

    public string? MaritalType { get; set; }

    public string? WifeOf { get; set; }

    public short? PartnerAbusedInDrug { get; set; }

    public string? ProofOfMarriage { get; set; }

    public short? HaveChildren { get; set; }

    public int? TotalChildren { get; set; }

    public string? AccompanyingChildrenName { get; set; }

    public string? AccompanyingChildrenAge { get; set; }

    public string? AccompanyingChildrenRelation { get; set; }

    public short? CurrentlyExpecting { get; set; }

    public DateTime? ExpectedDeliveryDate { get; set; }
    public int? AgeOfMarriage { get; set; }
}
