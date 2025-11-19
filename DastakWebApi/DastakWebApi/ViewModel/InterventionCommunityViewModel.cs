using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class InterventionCommunityViewModel
    {
        public string? FileNo { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Name { get; set; }
    }

    public class InterventionCommunityFormModel
    {
        public int? Id { get; set; }
        public string? FileNo { get; set; }
        public string? Name { get; set; }
        public string? ReferenceNo { get; set; }
        public DateTime? InterventionDate { get; set; }
        public List<InterventionDetailCommunityModel> DetailsOfIntervention { get; set; }
    }

    public class InterventionDetailCommunityModel
    {
        public string? NatureOfIntervention { get; set; }
        public string? Detail { get; set; }
        public string? AdditionalDetails { get; set; }
        public string? Complications { get; set; }
        public string? AdditionalComplications { get; set; }
        public string? Outcome { get; set; }
    }

}
