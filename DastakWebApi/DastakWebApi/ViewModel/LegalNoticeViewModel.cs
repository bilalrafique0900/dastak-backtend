using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class LegalNoticeViewModel
    {
        public string ReferenceNo { get; set; }
        public short? LegalAdviceSought { get; set; }
        public short? LegalAssistanceSought { get; set; }
        public short? LegalNoticeSent { get; set; }
        public DateTime? DateWhenLegalNoticeSent { get; set; }
        public string LegalNoticeSentTo { get; set; }
        public string TypeOfAssistance { get; set; }
        public string ReasonForWithdrawal { get; set; }
        public string NatureOfLegalConcern { get; set; }
        public string FirNo { get; set; }
        public string CaseNo { get; set; }
        public string CaseFiledBy { get; set; }
        public string CaseFiledAgainst { get; set; }
        public short? IsLawyerShelterAssigned { get; set; }
        public string NameOfLawyer { get; set; }
        public string ContactOfLawyer { get; set; }
        public string Court { get; set; }
        public string ProvinceOfCourt { get; set; }
        public string CityOfCourt { get; set; }
        public DateTime? NextDateOfHearing { get; set; }
        public string Remarks { get; set; }
        public string StatusOfCase { get; set; }
    }

}
