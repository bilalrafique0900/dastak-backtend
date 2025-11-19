using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class AbuseCountModel
    {
        public int TotalCount { get; set; }
        public int ActiveCount { get; set; }

        public AbuseCountModel(int totalCount, int activeCount)
        {
            TotalCount = totalCount;
            ActiveCount = activeCount;
        }
    }

}
