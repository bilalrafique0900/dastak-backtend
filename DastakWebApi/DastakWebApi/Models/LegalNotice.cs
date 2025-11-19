using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class LegalNotice
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? CaseId { get; set; }

    public short? LegalAdviceSought { get; set; }

    public short? LegalAssistanceSought { get; set; }

    public short? LegalNoticeSent { get; set; }

    public DateTime? DateWhenLegalNoticeSent { get; set; }

    public string? LegalNoticeSentTo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public short? Active { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
