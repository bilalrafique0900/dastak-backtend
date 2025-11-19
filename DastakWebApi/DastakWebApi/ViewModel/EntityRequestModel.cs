using DastakWebApi.Models;

namespace DastakWebApi.ViewModel
{
    // Model for capturing request data
    public class EntityRequestModel
    {
        public string? FileNo2 { get; set; }
        public string? ReferenceNo2 { get; set; }
        public string? FileNo { get; set; }
        public string? ReferenceNo { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AssessmentRisk { get; set; }
        public string? ResidenceBeforeReadmission { get; set; }
        public string? EnsurePrivacy { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Religion { get; set; }
        public string? BirthReligion { get; set; }
        public string? FatherName { get; set; }
        public string? FatherLivingStatus { get; set; }
        public string? MotherName { get; set; }
        public string? MotherLivingStatus { get; set; }
        public string? GuardianName { get; set; }
        public string? GuardianRelation { get; set; }
        public string? Ethinicity { get; set; }
        public string? Nationality { get; set; }
        public string? PassportNo { get; set; }
        public string? CNIC { get; set; }
        public string? DomicileCity { get; set; }
        public string? DomicileProvince { get; set; }
        public string? Gender { get; set; }
        public string? LiteracyLevel { get; set; }
        public string? Phone { get; set; }
        public string? Phone2 { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int? Age { get; set; }
        public string? MaritalStatus { get; set; }
        public DateTime? SeparatedSince { get; set; }
        public string? MaritalCategory { get; set; }
        public string? MaritalType { get; set; }
        public string? WifeOf { get; set; }
        public short? PartnerAbusedInDrug { get; set; }
        public string? ProofOfMarriage { get; set; }
        public short? HaveChildren { get; set; }
        public short? TotalChildren { get; set; }
        public string? AccompanyingChildren { get; set; }
        public string? AccompanyingChildrenName { get; set; }
        public string? AccompanyingChildrenAge { get; set; }
        public string? AccompanyingChildrenRelation { get; set; }
        public short? CurrentlyExpecting { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string? TypeOfReference { get; set; }
        public string? NameOfReference { get; set; }
        public string? CityOfReference { get; set; }
        public short? IsReferential { get; set; }
        public int? AgeOfMarriage { get; set; }
        public short? IsReadmission { get; set; }
        public short? IsAdmitted { get; set; }
    }
}
