using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{


    public class MeetingsViewModel
    {
        public string ReferenceNo { get; set; }
        public string? NameOfResident { get; set; }
        public DateTime? DateOfMeeting { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public short AtShelter { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? GuestNames { get; set; }
        public string? GuestCnics { get; set; }
        public string? GuestRelations { get; set; }
    }



}



