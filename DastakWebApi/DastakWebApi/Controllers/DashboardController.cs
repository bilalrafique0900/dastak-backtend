using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DastakDbContext _context;
        public DashboardController(DastakDbContext context)
        {
            _context = context;
        }
        [HttpGet("home")]
        public async Task<IActionResult> Home()
        {
            var CreatedBy = User?.Identity.Name;

            var year = 2020;
            var month = 12;

            // Medical Assistance Counts

            var medicalCounts = await GetCountForLastMonthsAsync(_context.MedicalAssisstances, year, month);
            var psychologicalCounts = await GetCountForLastMonthsAsync(_context.PsychologicalAssisstances, year, month);
            var interventionsCounts = await GetCountForLastMonthsAsync(_context.Interventions, year, month);


            // Admissions Counts
            var admissionsCounts = await GetCountForLastMonthsAsync(_context.Parents, year, month, "AdmissionAt");
            // Departure Counts
            var departureCounts = await GetCountForLastMonthsAsync(_context.Discharges, year, month, "DischargeDate");


            // Admissions Counts
            var admissionsLastYearCounts = await GetCountForLastYearAsync(_context.Parents, year, month, "AdmissionAt");
            // monthlyAveragesStay

            var today = DateTime.Today;
            var twelveMonthsAgo = today.AddMonths(-11);
           // var twelveMonthsAgo = DateTime.UtcNow.AddMonths(-12);
            // Step 1: Get all needed data into memory
            var discharges = await _context.Discharges.AsNoTracking()
                .Where(d => d.AdmissionDate.HasValue && d.DischargeDate.HasValue &&
                            d.DischargeDate.Value >= d.AdmissionDate.Value &&
                            d.AdmissionDate >= new DateTime(twelveMonthsAgo.Year, twelveMonthsAgo.Month, 1))
                .Select(d => new
                {
                    AdmissionDate = d.AdmissionDate.Value,
                    DischargeDate = d.DischargeDate.Value,
                    StayDuration = (d.DischargeDate.Value - d.AdmissionDate.Value).TotalDays
                })
                .ToListAsync();

            // Step 2: Group and calculate average in memory
            var grouped = discharges
                .GroupBy(d => new { d.AdmissionDate.Year, d.AdmissionDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    AverageDays = Math.Round(g.Average(x => x.StayDuration))
                })
                .ToList();

            // Step 3: Fill in months with no data
            var averagedaysstayLastYear = Enumerable.Range(0, 12)
                .Select(i => twelveMonthsAgo.AddMonths(i))
                .Select(date =>
                {
                    var match = grouped.FirstOrDefault(m => m.Year == date.Year && m.Month == date.Month);
                    return new
                    {
                        AverageDays = match != null ? match.AverageDays : 0
                    };
                })
                .ToList();

            // Departure Counts
            var departureLastYearCounts = await GetCountForLastYearAsync(_context.Discharges, year, month, "DischargeDate");

            // Top Cities
            var topCities = await _context.BasicInfos.AsNoTracking()
                .Where(b => !string.IsNullOrEmpty(b.City) && b.City != "Other")
                .GroupBy(b => b.City)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new { City = g.Key, Count = g.Count() })
                .ToListAsync();
            var stats =await StatsAsync();
            var data = new
            {
                Medical = medicalCounts,
                Psychological = psychologicalCounts,
                Interventions = interventionsCounts,
                Admissions = admissionsCounts,
                Departure = departureCounts,
                AdmissionsLastYear= admissionsLastYearCounts,
                AveragedaysstayLastYear= averagedaysstayLastYear,
                DepartureLastYear = departureLastYearCounts,
                TopCities = topCities,
           
                stats =stats
            };

            return Ok(new { data = data });
        }

        private async Task<Dictionary<string, object>> StatsAsync()
        {
            var twelveMonthsAgo = DateTime.UtcNow.AddMonths(-12);
            var children = await _context.Children.AsNoTracking()
                .Join(_context.Parents.AsNoTracking(),
                    child => child.ReferenceNo,
                    parent => parent.ReferenceNo,
                    (child, parent) => new { child, parent })
                .CountAsync(parent => parent.parent.Pending == 0 && parent.parent.IsAdmitted == 1 && parent.parent.Discharged != 1 && parent.parent.Active == 1);

            var childrensince_1991 = await _context.Children.AsNoTracking().CountAsync(c=> c.CreatedAt >= twelveMonthsAgo);

            var parents = await _context.Parents.AsNoTracking()
                .CountAsync(parent => parent.Pending == 0 && parent.IsAdmitted == 1 && parent.Discharged != 1 && parent.Active == 1);

            var pending = await _context.Parents
                .Join(_context.Files,
                    parent => parent.FileNo,
                    file => file.FileNo,
                    (parent, file) => new { parent, file })
                .CountAsync(p => p.file.Active == 1 && p.parent.Active == 1 && p.parent.Pending == 1);

            var readmissions = await _context.Parents.AsNoTracking()
                .CountAsync(parent => parent.Pending == 0 && 
                parent.IsAdmitted == 1 &&
                parent.IsReadmission == 1 &&
        parent.AdmissionAt >= twelveMonthsAgo
                );

            var cases = await _context.Parents.AsNoTracking()
                .CountAsync(parent => parent.Pending == 0 && parent.IsAdmitted == 1);

            var total = parents + children;

            var daysList = await _context.Discharges.AsNoTracking()
        .Where(d => d.AdmissionDate.HasValue && d.DischargeDate.HasValue &&
                    d.DischargeDate.Value >= d.AdmissionDate.Value && d.AdmissionDate >= twelveMonthsAgo)
        .Select(d => (d.DischargeDate.Value - d.AdmissionDate.Value).TotalDays)
        .ToListAsync();

            //var averageDays = daysList.Any() ? daysList.Average() : 0;
            var averageDays = daysList.Any() ? (int)Math.Round(daysList.Average()) : 0;


            var literacyLevel = new Dictionary<string, int>
    {
        { "PhD", await _context.BasicInfos.AsNoTracking().CountAsync(bi => bi.LiteracyLevel.Contains("PhD")) },
        { "Masters", await _context.BasicInfos.AsNoTracking().CountAsync(bi => bi.LiteracyLevel.Contains("Masters")) },
        { "Graduation", await _context.BasicInfos.AsNoTracking().CountAsync(bi => bi.LiteracyLevel.Contains("Graduation")) },
        { "Intermediate", await _context.BasicInfos.AsNoTracking().CountAsync(bi => bi.LiteracyLevel.Contains("Intermediate")) },
        { "Matric", await _context.BasicInfos.AsNoTracking().CountAsync(bi => bi.LiteracyLevel.Contains("Matric")) },
        { "Religious course", await _context.BasicInfos.AsNoTracking().CountAsync(bi => bi.LiteracyLevel.Contains("Religious course")) },
        { "Undermatric", await _context.BasicInfos.AsNoTracking().CountAsync(bi => bi.LiteracyLevel.Contains("Undermatric")) },
        { "Non-Formal Education", await _context.BasicInfos.CountAsync(bi => bi.LiteracyLevel.Contains("Non-Formal Education")) },
        { "Illiterate", await _context.BasicInfos.AsNoTracking().CountAsync(bi => bi.LiteracyLevel.Contains("Illiterate")) }
    };

            var reference = new Dictionary<string, int>
    {
        { "Court referral", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("Court referral")) },
        { "Police", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("Police")) },
        { "Former Dastak resident", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("Former Dastak resident")) },
        { "Community member", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("Community member")) },
        { "Relative", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("Relative")) },
        { "Other shelter", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("Other shelter")) },
        { "Advertisement", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("Advertisement")) },
        { "NGO", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("NGO")) },
        { "Website", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("Website")) },
        { "Lawyer", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("Lawyer")) },
        { "Shelter helpline", await _context.ReferencesRecords.AsNoTracking().CountAsync(rr => rr.TypeOfReference.Contains("Shelter helpline")) }
    };

            var age = await _context.BasicInfos.AsNoTracking()
                .GroupBy(bi => bi.Age)
                .Select(g => new { Age = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToListAsync();

            var reasonOfAdmission = new Dictionary<string, int>
    {
        { "Seeking shelter and protection from serious threats", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Seeking shelter and protection from serious threats")) },
        { "Escape from forced marriage", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Escape from forced marriage")) },
        { "Escape from abuse and harassment", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Escape from abuse and harassment")) },
        { "Lack of family/social support", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Lack of family/social support")) },
        { "Vani", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Vani")) },
        { "Sawara", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Sawara")) },
        { "Karo-kari", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Karo-kari")) },
        { "Watta satta", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Watta satta")) },
        { "Marriage with Quran", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Marriage with Quran")) },
        { "Deprivation from inheriting property", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Deprivation from inheriting property")) },
        { "Other", await _context.AdmissionRecords.AsNoTracking().CountAsync(ar => ar.ReasonForAdmission.Contains("Other")) },
        { "Under Age Marriage", await _context.MaritalInfos.AsNoTracking().CountAsync(ar => ar.AgeOfMarriage != null && ar.AgeOfMarriage < 18) }
    };

            var abuseType = new Dictionary<string, AbuseCountModel>
    {
        { "Physical",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Physical")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Physical") && rr.Active == 1)
          )
        },
        { "Psychological",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Psychological")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Psychological") && rr.Active == 1)
          )
        },
        { "Sexual",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Sexual")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Sexual") && rr.Active == 1)
          )
        },
        { "Economic",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Economic")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Economic") && rr.Active == 1)
          )
        },
        { "Verbal",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Verbal")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Verbal") && rr.Active == 1)
          )
        },
        { "Threats",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Threats")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Threats") && rr.Active == 1)
          )
        },
                                    { "Other",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Other")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.TypeOfAbuse.Contains("Other") && rr.Active == 1)
          )
        },
    };

            var abuserrelation = new Dictionary<string, AbuseCountModel>
    {
        { "In-laws",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("In-laws")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("In-laws") && rr.Active == 1)
          )
        },
        { "Husband",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Husband")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Husband") && rr.Active == 1)
          )
        },
        { "Son",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Son")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Son") && rr.Active == 1)
          )
        },
        { "Father",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Father")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Father") && rr.Active == 1)
          )
        },
        { "Brother",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Brother")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Brother") && rr.Active == 1)
          )
        },
        { "Mother",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Mother")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Mother") && rr.Active == 1)
          )
        },
          { "Employer/Coworker",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Employer/Coworker")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Employer/Coworker") && rr.Active == 1)
          )
        },
            { "Stranger",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Stranger")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Stranger") && rr.Active == 1)
          )
        },  { "Teacher",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Teacher")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Teacher") && rr.Active == 1)
          )
        },
              { "Neighbour",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Neighbour")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Neighbour") && rr.Active == 1)
          )
        },
                { "Acquaintance",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Acquaintance")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Acquaintance") && rr.Active == 1)
          )
        },
                 { "Male Relative from own Family",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Male Relative from own Family")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Male Relative from own Family") && rr.Active == 1)
          )
        },
                          { "Male Relative from Husband's family",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Male Relative from Husband's family")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("OtMale Relative from Husband's familyher") && rr.Active == 1)
          )
        },
                                   { "Other",
          new AbuseCountModel(
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Other")),
              await _context.AllegedAbusers.AsNoTracking().CountAsync(rr => rr.Relationship.Contains("Other") && rr.Active == 1)
          )
        },
    };
            return new Dictionary<string, object>
    {
        { "children", children },
        { "childrensince_1991", childrensince_1991 },
        { "parents", parents },
        { "pending", pending },
        { "readmissions", readmissions },
        { "cases", cases },
        { "total", total },
        { "averageDays", averageDays },
        { "literacyLevel", literacyLevel },
        { "reference", reference },
        { "age", age },
        { "reasonOfAdmission", reasonOfAdmission },
        { "abuseType", abuseType },
        { "abuserrelation", abuserrelation }
    };
        }

        private async Task<int[]> GetCountForLastMonthsAsync<TEntity>(
DbSet<TEntity> dbSet,
int year,
int month,
string dateColumn = "CreatedAt") where TEntity : class
        {
            var counts = new int[6]; // Array to hold counts for the last 6 months

            for (int i = 0; i < 6; i++)
            {
                var targetDate = new DateTime(year, month, 1).AddMonths(-i);
                var startDate = targetDate; // Start of the month
                var endDate = targetDate.AddMonths(1); // Start of the next month

                counts[i] = await dbSet.AsNoTracking().CountAsync(m =>
                    (int)EF.Property<short>(m, "Active") == 1 &&
                    EF.Property<DateTime>(m, dateColumn) >= startDate &&
                    EF.Property<DateTime>(m, dateColumn) < endDate);
            }

            return counts;
        }

        private async Task<int[]> GetCountForLastYearAsync<TEntity>(
DbSet<TEntity> dbSet,
int year,
int month,
string dateColumn = "CreatedAt") where TEntity : class
        {
            var counts = new int[12]; // Array to hold counts for the last 6 months

            for (int i = 0; i < 12; i++)
            {
                var targetDate = new DateTime(year, month, 1).AddMonths(-i);
                var startDate = targetDate; // Start of the month
                var endDate = targetDate.AddMonths(1); // Start of the next month

                counts[i] = await dbSet.AsNoTracking().CountAsync(m =>
                    (int)EF.Property<short>(m, "Active") == 1 &&
                    EF.Property<DateTime>(m, dateColumn) >= startDate &&
                    EF.Property<DateTime>(m, dateColumn) < endDate);
            }

            return counts;
        }

 }

}
















