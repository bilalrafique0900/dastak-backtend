using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class EntityModel
    {
        public string ReferenceNo { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class MeetingModel
    {
        public int Id { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime MeetingDate { get; set; } // Example field, replace with actual fields in your DB
        public string MeetingLocation { get; set; } // Example field
    }

}
