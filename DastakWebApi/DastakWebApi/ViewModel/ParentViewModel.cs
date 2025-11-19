using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    public class ParentViewModel
    {
        public string ReferenceNo { get; set; }
        public string FileNo { get; set; }
        public string Title { get; set; }
        public bool Discharged { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DischargeDate { get; set; }
    }


}
