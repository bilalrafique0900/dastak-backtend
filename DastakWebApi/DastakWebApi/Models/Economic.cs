using System;
using System.Collections.Generic;

namespace DastakWebApi.Models;

public partial class Economic
{
    public int Id { get; set; }

    public string? ReferenceNo { get; set; }

    public string? NameOfResident { get; set; }

    public int? Age { get; set; }

    public string? Education { get; set; }

    public string? PreviousOccupation { get; set; }

    public string? SourceOfResidentIncome { get; set; }

    public string? FamilyIncome { get; set; }

    public string? OccupationOfBreadwinner { get; set; }

    public int? NoOfIndividualsEarn { get; set; }

    public string? LivingArrangementBeforeAdmission { get; set; }

    public short? InterestedInContinuingEducation { get; set; }

    public short? AlreadyEnrolledInEducationalInstitute { get; set; }

    public short? HasShelterAssistedInExternalInstituteEducation { get; set; }

    public string? SourceOfEducationalArrangements { get; set; }

    public short? EmployementOpportunityProvided { get; set; }

    public string? DurationOfEmployement { get; set; }

    public string? EnrolledToLearnNewSkills { get; set; }

    public string? EnrolledToEvent { get; set; }

    public string? EnrolledCourseDetail { get; set; }

    public string? NatureOfCourse { get; set; }

    public string? DurationOfCourse { get; set; }

    public string? CourseConductedBy { get; set; }

    public short? AttendedAnyWorkshopAtShelter { get; set; }

    public DateTime? DateOfWorkshopAtShelter { get; set; }

    public string? TypeOfWorkshopAtShelter { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short? Active { get; set; }

    public string? DeactivatedBy { get; set; }
}
