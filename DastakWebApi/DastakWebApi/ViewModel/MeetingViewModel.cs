using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{


    public class MeetingViewModel
    {
        public string? Entity { get; set; }
        public short? Discharged { get; set; }
        public string? File { get; set; }
        public List<GuestMeeting>? Guests { get; set; } = new List<GuestMeeting>();
    }

    public class GuestMeeting
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfMeeting { get; set; }
        public string? GuestNames { get; set; }
        public string? GuestRelations { get; set; }
        public string? GuestCnic { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class UserData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }


}



