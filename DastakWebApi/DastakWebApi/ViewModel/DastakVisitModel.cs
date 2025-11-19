using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class DastakVisitModel
    {
        public DateTime? Date { get; set; }
        public string? Name { get; set; } // Adjust as needed
        public string? ObjectiveOfVisit { get; set; }
        public string? Location { get; set; }
        public string? DetailOfVisit { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int? NoOfPreviousVisits { get; set; }
        public int? NoOfPlannedVisits { get; set; }

        // Add any other necessary properties like StartTime and EndTime if needed
    }


}
