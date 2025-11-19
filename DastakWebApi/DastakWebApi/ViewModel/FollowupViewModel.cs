using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class FollowupViewModel
    {

        public DateTime? FollowupDate { get; set; }

        public DateTime? DischargeDate { get; set; }
        public string FileNo { get; set; }
        public string Title { get; set; }
        public string ReferenceNo { get; set; }
        public string FullName { get; set; }
    }
        public class FollowupFormModel
        {
            public string ReferenceNo { get; set; }
            public string NameOfResident { get; set; }
            public string FileNo { get; set; }
            public string ContactNo { get; set; }
            public DateTime? FollowupDate { get; set; }
            public DateTime? DischargeDate { get; set; }
            public string CurrentResidence { get; set; }
            public string StatusOfOriginalConcern { get; set; }
            public string BehaviourOfFamilyTowardsHer { get; set; }
            public short CurrentlyEmployed { get; set; }
            public short RecommendedSomeoneElseToShelter { get; set; }
            public short ConsentToFurtherFollowup { get; set; }
            public string Frequency { get; set; }
        }

    }

