using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class BasicView
    {
        public string FileNo { get; set; }
        public string ReferenceNo { get; set; }

        public BasicInfo BasicInfo { get; set; }
        public MaritalInfo MaritalInfo { get; set; }
        public ReferencesRecord ReferencesRecord { get; set; }
        public AdmissionRecord AdmissionRecord { get; set; }
        public ContactsInfo ContactsInfo { get; set; }
        public Document Document { get; set; }
        public Possession Possession { get; set; }
        public CommunicableDisease CommunicableDisease { get; set; }
        public Orientation Orientation { get; set; }
        public AdditionalDetail AdditionalDetail { get; set; }
    }



}
