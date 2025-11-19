using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class BasicInfo
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int? Age { get; set; }

    public string? Religion { get; set; }

    public string? BirthReligion { get; set; }

    public short? IsConvert { get; set; }

    public string? FatherName { get; set; }

    public string? FatherLivingStatus { get; set; }

    public string? MotherName { get; set; }

    public string? MotherLivingStatus { get; set; }

    public string? GuardianName { get; set; }

    public string? GuardianRelation { get; set; }

    public string? Ethinicity { get; set; }

    public string? Nationality { get; set; }

    public string? Cnic { get; set; }

    public string? PassportNo { get; set; }

    public string? DomicileCity { get; set; }

    public string? DomicileProvince { get; set; }

    public string? Gender { get; set; }

    public string? LiteracyLevel { get; set; }

    public string? Phone { get; set; }

    public string? Phone2 { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }
}
