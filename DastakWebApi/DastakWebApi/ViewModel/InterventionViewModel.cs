using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class InterventionViewModel
    {
        public string FileNo { get; set; }
        public string ReferenceNo { get; set; }
        public string FullName { get; set; }
    }

    public class InterventionFormModel
    {
        public string FileNo { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime InterventionDate { get; set; }
        public List<InterventionDetailModel> DetailsOfIntervention { get; set; }
    }

    public class InterventionDetailModel
    {
        public string NatureOfIntervention { get; set; }
        public string Detail { get; set; }
        public string AdditionalDetails { get; set; }
        public string Complications { get; set; }
        public string AdditionalComplications { get; set; }
        public string Outcome { get; set; }
    }

}
