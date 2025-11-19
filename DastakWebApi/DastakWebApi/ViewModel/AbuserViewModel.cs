using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class AbuserViewModel
    {
        public string ReferenceNo { get; set; }
        public string ResidentName { get; set; }
        public string AbuserName { get; set; }
        public string FatherName { get; set; }
        public string[] TypeOfAbuse { get; set; }
        public string[] ReasonOfInflictingAbuse { get; set; }
        public string[] ReasonForToleratingAbuse { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Contact { get; set; }
        public string Relationship { get; set; }
        public string RelationDuration { get; set; }
        public string Qualification { get; set; }
        public string Profession { get; set; }
        public string[] TypeOfEconomicAbuse { get; set; }
    }
}
