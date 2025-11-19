using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class CommunityConsultation
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }
    

    public string? Name { get; set; }

   

    public string? CaseId { get; set; }

    public string? CaseRef { get; set; }

    public short? LegalAdviceSought { get; set; }

    public short? LegalAssistanceSought { get; set; }

    public short? LegalNoticeSent { get; set; }

    public DateTime? DateWhenLegalNoticeSent { get; set; }

    public string? LegalNoticeSentTo { get; set; }

    public string? TypeOfAssistance { get; set; }

    public string? ReasonForWithdrawal { get; set; }

    public string? NatureOfLegalConcern { get; set; }

    public string? FirNo { get; set; }

    public string? CaseNo { get; set; }

    public string? CaseFiledBy { get; set; }

    public string? CaseFiledAgainst { get; set; }

    public short? IsLawyerShelterAssigned { get; set; }

    public string? NameOfLawyer { get; set; }

    public string? ContactOfLawyer { get; set; }

    public string? Court { get; set; }

    public string? ProvinceOfCourt { get; set; }

    public string? CityOfCourt { get; set; }

    public DateTime? NextDateOfHearing { get; set; }

    public string? Remarks { get; set; }
    public string? Outcome { get; set; }

    public string? StatusOfCase { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public short? Active { get; set; }
}
