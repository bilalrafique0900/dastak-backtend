using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class ContactsInfo
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? Phone { get; set; }

    public string? Phone2 { get; set; }

    public short? ConcentToInformFamily { get; set; }

    public string? FamilyPhone { get; set; }

    public string? FamilyName { get; set; }

    public string? FamilyRelation { get; set; }

    public string? EmergencyPhone { get; set; }

    public string? EmergencyName { get; set; }

    public string? EmergencyRelation { get; set; }
}
