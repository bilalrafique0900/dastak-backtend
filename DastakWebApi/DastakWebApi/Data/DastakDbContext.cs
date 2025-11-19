using System;
using System.Collections.Generic;
using DastakWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Data;

public partial class DastakDbContext : DbContext
{
    public DastakDbContext()
    {
    }

    public DastakDbContext(DbContextOptions<DastakDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdditionalDetail> AdditionalDetails { get; set; }

    public virtual DbSet<AdmissionRecord> AdmissionRecords { get; set; }

    public virtual DbSet<AllegedAbuser> AllegedAbusers { get; set; }

    public virtual DbSet<BasicInfo> BasicInfos { get; set; }

    public virtual DbSet<Caller> Callers { get; set; }
    public virtual DbSet<GeneralInquiry> GeneralInquirys { get; set; }

    public virtual DbSet<Child> Children { get; set; }
    public virtual DbSet<ChildMedicalAssistance> ChildMedicalAssistance { get; set; }
    public virtual DbSet<ChildpsychologicalAssistance> ChildPsychologicalAssistance { get; set; }

    public virtual DbSet<ChildHealth> ChildHealths { get; set; }

    public virtual DbSet<ChildOrientation> ChildOrientations { get; set; }

    public virtual DbSet<ChildSchooling> ChildSchoolings { get; set; }

    public virtual DbSet<CommunicableDisease> CommunicableDiseases { get; set; }

    public virtual DbSet<ContactsInfo> ContactsInfos { get; set; }

    public virtual DbSet<DastakVisit> DastakVisits { get; set; }

    public virtual DbSet<Discharge> Discharges { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Documentfile> Documentfiles { get; set; }

    public virtual DbSet<Economic> Economics { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<DastakWebApi.Models.File> Files { get; set; }

    public virtual DbSet<FollowUp> FollowUps { get; set; }

    public virtual DbSet<Intervention> Interventions { get; set; }
    public virtual DbSet<InterventionCommunity> InterventionCommunity{ get; set; }

    public virtual DbSet<LegalAssistance> LegalAssistances { get; set; }
    public virtual DbSet<CommunityConsultation> CommunityConsultation { get; set; }

    public virtual DbSet<LegalNotice> LegalNotices { get; set; }

    public virtual DbSet<LoginActivity> LoginActivities { get; set; }

    public virtual DbSet<MaritalInfo> MaritalInfos { get; set; }

    public virtual DbSet<MedicalAssisstance> MedicalAssisstances { get; set; }

    public virtual DbSet<MedicalHistory> MedicalHistories { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<Orientation> Orientations { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Possession> Possessions { get; set; }

    public virtual DbSet<PsychologicalAssisstance> PsychologicalAssisstances { get; set; }

    public virtual DbSet<PsychologicalHistory> PsychologicalHistories { get; set; }

    public virtual DbSet<PsychologicalTherapySession> PsychologicalTherapySessions { get; set; }

    public virtual DbSet<ReferencesRecord> ReferencesRecords { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      //  => optionsBuilder.UseSqlServer("Data Source=DASTAK;Initial Catalog=DastakLatest;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Connect Timeout=30;");
    => optionsBuilder.UseSqlServer("Data Source=DESKTOP-1H4OLKQ\\SQLEXPRESS ;Initial Catalog=DastakLatest;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdditionalDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_additional_details_id");

            entity.ToTable("additional_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
        });

        modelBuilder.Entity<AdmissionRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_admission_record_id");

            entity.ToTable("admission_record");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdmissionDate)
                .HasColumnType("date")
                .HasColumnName("admission_date");
            entity.Property(e => e.InterviewDate)
                .HasColumnType("date")
                .HasColumnName("interview_date");
            entity.Property(e => e.IsAbused).HasColumnName("is_abused");
            entity.Property(e => e.IsReferedToOtherShelter).HasColumnName("is_refered_to_other_shelter");
            entity.Property(e => e.NatureOfAssisstance)
                .HasMaxLength(255)
                .HasColumnName("nature_of_assisstance");
            entity.Property(e => e.ReasonForAdmission)
                .HasMaxLength(255)
                .HasColumnName("reason_for_admission");
            entity.Property(e => e.ReasonOfRefuse)
                .HasMaxLength(255)
                .HasColumnName("reason_of_refuse");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.WhereHasSheRefered)
                .HasMaxLength(255)
                .HasColumnName("where_has_she_refered");
        });

        modelBuilder.Entity<AllegedAbuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_alleged_abuser_id");

            entity.ToTable("alleged_abuser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AbuserName)
                .HasMaxLength(50)
                .HasColumnName("abuser_name");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Address2)
                .HasMaxLength(255)
                .HasColumnName("address2");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .HasColumnName("contact");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.DetailOfAttemptedAbuse)
                .HasMaxLength(255)
                .HasColumnName("detail_of_attempted_abuse");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("father_name");
            entity.Property(e => e.HasBeenThreatened).HasColumnName("has_been_threatened");
            entity.Property(e => e.HasSufferedVerbalAbuse).HasColumnName("has_suffered_verbal_abuse");
            entity.Property(e => e.NatureOfBodilyInjury)
                .HasMaxLength(255)
                .HasColumnName("nature_of_bodily_injury");
            entity.Property(e => e.NatureOfPhysicalAbuse)
                .HasMaxLength(255)
                .HasColumnName("nature_of_physical_abuse");
            entity.Property(e => e.NatureOfSexualAbuse)
                .HasMaxLength(255)
                .HasColumnName("nature_of_sexual_abuse");
            entity.Property(e => e.NatureOfThreats)
                .HasMaxLength(255)
                .HasColumnName("nature_of_threats");
            entity.Property(e => e.Profession)
                .HasMaxLength(50)
                .HasColumnName("profession");
            entity.Property(e => e.Qualification)
                .HasMaxLength(50)
                .HasColumnName("qualification");
            entity.Property(e => e.ReasonOfInflictingAbuse)
                .HasMaxLength(255)
                .HasColumnName("reason_of_inflicting_abuse");
            entity.Property(e => e.ReasonOfToleratingAbuse)
                .HasMaxLength(255)
                .HasColumnName("reason_of_tolerating_abuse");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.RelationDuration)
                .HasMaxLength(50)
                .HasColumnName("relation_duration");
            entity.Property(e => e.Relationship)
                .HasMaxLength(50)
                .HasColumnName("relationship");
            entity.Property(e => e.ResidentName)
                .HasMaxLength(50)
                .HasColumnName("resident_name");
            entity.Property(e => e.SexualAbuseInflictedBy)
                .HasMaxLength(255)
                .HasColumnName("sexual_abuse_inflicted_by");
            entity.Property(e => e.TypeOfAbuse)
                .HasMaxLength(255)
                .HasColumnName("type_of_abuse");
            entity.Property(e => e.TypeOfEconomicAbuse)
                .HasMaxLength(255)
                .HasColumnName("type_of_economic_abuse");
            entity.Property(e => e.TypeOfVerbalAbuse)
                .HasMaxLength(255)
                .HasColumnName("type_of_verbal_abuse");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.WorkplacePhone)
                .HasMaxLength(50)
                .HasColumnName("workplace_phone");
        });

        modelBuilder.Entity<BasicInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_basic_info_id");

            entity.ToTable("basic_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.BirthReligion)
                .HasMaxLength(50)
                .HasColumnName("birth_religion");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .HasColumnName("cnic");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DomicileCity)
                .HasMaxLength(50)
                .HasColumnName("domicile_city");
            entity.Property(e => e.DomicileProvince)
                .HasMaxLength(50)
                .HasColumnName("domicile_province");
            entity.Property(e => e.Ethinicity)
                .HasMaxLength(50)
                .HasColumnName("ethinicity");
            entity.Property(e => e.FatherLivingStatus)
                .HasMaxLength(50)
                .HasColumnName("father_living_status");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("father_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.GuardianName)
                .HasMaxLength(50)
                .HasColumnName("guardian_name");
            entity.Property(e => e.GuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("guardian_relation");
            entity.Property(e => e.IsConvert).HasColumnName("is_convert");
            entity.Property(e => e.LiteracyLevel)
                .HasMaxLength(50)
                .HasColumnName("literacy_level");
            entity.Property(e => e.MotherLivingStatus)
                .HasMaxLength(50)
                .HasColumnName("mother_living_status");
            entity.Property(e => e.MotherName)
                .HasMaxLength(50)
                .HasColumnName("mother_name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(255)
                .HasColumnName("nationality");
            entity.Property(e => e.PassportNo)
                .HasMaxLength(255)
                .HasColumnName("passport_no");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Phone2)
                .HasMaxLength(50)
                .HasColumnName("phone2");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.Religion)
                .HasMaxLength(50)
                .HasColumnName("religion");
        });

        modelBuilder.Entity<Caller>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_callers_id");

            entity.ToTable("callers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contact_no");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Designation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("designation");
            entity.Property(e => e.DetailOfCall)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detail_of_call");
            entity.Property(e => e.DetailOfCaller)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detail_of_caller");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NoOfPlannedCalls).HasColumnName("no_of_planned_calls");
            entity.Property(e => e.NoOfPreviousCalls).HasColumnName("no_of_previous_calls");
            entity.Property(e => e.Organisation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("organisation");
            entity.Property(e => e.Outcome)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("outcome");
            entity.Property(e => e.ReasonForCall)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reason_for_call");
            entity.Property(e => e.Time).HasColumnName("time");
        });
        modelBuilder.Entity<GeneralInquiry>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_GeneralInquirys");

            entity.ToTable("GeneralInquirys");

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");

            entity.Property(e => e.Time)
                .HasColumnType("time(7)")
                .HasColumnName("time");

            entity.Property(e => e.ModeOfInquiry)
                .HasMaxLength(255)
                .HasColumnName("ModeOfInquiry");

            entity.Property(e => e.Active)
                .HasColumnName("active");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_children_id");

            entity.ToTable("children");

            entity.HasIndex(e => e.ChildReferenceNo, "children$child_reference_no").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Age)
                .HasMaxLength(50)
                .HasColumnName("age");
            entity.Property(e => e.ChildReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("child_reference_no");
            entity.Property(e => e.ChildReferenceNo2)
                .HasMaxLength(50)
                .HasColumnName("child_reference_no2");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.DischargeDate)
                .HasColumnType("date")
                .HasColumnName("discharge_date");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.HasBeenReferred).HasColumnName("has_been_referred");
            entity.Property(e => e.MotherName)
                .HasMaxLength(50)
                .HasColumnName("mother_name");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.WhereHasBeenReferred)
                .HasMaxLength(255)
                .HasColumnName("where_has_been_referred");
        });

        modelBuilder.Entity<ChildHealth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_child_health_id");

            entity.ToTable("child_health");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChildReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("child_reference_no");
            entity.Property(e => e.Hygiene)
                .HasMaxLength(50)
                .HasColumnName("hygiene");
            entity.Property(e => e.RequireMedicalOrPsychologicalAssisstance).HasColumnName("require_medical_or_psychological_assisstance");
            entity.Property(e => e.Residence)
                .HasMaxLength(50)
                .HasColumnName("residence");
            entity.Property(e => e.SoughtMedicalTreatment).HasColumnName("sought_medical_treatment");
            entity.Property(e => e.SpecialChild).HasColumnName("special_child");
            entity.Property(e => e.UnderPhysicalVoilence).HasColumnName("under_physical_voilence");
            entity.Property(e => e.UnderSexualVoilence).HasColumnName("under_sexual_voilence");
        });

        modelBuilder.Entity<ChildOrientation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_child_orientation_id");

            entity.ToTable("child_orientation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttendedTraining)
                .HasMaxLength(50)
                .HasColumnName("attended_training");
            entity.Property(e => e.ChildReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("child_reference_no");
            entity.Property(e => e.IsChildMaleAbove10).HasColumnName("is_child_male_above_10");
            entity.Property(e => e.NatureOfTraining)
                .HasMaxLength(255)
                .HasColumnName("nature_of_training");
            entity.Property(e => e.NextDateOfVaccination)
                .HasColumnType("date")
                .HasColumnName("next_date_of_vaccination");
            entity.Property(e => e.TypeOfVaccination)
                .HasMaxLength(255)
                .HasColumnName("type_of_vaccination");
            entity.Property(e => e.Vaccinated).HasColumnName("vaccinated");
            entity.Property(e => e.WhereMaleChildBeenSent)
                .HasMaxLength(50)
                .HasColumnName("where_male_child_been_sent");
        });

        modelBuilder.Entity<ChildSchooling>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_child_schooling_id");

            entity.ToTable("child_schooling");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChildReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("child_reference_no");
            entity.Property(e => e.GradeAssigned)
                .HasMaxLength(50)
                .HasColumnName("grade_assigned");
            entity.Property(e => e.ImpactOnExtraCuricularAbility)
                .HasMaxLength(50)
                .HasColumnName("impact_on_extra_curicular_ability");
            entity.Property(e => e.ImpactOnMathsAbility)
                .HasMaxLength(50)
                .HasColumnName("impact_on_maths_ability");
            entity.Property(e => e.ImpactOnReadingAbility)
                .HasMaxLength(50)
                .HasColumnName("impact_on_reading_ability");
            entity.Property(e => e.ImpactOnSocialAbility)
                .HasMaxLength(50)
                .HasColumnName("impact_on_social_ability");
            entity.Property(e => e.ImpactOnWritingAbility)
                .HasMaxLength(50)
                .HasColumnName("impact_on_writing_ability");
            entity.Property(e => e.ShelterSchoolEntryDate)
                .HasColumnType("date")
                .HasColumnName("shelter_school_entry_date");
            entity.Property(e => e.ShelterSchoolLeavingDate)
                .HasColumnType("date")
                .HasColumnName("shelter_school_leaving_date");
        });
        modelBuilder.Entity<ChildMedicalAssistance>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_ChildMedicalAssistance");

            entity.ToTable("ChildMedicalAssistance");

            entity.Property(e => e.Id)
                .HasColumnName("Id");

            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.Property(e => e.Age)
                .HasMaxLength(50)
                .HasColumnName("age");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");

            entity.Property(e => e.MedicalAssistanceId)
                .HasColumnName("MedicalAssistanceId");
        });
        
