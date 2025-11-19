
    namespace DastakWebApi.ViewModel
    {
        public class EntityEditViewModel
        {
            // Entity Section
            public int? Id { get; set; }
            public string? ReferenceNo { get; set; }

            public string? ReferenceNo2 { get; set; }

           
            public string? Title { get; set; }

           
            public string? FirstName { get; set; }

       
            public string? LastName { get; set; }

            public string? AssessmentRisk { get; set; }
            public string? EnsurePrivacy { get; set; }
            public string? ResidenceBeforeReadmission { get; set; }
            public DateTime? DateOfBirth { get; set; }

            public string? Religion { get; set; }
            public string? BirthReligion { get; set; }
            public string? FatherName { get; set; }

            public string? FatherLivingStatus { get; set; }
            public string? MotherName { get; set; }

            public string? MotherLivingStatus { get; set; }
            public string? GuardianName { get; set; }
            public string?GuardianRelation { get; set; }
        public string? Ethinicity { get; set; }
        public string? PassportNo { get; set; }

        public string? Nationality { get; set; }
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

            public int? Age { get; set; }  // Calculated if DateOfBirth is provided
            public bool? IsConvert { get; set; }

            // Marital Information Section
            public string? MaritalStatus { get; set; }
            public DateTime? SeparatedSince { get; set; }
            public string? MaritalCategory { get; set; }
            public string? MaritalType { get; set; }
            public string? WifeOf { get; set; }
            public short? PartnerAbusedInDrug { get; set; }
            public string? ProofOfMarriage { get; set; }  // Stored as JSON in the original PHP code

            public short? HaveChildren { get; set; }
            public int? TotalChildren { get; set; }
            public int? AccompanyingChildren { get; set; }
            public string? AccompanyingChildrenName { get; set; }  // Stored as JSON in the original PHP code
            public string? AccompanyingChildrenAge { get; set; }   // Stored as JSON in the original PHP code
            public string? AccompanyingChildrenRelation { get; set; }  // Stored as JSON in the original PHP code

            public short? CurrentlyExpecting { get; set; }

      
            public DateTime? ExpectedDeliveryDate { get; set; }

            // References Section
            public string? TypeOfReference { get; set; }  // Stored as JSON in the original PHP code
            public string? ReferencialName { get; set; }  // Stored as JSON in the original PHP code
            public string? ReferencialCity { get; set; }  // Stored as JSON in the original PHP code

            public short? IsReferencial { get; set; }

            // Metadata Section
            public DateTime ?UpdatedAt { get; set; }
            public string? UpdatedBy { get; set; }
        public int? AgeOfMarriage { get; set; }
    }
    }


