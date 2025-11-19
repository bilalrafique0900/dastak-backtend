using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly IMenuService _menService;

        public MenuController(DastakDbContext context, IMenuService menService)
        {
            _context = context;
            _menService = menService;
        }

        [HttpGet("files")]
        public async Task<IActionResult> Index(string? searchQuery = null, List<FileViewModel>? data = null)
        {
         

            if (data == null)
            {
                if (string.IsNullOrEmpty(searchQuery))
                {
                    // Fetch all relevant data if no entity provided
                    data = _menService.SelectFiles()
                        .OrderByDescending(f => f.Id)
                        .Take(50)
                        .ToList();
                }
                else
                {
                    // Fetch specific entity data
                    data = _menService.SelectFiles()
                        //.Where(f => f.Active == 1 && f.ParentActive == 1)
                       // .Where(f => f.IsAdmitted == 1 && f.ReferenceNo == searchQuery)
                          .Where(f=>f.ReferenceNo == searchQuery
                          || f.FileNo==searchQuery 
                          || f.AssessmentRisk==searchQuery
                          || (f.Title + " " + f.FirstName + " " + f.LastName).Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
                          )
                        .OrderByDescending(f => f.ParentId)
                        .Take(50)
                        .ToList();
                }

                // If no data found, return view with null data
                if (!data.Any())
                {
                    return Ok(new { data = (List<FileViewModel>?)null });
                }
            }

            // Return view with fetched data
            return Ok( new { data });
        }
        [HttpGet("all-files")]
        public async Task<IActionResult> AllFiles(string? searchQuery = null, List<FileViewModel>? data = null)
        {


            if (data == null)
            {
                if (string.IsNullOrEmpty(searchQuery))
                {
                    // Fetch all relevant data if no entity provided
                    data = _menService.AllFiles()
                        .OrderByDescending(f => f.Id)
                        .Take(50)
                        .ToList();
                }
                else
                {
                    // Fetch specific entity data
                    data = _menService.AllFiles()
                          //.Where(f => f.Active == 1 && f.ParentActive == 1)
                          // .Where(f => f.IsAdmitted == 1 && f.ReferenceNo == searchQuery)
                          //.Where(f => f.ReferenceNo == searchQuery
                          //|| f.FileNo == searchQuery
                          //|| f.AssessmentRisk == searchQuery
                          //|| (f.Title + " " + f.FirstName + " " + f.LastName).Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
                          //)
                          .Where(f =>
                       f.ReferenceNo == searchQuery
                     || f.FileNo == searchQuery
                    || f.AssessmentRisk == searchQuery
              || (f.Title + " " + f.FirstName + " " + f.LastName).Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
             || (f.Title + " " + f.FirstName).Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
            || (f.FirstName + " " + f.LastName).Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
            || (f.FirstName??"").Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
            || (f.LastName??"").Equals(searchQuery, StringComparison.OrdinalIgnoreCase)
)

                        .OrderByDescending(f => f.ParentId)
                        .Take(50)
                        .ToList();
                }

                // If no data found, return view with null data
                if (!data.Any())
                {
                    return Ok(new { data = (List<FileViewModel>?)null });
                }
            }

            // Return view with fetched data
            return Ok(new { data });
        }



        [HttpGet ("getfeedbackdeparture")]
        public async Task<IActionResult> Add(string file, string entity)
        {
        

            var data = _context.Parents
                .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                .Select(e => new
                {
                    e.FileNo,
                    e.ReferenceNo,
                    e.Title,
                    e.FirstName,
                    e.LastName,
                    FullName = e.FirstName + " " + e.LastName,
                    e.AdmissionAt
                })
                .FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            return Ok( new {  data });
        }
        [HttpGet("geteditbasicinfo")]
        public async Task<ActionResult> geteditbasicinfo( string entity)
        {



            var data = _context.Parents
                .Where(e =>  e.ReferenceNo == entity)
                .Select(e => new
                {
                    
                    
                    e.Title,

                    e.FirstName ,
                    e.LastName,
                    FullName= e.Title+ " "+ e.FirstName+ " " + e.LastName,
                    e.IsReadmission
                })
                .FirstOrDefault();

            return Ok(new { data });
        }

        [HttpGet("getproceedinfoforreadmission")]
        public async Task<ActionResult> getproceedinfoforreadmission(string entity)
        {



            var parentinfo = _context.Parents.Where(e => e.ReferenceNo == entity).Select(e => new{e.Title,e.FirstName,e.LastName,FullName = e.Title + " " + e.FirstName + " " + e.LastName,e.IsReadmission}).FirstOrDefault();
            var admissioninfo=_context.AdmissionRecords.Where(m=>m.ReferenceNo == entity).FirstOrDefault();
            var contactinfo=_context.ContactsInfos.Where(m=>m.ReferenceNo==entity).FirstOrDefault();
            var documentinfo = _context.Documents.Where(m => m.ReferenceNo == entity).FirstOrDefault();
            var communicableDiseasesinfo = _context.CommunicableDiseases.Where(m => m.ReferenceNo == entity).FirstOrDefault();
            var possessioninfo = _context.Possessions.Where(m => m.ReferenceNo == entity).FirstOrDefault();
            var additionaldetailsinfo= _context.AdditionalDetails.Where(m => m.ReferenceNo == entity).FirstOrDefault();
            var data = new
            {
                parentinfo,
                admissioninfo,
                contactinfo,
                documentinfo,
                communicableDiseasesinfo,
                possessioninfo,
                additionaldetailsinfo
            };
            return Ok(new { data });
        }
        [HttpGet("getlegalreadmission")]
        public async Task<ActionResult> getlegalreadmission(string file, string entity)
        {



            var data = _context.Parents
                .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                .Select(e => new
                {
                    
                    
                    e.Title,
                    e.FirstName,
                    e.LastName,
                    FullName = e.FirstName + " " + e.LastName
                })
                .FirstOrDefault();

            return Ok(new { data });
        }

        [HttpPost ("postfeedbackdeparture")]
        public async Task<IActionResult>Add(DischargeViewModel model)
        {
          

            if (!ModelState.IsValid)
            {
                return Ok(new { data = model });
            }

            // Create and populate Discharge entity
            var discharge = new Discharge
            {
                ReferenceNo = model.ReferenceNo,
                NameOfResident = model.NameOfResident,
                AdmissionDate = model.AdmissionDate,
                DischargeDate = model.DischargeDate,
                SurvivorInformedShelter = model.SurvivorInformedShelter,
                HasPoliceBeenInformed = model.HasPoliceBeenInformed,
                PoliceInformedAt = model.PoliceInformedAt,
                OriginalPossessionsReturned = model.OriginalPossessionsReturned,
                FamilySignedRazinama = model.FamilySignedRazinama,
                ReasonForLeaving = model.ReasonForLeaving,
                ResidenceAfterDischarge = model.ResidenceAfterDischarge,
                ConsentFollowUps = model.ConsentFollowUps,
                LevelOfRiskAtDeparture = model.LevelOfRiskAtDeparture,
                ForwardingAddress = model.ForwardingAddress,
                FrequencyOfFollowUps = model.FrequencyOfFollowUps,
                GivenResourcesList = model.GivenResourcesList,
                CreatedAt = DateTime.Now,
               CreatedBy = User?.Identity.Name,
                Active = 1
            };

            _context.Discharges.Add(discharge);

            // Create and populate Feedback entity
            var feedback = new Feedback
            {
                ReferenceNo = model.ReferenceNo,
                NameOfResident = model.NameOfResident,
                OverAllExperience = model.OverAllExperience,
                SecurityArrangements = model.SecurityArrangements,
                ProvisionOfFood = model.ProvisionOfFood,
                ProvisionOfClothingAndAccessories = model.ProvisionOfClothingAndAccessories,
                MedicalOrPsychologicalFacilities = model.MedicalOrPsychologicalFacilities,
                ProvisionOfLegalAssisstance = model.ProvisionOfLegalAssistance,
                ProvisionForFamilyMeetings = model.ProvisionForFamilyMeetings,
                CrisisManagementAndAttitude = model.CrisisManagementAndAttitude,
                ServicesProvidedToHerChildren = model.ServicesProvidedToHerChildren,
                AwarenessProgramsAndWorkshop = model.AwarenessProgramsAndWorkshop,
                HasSuggestionsOrComplaints = model.HasSuggestionsOrComplaints,
                SuggestionsOrComplaints = model.SuggestionsOrComplaints,
                RightsWereRespected = model.RightsWereRespected,
                PrivacyEnsuredDuringMeeting = model.PrivacyEnsuredDuringMeeting,
                PracticesKeepingChildrenSafe = model.PracticesKeepingChildrenSafe,
                GivenOpportunitiesOfParticipating = model.GivenOpportunitiesOfParticipating,
                CreatedAt = DateTime.Now,
               CreatedBy = User?.Identity.Name,
                Active = 1
            };

            _context.Feedbacks.Add(feedback);

            // Update Entity as discharged
            var entityToUpdate = _context.Parents.FirstOrDefault(e => e.FileNo == model.file && e.ReferenceNo == model.entity);
            if (entityToUpdate != null)
            {
                entityToUpdate.Discharged = 1;
                entityToUpdate.UpdatedAt = DateTime.Now;
                entityToUpdate.DeactivatedBy = User?.Identity.Name;
            }

            // Update Abuser as inactive
            var abuserToUpdate = _context.AllegedAbusers.FirstOrDefault(a => a.ReferenceNo == model.entity);
            if (abuserToUpdate != null)
            {
                abuserToUpdate.Active = 0;
               abuserToUpdate.DeactivatedBy = User?.Identity.Name;
            }

            // Update Child as inactive and set discharge date
            var childrenToUpdate = _context.Children.Where(c => c.ReferenceNo == model.entity).ToList();
            foreach (var child in childrenToUpdate)
            {
                child.Active = 0;
                child.DeactivatedBy = User?.Identity.Name;
                child.DischargeDate = model.DischargeDate;
            }

            _context.SaveChangesAsync();

         // var msg = $"Dastak resident having reference number {entity} has been departed by {userData.Email} on {discharge.CreatedAt}";
            // Optionally send an email
            // EmailService.Send("sabashaikh@dastak.org.pk", "Resident departure", msg);

            return Ok(new {data= model });
        }
    }
}