modelBuilder.Entity<ChildpsychologicalAssistance>(entity =>
{
    entity.HasKey(e => e.Id)
        .HasName("PK_ChildpsychologicalAssistance");

    entity.ToTable("ChildpsychologicalAssistance");

    entity.Property(e => e.Id)
        .HasColumnName("Id");

    entity.Property(e => e.ReferenceNo)
        .HasMaxLength(50)
        .HasColumnName("reference_no");

    entity.Property(e => e.Name)
        .HasMaxLength(50)
        .HasColumnName("name");

    entity.Property(e => e.ChildAge)
        .HasMaxLength(50)
        .HasColumnName("childage");

    entity.Property(e => e.CreatedAt)
        .HasColumnType("date")
        .HasColumnName("created_at");

    entity.Property(e => e.CreatedBy)
        .HasMaxLength(50)
        .HasColumnName("created_by");

    entity.Property(e => e.PsychologicalAssistanceId)
        .HasColumnName("PsychologicalAssistanceId");
});

        modelBuilder.Entity<CommunicableDisease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_communicable_diseases_id");

            entity.ToTable("communicable_diseases");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Diseases)
                .HasMaxLength(255)
                .HasColumnName("diseases");
            entity.Property(e => e.HasScreened).HasColumnName("has_screened");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
        });

        modelBuilder.Entity<ContactsInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_contacts_info_id");

            entity.ToTable("contacts_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConcentToInformFamily).HasColumnName("concent_to_inform_family");
            entity.Property(e => e.EmergencyName)
                .HasMaxLength(50)
                .HasColumnName("emergency_name");
            entity.Property(e => e.EmergencyPhone)
                .HasMaxLength(50)
                .HasColumnName("emergency_phone");
            entity.Property(e => e.EmergencyRelation)
                .HasMaxLength(50)
                .HasColumnName("emergency_relation");
            entity.Property(e => e.FamilyName)
                .HasMaxLength(50)
                .HasColumnName("family_name");
            entity.Property(e => e.FamilyPhone)
                .HasMaxLength(50)
                .HasColumnName("family_phone");
            entity.Property(e => e.FamilyRelation)
                .HasMaxLength(50)
                .HasColumnName("family_relation");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Phone2)
                .HasMaxLength(50)
                .HasColumnName("phone2");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
        });

        modelBuilder.Entity<DastakVisit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dastak_visit_id");

            entity.ToTable("dastak_visit");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.DetailOfVisit)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detail_of_visit");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NumberOfPlannedVisits).HasColumnName("number_of_planned_visits");
            entity.Property(e => e.NumberOfPreviousVisits).HasColumnName("number_of_previous_visits");
            entity.Property(e => e.ObjectiveOfVisit)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("objective_of_visit");
        });

        modelBuilder.Entity<Discharge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_discharge_id");

            entity.ToTable("discharge");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.AdmissionDate)
                .HasColumnType("date")
                .HasColumnName("admission_date");
            entity.Property(e => e.ConsentFollowUps).HasColumnName("consent_follow_ups");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.DischargeDate)
                .HasColumnType("date")
                .HasColumnName("discharge_date");
            entity.Property(e => e.FamilySignedRazinama).HasColumnName("family_signed_razinama");
            entity.Property(e => e.ForwardingAddress)
                .HasMaxLength(255)
                .HasColumnName("forwarding_address");
            entity.Property(e => e.FrequencyOfFollowUps)
                .HasMaxLength(50)
                .HasColumnName("frequency_of_follow_ups");
            entity.Property(e => e.GivenResourcesList).HasColumnName("given_resources_list");
            entity.Property(e => e.HasPoliceBeenInformed).HasColumnName("has_police_been_informed");
            entity.Property(e => e.LevelOfRiskAtDeparture)
                .HasMaxLength(50)
                .HasColumnName("level_of_risk_at_departure");
            entity.Property(e => e.NameOfResident)
                .HasMaxLength(50)
                .HasColumnName("name_of_resident");
            entity.Property(e => e.OriginalPossessionsReturned)
                .HasMaxLength(50)
                .HasColumnName("original_possessions_returned");
            entity.Property(e => e.PoliceInformedAt)
                .HasColumnType("date")
                .HasColumnName("police_informed_at");
            entity.Property(e => e.ReasonForLeaving)
                .HasMaxLength(50)
                .HasColumnName("reason_for_leaving");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.ResidenceAfterDischarge)
                .HasMaxLength(50)
                .HasColumnName("residence_after_discharge");
            entity.Property(e => e.SurvivorInformedShelter).HasColumnName("survivor_informed_shelter");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_documents_id");

            entity.ToTable("documents");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ListOfDocuments)
                .HasMaxLength(255)
                .HasColumnName("list_of_documents");
            entity.Property(e => e.Photocopied).HasColumnName("photocopied");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
        });

        modelBuilder.Entity<Documentfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_documentfiles_id");

            entity.ToTable("documentfiles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.Detail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detail");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reference_no");
        });

        modelBuilder.Entity<Economic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_economic_id");

            entity.ToTable("economic");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AlreadyEnrolledInEducationalInstitute).HasColumnName("already_enrolled_in_educational_institute");
            entity.Property(e => e.AttendedAnyWorkshopAtShelter).HasColumnName("attended_any_workshop_at_shelter");
            entity.Property(e => e.CourseConductedBy)
                .HasMaxLength(50)
                .HasColumnName("course_conducted_by");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DateOfWorkshopAtShelter)
                .HasColumnType("date")
                .HasColumnName("date_of_workshop_at_shelter");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.DurationOfCourse)
                .HasMaxLength(50)
                .HasColumnName("duration_of_course");
            entity.Property(e => e.DurationOfEmployement)
                .HasMaxLength(50)
                .HasColumnName("duration_of_employement");
            entity.Property(e => e.Education)
                .HasMaxLength(50)
                .HasColumnName("education");
            entity.Property(e => e.EmployementOpportunityProvided).HasColumnName("employement_opportunity_provided");
            entity.Property(e => e.EnrolledCourseDetail)
                .HasMaxLength(50)
                .HasColumnName("enrolled_course_detail");
            entity.Property(e => e.EnrolledToEvent)
                .HasMaxLength(255)
                .HasColumnName("enrolled_to_event");
            entity.Property(e => e.EnrolledToLearnNewSkills)
                .HasMaxLength(255)
                .HasColumnName("enrolled_to_learn_new_skills");
            entity.Property(e => e.FamilyIncome)
                .HasMaxLength(50)
                .HasColumnName("family_income");
            entity.Property(e => e.HasShelterAssistedInExternalInstituteEducation).HasColumnName("has_shelter_assisted_in_external_institute_education");
            entity.Property(e => e.InterestedInContinuingEducation).HasColumnName("interested_in_continuing_education");
            entity.Property(e => e.LivingArrangementBeforeAdmission)
                .HasMaxLength(255)
                .HasColumnName("living_arrangement_before_admission");
            entity.Property(e => e.NameOfResident)
                .HasMaxLength(50)
                .HasColumnName("name_of_resident");
            entity.Property(e => e.NatureOfCourse)
                .HasMaxLength(50)
                .HasColumnName("nature_of_course");
            entity.Property(e => e.NoOfIndividualsEarn).HasColumnName("no_of_individuals_earn");
            entity.Property(e => e.OccupationOfBreadwinner)
                .HasMaxLength(50)
                .HasColumnName("occupation_of_breadwinner");
            entity.Property(e => e.PreviousOccupation)
                .HasMaxLength(50)
                .HasColumnName("previous_occupation");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.SourceOfEducationalArrangements)
                .HasMaxLength(50)
                .HasColumnName("source_of_educational_arrangements");
            entity.Property(e => e.SourceOfResidentIncome)
                .HasMaxLength(50)
                .HasColumnName("source_of_resident_income");
            entity.Property(e => e.TypeOfWorkshopAtShelter)
                .HasMaxLength(50)
                .HasColumnName("type_of_workshop_at_shelter");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_feedback_id");

            entity.ToTable("feedback");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.AwarenessProgramsAndWorkshop)
                .HasMaxLength(50)
                .HasColumnName("awareness_programs_and_workshop");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CrisisManagementAndAttitude)
                .HasMaxLength(50)
                .HasColumnName("crisis_management_and_attitude");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.GivenOpportunitiesOfParticipating).HasColumnName("given_opportunities_of_participating");
            entity.Property(e => e.HasSuggestionsOrComplaints).HasColumnName("has_suggestions_or_complaints");
            entity.Property(e => e.MedicalOrPsychologicalFacilities)
                .HasMaxLength(50)
                .HasColumnName("medical_or_psychological_facilities");
            entity.Property(e => e.NameOfResident)
                .HasMaxLength(50)
                .HasColumnName("name_of_resident");
            entity.Property(e => e.OverAllExperience)
                .HasMaxLength(50)
                .HasColumnName("over_all_experience");
            entity.Property(e => e.PracticesKeepingChildrenSafe)
                .HasMaxLength(50)
                .HasColumnName("practices_keeping_children_safe");
            entity.Property(e => e.PrivacyEnsuredDuringMeeting).HasColumnName("privacy_ensured_during_meeting");
            entity.Property(e => e.ProvisionForFamilyMeetings)
                .HasMaxLength(50)
                .HasColumnName("provision_for_family_meetings");
            entity.Property(e => e.ProvisionOfClothingAndAccessories)
                .HasMaxLength(50)
                .HasColumnName("provision_of_clothing_and_accessories");
            entity.Property(e => e.ProvisionOfFood)
                .HasMaxLength(50)
                .HasColumnName("provision_of_food");
            entity.Property(e => e.ProvisionOfLegalAssisstance)
                .HasMaxLength(50)
                .HasColumnName("provision_of_legal_assisstance");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.RightsWereRespected).HasColumnName("rights_were_respected");
            entity.Property(e => e.SecurityArrangements)
                .HasMaxLength(50)
                .HasColumnName("security_arrangements");
            entity.Property(e => e.ServicesProvidedToHerChildren)
                .HasMaxLength(50)
                .HasColumnName("services_provided_to_her_children");
            entity.Property(e => e.SuggestionsOrComplaints)
                .HasMaxLength(50)
                .HasColumnName("suggestions_or_complaints");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<DastakWebApi.Models.File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_files_id");

            entity.ToTable("files");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.FileNo)
                .HasMaxLength(50)
                .HasColumnName("file_no");
            entity.Property(e => e.FileNo2)
                .HasMaxLength(50)
                .HasColumnName("file_no2");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<FollowUp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_follow_up_id");

            entity.ToTable("follow_up");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.BehaviourOfFamilyTowardsHer)
                .HasMaxLength(50)
                .HasColumnName("behaviour_of_family_towards_her");
            entity.Property(e => e.ConsentToFurtherFollowup).HasColumnName("consent_to_further_followup");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .HasColumnName("contact_no");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CurrentResidence)
                .HasMaxLength(50)
                .HasColumnName("current_residence");
            entity.Property(e => e.CurrentlyEmployed).HasColumnName("currently_employed");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.DischargeDate)
                .HasColumnType("date")
                .HasColumnName("discharge_date");
            entity.Property(e => e.FileNo)
                .HasMaxLength(50)
                .HasColumnName("file_no");
            entity.Property(e => e.FollowupDate)
                .HasColumnType("date")
                .HasColumnName("followup_date");
            entity.Property(e => e.FrequencyOfFollowUps)
                .HasMaxLength(50)
                .HasColumnName("frequency_of_follow_ups");
            entity.Property(e => e.NameOfResident)
                .HasMaxLength(50)
                .HasColumnName("name_of_resident");
            entity.Property(e => e.RecommendedSomeoneElseToShelter).HasColumnName("recommended_someone_else_to_shelter");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.StatusOfOriginalConcern)
                .HasMaxLength(50)
                .HasColumnName("status_of_original_concern");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Intervention>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_interventions_id");

            entity.ToTable("interventions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.AdditionalDetailsOfComplications)
                .HasMaxLength(255)
                .HasColumnName("additional_details_of_complications");
            entity.Property(e => e.AdditionalDetailsOfIntervention)
                .HasMaxLength(255)
                .HasColumnName("additional_details_of_intervention");
            entity.Property(e => e.Complications)
                .HasMaxLength(255)
                .HasColumnName("complications");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DetailOfIntervention)
                .HasMaxLength(255)
                .HasColumnName("detail_of_intervention");
            entity.Property(e => e.InterventionDate)
                .HasMaxLength(50)
                .HasColumnName("intervention_date");
            entity.Property(e => e.NatureOfIntervention)
                .HasMaxLength(255)
                .HasColumnName("nature_of_intervention");
            entity.Property(e => e.Outcome)
                .HasMaxLength(255)
                .HasColumnName("outcome");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
        });

        modelBuilder.Entity<LegalAssistance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_legal_assistances_id");

            entity.ToTable("legal_assistances");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CaseFiledAgainst)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("case_filed_against");
            entity.Property(e => e.CaseFiledBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("case_filed_by");
            entity.Property(e => e.CaseId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("case_id");
            entity.Property(e => e.CaseNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("case_no");
            entity.Property(e => e.CaseRef)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("case_ref");
            entity.Property(e => e.CityOfCourt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city_of_court");
            entity.Property(e => e.ContactOfLawyer)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contact_of_lawyer");
            entity.Property(e => e.Court)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("court");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.FirNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fir_no");
            entity.Property(e => e.IsLawyerShelterAssigned).HasColumnName("is_lawyer_shelter_assigned");
            entity.Property(e => e.NameOfLawyer)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name_of_lawyer");
            entity.Property(e => e.NatureOfLegalConcern)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nature_of_legal_concern");
            entity.Property(e => e.NextDateOfHearing)
                .HasColumnType("date")
                .HasColumnName("next_date_of_hearing");
            entity.Property(e => e.ProvinceOfCourt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("province_of_court");
            entity.Property(e => e.ReasonForWithdrawal)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reason_for_withdrawal");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reference_no");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("remarks");
            entity.Property(e => e.StatusOfCase)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status_of_case");
            entity.Property(e => e.TypeOfAssistance)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type_of_assistance");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<LegalNotice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_legal_notices_id");

            entity.ToTable("legal_notices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CaseId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("case_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.DateWhenLegalNoticeSent)
                .HasColumnType("date")
                .HasColumnName("date_when_legal_notice_sent");
            entity.Property(e => e.LegalAdviceSought).HasColumnName("legal_advice_sought");
            entity.Property(e => e.LegalAssistanceSought).HasColumnName("legal_assistance_sought");
            entity.Property(e => e.LegalNoticeSent).HasColumnName("legal_notice_sent");
            entity.Property(e => e.LegalNoticeSentTo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("legal_notice_sent_to");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reference_no");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<LoginActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_login_activity_id");

            entity.ToTable("login_activity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Passcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("passcode");
        });

        modelBuilder.Entity<MaritalInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_marital_info_id");

            entity.ToTable("marital_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccompanyingChildrenAge)
                .HasMaxLength(255)
                .HasColumnName("accompanying_children_age");
            entity.Property(e => e.AccompanyingChildrenName)
                .HasMaxLength(255)
                .HasColumnName("accompanying_children_name");
            entity.Property(e => e.AccompanyingChildrenRelation)
                .HasMaxLength(255)
                .HasColumnName("accompanying_children_relation");
            entity.Property(e => e.CurrentlyExpecting).HasColumnName("currently_expecting");
            entity.Property(e => e.ExpectedDeliveryDate)
                .HasColumnType("date")
                .HasColumnName("expected_delivery_date");
            entity.Property(e => e.HaveChildren).HasColumnName("have_children");
            entity.Property(e => e.MaritalCategory)
                .HasMaxLength(50)
                .HasColumnName("marital_category");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("marital_status");
            entity.Property(e => e.MaritalType)
                .HasMaxLength(50)
                .HasColumnName("marital_type");
            entity.Property(e => e.PartnerAbusedInDrug).HasColumnName("partner_abused_in_drug");
            entity.Property(e => e.ProofOfMarriage)
                .HasMaxLength(255)
                .HasColumnName("proof_of_marriage");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.SeparatedSince)
                .HasColumnType("date")
                .HasColumnName("separated_since");
            entity.Property(e => e.TotalChildren).HasColumnName("total_children");
            entity.Property(e => e.WifeOf)
                .HasMaxLength(50)
                .HasColumnName("wife_of");
            entity.Property(e => e.AgeOfMarriage).HasColumnName("age_of_marriage");
        });

        modelBuilder.Entity<MedicalAssisstance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_medical_assisstance_id");

            entity.ToTable("medical_assisstance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.CaseId)
                .HasMaxLength(50)
                .HasColumnName("case_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Complaint)
                .HasMaxLength(255)
                .HasColumnName("complaint");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .HasColumnName("contact_no");
            entity.Property(e => e.ContactNo2)
                .HasMaxLength(50)
                .HasColumnName("contact_no2");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DateWhenSought)
                .HasColumnType("date")
                .HasColumnName("date_when_sought");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.DetailOfTest)
                .HasMaxLength(255)
                .HasColumnName("detail_of_test");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(255)
                .HasColumnName("diagnosis");
            entity.Property(e => e.NameOfClinicAssisting)
                .HasMaxLength(50)
                .HasColumnName("name_of_clinic_assisting");
            entity.Property(e => e.NameOfDoctorAssisting)
                .HasMaxLength(50)
                .HasColumnName("name_of_doctor_assisting");
            entity.Property(e => e.NameOfResident)
                .HasMaxLength(50)
                .HasColumnName("name_of_resident");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.ShelterAgreedToConductTests)
                .HasMaxLength(50)
                .HasColumnName("shelter_agreed_to_conduct_tests");
            entity.Property(e => e.TreatmentSuggested)
                .HasMaxLength(255)
                .HasColumnName("treatment_suggested");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<MedicalHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_medical_history_id");

            entity.ToTable("medical_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Address2OfDoctor)
                .HasMaxLength(50)
                .HasColumnName("address2_of_doctor");
            entity.Property(e => e.AddressOfDoctor)
                .HasMaxLength(50)
                .HasColumnName("address_of_doctor");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.BriefOfHistory)
                .HasMaxLength(255)
                .HasColumnName("brief_of_history");
            entity.Property(e => e.CaseId)
                .HasMaxLength(50)
                .HasColumnName("case_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.ContactOfDoctor)
                .HasMaxLength(50)
                .HasColumnName("contact_of_doctor");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.CurrentMedicalPrescription)
                .HasMaxLength(255)
                .HasColumnName("current_medical_prescription");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.ExpectedDeliveryDate)
                .HasColumnType("date")
                .HasColumnName("expected_delivery_date");
            entity.Property(e => e.IntensityOfAbuse)
                .HasMaxLength(255)
                .HasColumnName("intensity_of_abuse");
            entity.Property(e => e.IntensityOfCurrentAbuse)
                .HasMaxLength(255)
                .HasColumnName("intensity_of_current_abuse");
            entity.Property(e => e.IsCurrentlySubstanceAbuser).HasColumnName("is_currently_substance_abuser");
            entity.Property(e => e.NameOfDoctor)
                .HasMaxLength(50)
                .HasColumnName("name_of_doctor");
            entity.Property(e => e.NameOfResident)
                .HasMaxLength(50)
                .HasColumnName("name_of_resident");
            entity.Property(e => e.NatureOfChronicIllness)
                .HasMaxLength(255)
                .HasColumnName("nature_of_chronic_illness");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.SubstancesInDrugAbused)
                .HasMaxLength(255)
                .HasColumnName("substances_in_drug_abused");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_meetings_id");

            entity.ToTable("meetings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.AtShelter).HasColumnName("at_shelter");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DateOfMeeting)
                .HasColumnType("date")
                .HasColumnName("date_of_meeting");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.GuestCnic)
                .HasMaxLength(255)
                .HasColumnName("guest_cnic");
            entity.Property(e => e.GuestName)
                .HasMaxLength(255)
                .HasColumnName("guest_name");
            entity.Property(e => e.GuestRelation)
                .HasMaxLength(255)
                .HasColumnName("guest_relation");
            entity.Property(e => e.Guests)
                .HasMaxLength(255)
                .HasColumnName("guests");
            entity.Property(e => e.NameOfResident)
                .HasMaxLength(50)
                .HasColumnName("name_of_resident");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.ResidentConsent).HasColumnName("resident_consent");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Orientation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_orientation_id");

            entity.ToTable("orientation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EnsuredConfidentialityOfData).HasColumnName("ensured_confidentiality_of_data");
            entity.Property(e => e.GivenCopyOfRights).HasColumnName("given_copy_of_rights");
            entity.Property(e => e.GivenCopyOfRules).HasColumnName("given_copy_of_rules");
            entity.Property(e => e.HasBeenOriented).HasColumnName("has_been_oriented");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_parents_id");

            entity.ToTable("parents");

            entity.HasIndex(e => e.ReferenceNo, "parents$reference_no").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.AdmissionAt)
                .HasColumnType("date")
                .HasColumnName("admission_at");
            entity.Property(e => e.AssessmentRisk)
                .HasMaxLength(50)
                .HasColumnName("assessment_risk");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.DiscardedBy)
                .HasMaxLength(50)
                .HasColumnName("discarded_by");
            entity.Property(e => e.Discharged).HasColumnName("discharged");
            entity.Property(e => e.EnsurePrivacy)
                .HasMaxLength(255)
                .HasColumnName("ensure_privacy");
            entity.Property(e => e.FileNo)
                .HasMaxLength(50)
                .HasColumnName("file_no");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IsAdmitted).HasColumnName("is_admitted");
            entity.Property(e => e.IsReadmission)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_readmission");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Pending).HasColumnName("pending");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.ReferenceNo2)
                .HasMaxLength(50)
                .HasColumnName("reference_no2");
            entity.Property(e => e.ResidenceBeforeReadmission)
                .HasMaxLength(50)
                .HasColumnName("residence_before_readmission");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Possession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_possessions_id");

            entity.ToTable("possessions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HasSignedAuthorizationLetter).HasColumnName("has_signed_authorization_letter");
            entity.Property(e => e.InPossesstionOf)
                .HasMaxLength(255)
                .HasColumnName("in_possesstion_of");
            entity.Property(e => e.Items)
                .HasMaxLength(255)
                .HasColumnName("items");
            entity.Property(e => e.Quantities)
                .HasMaxLength(255)
                .HasColumnName("quantities");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
        });

        modelBuilder.Entity<PsychologicalAssisstance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_psychological_assisstance_id");

            entity.ToTable("psychological_assisstance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.CaseId)
                .HasMaxLength(50)
                .HasColumnName("case_id");
            entity.Property(e => e.ConductedAt)
                .HasColumnType("date")
                .HasColumnName("conducted_at");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .HasColumnName("contact");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.EndedAt).HasColumnName("ended_at");
            entity.Property(e => e.LocationOfConsultant)
                .HasMaxLength(50)
                .HasColumnName("location_of_consultant");
            entity.Property(e => e.NameOfConsultant)
                .HasMaxLength(50)
                .HasColumnName("name_of_consultant");
            entity.Property(e => e.NameOfResident)
                .HasMaxLength(50)
                .HasColumnName("name_of_resident");
            entity.Property(e => e.NatureOfAssistance)
                .HasMaxLength(255)
                .HasColumnName("nature_of_assistance");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.ProvidedAt)
                .HasColumnType("date")
                .HasColumnName("provided_at");
            entity.Property(e => e.PsychologicalAssessment)
                .HasMaxLength(255)
                .HasColumnName("psychological_assessment");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.SoughtAt)
                .HasColumnType("date")
                .HasColumnName("sought_at");
            entity.Property(e => e.StartedAt).HasColumnName("started_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.WhatArrangementsMadeForImmidiateAssisstance)
                .HasMaxLength(255)
                .HasColumnName("what_arrangements_made_for_immidiate_assisstance");
        });

        modelBuilder.Entity<PsychologicalHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_psychological_history_id");

            entity.ToTable("psychological_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.CaseId)
                .HasMaxLength(50)
                .HasColumnName("case_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.HasBeenThreatened).HasColumnName("has_been_threatened");
            entity.Property(e => e.HasSufferedVerbalAbuse).HasColumnName("has_suffered_verbal_abuse");
            entity.Property(e => e.NameOfResident)
                .HasMaxLength(50)
                .HasColumnName("name_of_resident");
            entity.Property(e => e.NatureOfThreat)
                .HasMaxLength(255)
                .HasColumnName("nature_of_threat");
            entity.Property(e => e.PsychologicalAssessment)
                .HasMaxLength(255)
                .HasColumnName("psychological_assessment");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.TypeOfVerbalAbuse)
                .HasMaxLength(255)
                .HasColumnName("type_of_verbal_abuse");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.WhatArrangementsMadeForImmidiateAssisstance)
                .HasMaxLength(255)
                .HasColumnName("what_arrangements_made_for_immidiate_assisstance");
        });

        modelBuilder.Entity<PsychologicalTherapySession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_psychological_therapy_session_id");

            entity.ToTable("psychological_therapy_session");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.CaseId)
                .HasMaxLength(50)
                .HasColumnName("case_id");
            entity.Property(e => e.Complaint)
                .HasMaxLength(255)
                .HasColumnName("complaint");
            entity.Property(e => e.ConductedAt)
                .HasColumnType("date")
                .HasColumnName("conducted_at");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(255)
                .HasColumnName("diagnosis");
            entity.Property(e => e.EndedAt).HasColumnName("ended_at");
            entity.Property(e => e.LengthOfSession).HasColumnName("length_of_session");
            entity.Property(e => e.NameOfResident)
                .HasMaxLength(50)
                .HasColumnName("name_of_resident");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.PsychologicalAssessMentalHealth)
                .HasMaxLength(255)
                .HasColumnName("psychological_assess_mental_health");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.StartedAt).HasColumnName("started_at");
            entity.Property(e => e.Treatment)
                .HasMaxLength(255)
                .HasColumnName("treatment");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<ReferencesRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_references_record_id");

            entity.ToTable("references_record");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsReferencial).HasColumnName("is_referencial");
            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");
            entity.Property(e => e.ReferencialCity)
                .HasMaxLength(255)
                .HasColumnName("referencial_city");
            entity.Property(e => e.ReferencialDetails)
                .HasMaxLength(255)
                .HasColumnName("referencial_details");
            entity.Property(e => e.ReferencialName)
                .HasMaxLength(255)
                .HasColumnName("referencial_name");
            entity.Property(e => e.TypeOfReference)
                .HasMaxLength(255)
                .HasColumnName("type_of_reference");
        });
        modelBuilder.Entity<CommunityConsultation>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_CommunityConsultation");

            entity.ToTable("CommunityConsultation");

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.ReferenceNo)
                .HasMaxLength(50)
                .HasColumnName("reference_no");

            entity.Property(e => e.CaseId)
                .HasMaxLength(50)
                .HasColumnName("case_id");
            entity.Property(e => e.CaseRef)
            .HasMaxLength(50)
            .HasColumnName("case_ref");

            entity.Property(e => e.LegalAdviceSought)
                .HasColumnName("legal_advice_sought");

            entity.Property(e => e.LegalAssistanceSought)
                .HasColumnName("legal_assistance_sought");

            entity.Property(e => e.LegalNoticeSent)
                .HasColumnName("legal_notice_sent");

            entity.Property(e => e.DateWhenLegalNoticeSent)
                .HasColumnType("date")
                .HasColumnName("date_when_legal_notice_sent");

            entity.Property(e => e.LegalNoticeSentTo)
                .HasMaxLength(255)
                .HasColumnName("legal_notice_sent_to");

            entity.Property(e => e.TypeOfAssistance)
                .HasMaxLength(255)
                .HasColumnName("type_of_assistance");

            entity.Property(e => e.ReasonForWithdrawal)
                .HasMaxLength(255)
                .HasColumnName("reason_for_withdrawal");

            entity.Property(e => e.NatureOfLegalConcern)
                .HasMaxLength(255)
                .HasColumnName("nature_of_legal_concern");

            entity.Property(e => e.FirNo)
                .HasMaxLength(50)
                .HasColumnName("fir_no");

            entity.Property(e => e.CaseNo)
                .HasMaxLength(50)
                .HasColumnName("case_no");

            entity.Property(e => e.CaseFiledBy)
                .HasMaxLength(255)
                .HasColumnName("case_filed_by");

            entity.Property(e => e.CaseFiledAgainst)
                .HasMaxLength(255)
                .HasColumnName("case_filed_against");

            entity.Property(e => e.IsLawyerShelterAssigned)
                .HasColumnName("is_lawyer_shelter_assigned");

            entity.Property(e => e.NameOfLawyer)
                .HasMaxLength(50)
                .HasColumnName("name_of_lawyer");

            entity.Property(e => e.ContactOfLawyer)
                .HasMaxLength(50)
                .HasColumnName("contact_of_lawyer");

            entity.Property(e => e.Court)
                .HasMaxLength(255)
                .HasColumnName("court");

            entity.Property(e => e.ProvinceOfCourt)
                .HasMaxLength(255)
                .HasColumnName("province_of_court");

            entity.Property(e => e.CityOfCourt)
                .HasMaxLength(255)
                .HasColumnName("city_of_court");

            entity.Property(e => e.NextDateOfHearing)
                .HasColumnType("date")
                .HasColumnName("next_date_of_hearing");

            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("remarks");
            entity.Property(e => e.Outcome)
            .HasMaxLength(255)
            .HasColumnName("outcome");

            entity.Property(e => e.StatusOfCase)
                .HasMaxLength(255)
                .HasColumnName("status_of_case");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");

            entity.Property(e => e.Active)
                .HasColumnName("active");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });
        modelBuilder.Entity<InterventionCommunity>(entity =>
        {
          
                entity.HasKey(e => e.Id)
                    .HasName("PK_InterventionCommunity");

                entity.ToTable("InterventionCommunity");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.ReferenceNo)
                    .HasMaxLength(50)
                    .HasColumnName("reference_no");

                entity.Property(e => e.FileNo)
                    .HasMaxLength(50)
                    .HasColumnName("file_no");

                entity.Property(e => e.Name)
               .HasMaxLength(50)
               .HasColumnName("name");

                entity.Property(e => e.NatureOfIntervention)
                    .HasMaxLength(250)
                    .HasColumnName("nature_of_intervention");

                entity.Property(e => e.DetailOfIntervention)
                    .HasColumnName("detail_of_intervention");

                entity.Property(e => e.AdditionalDetailsOfIntervention)
                    .HasColumnName("additional_details_of_intervention");

                entity.Property(e => e.AdditionalDetailsOfComplications)
                    .HasColumnName("additional_details_of_complications");

                entity.Property(e => e.Complications)
                    .HasColumnName("complications");
                entity.Property(e => e.Outcome)
                    .HasColumnName("outcome");

                entity.Property(e => e.InterventionDate)
                    .HasColumnType("date")
                    .HasColumnName("intervention_date");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("created_by");

                entity.Property(e => e.Active)
                    .HasColumnName("active");
            });

       


        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users_id");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("date")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("deactivated_by");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("date")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserCategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_category");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_visitors_id");

            entity.ToTable("visitors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contact_no");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Designation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("designation");
            entity.Property(e => e.DetailOfVisit)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detail_of_visit");
            entity.Property(e => e.DetailOfVisitor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detail_of_visitor");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NoOfPlannedVisits).HasColumnName("no_of_planned_visits");
            entity.Property(e => e.NoOfPreviousVisits).HasColumnName("no_of_previous_visits");
            entity.Property(e => e.Organisation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("organisation");
            entity.Property(e => e.ReasonForVisit)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reason_for_visit");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.ToDate)
                .HasColumnType("date")
                .HasColumnName("to_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
