using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class EconomicController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ICallersService _callService;

        public EconomicController(DastakDbContext context, ICallersService callService)
        {
            _context = context;
            _callService = callService;
        }
        [HttpGet("geteconomic")]
        public async Task<IActionResult> geteconomic(string file, string entity)
        {
            // Get user data from UserController


            // Create a dynamic object to hold the data
            dynamic data = new System.Dynamic.ExpandoObject();

            data.entity = entity;

            // Query for discharged value
            var dischargedEntity = await _context.Parents
                .Where(e => e.ReferenceNo == entity)
                .Select(e => new { e.Discharged })
                .FirstOrDefaultAsync();

            if (dischargedEntity != null)
            {
                data.discharged = dischargedEntity.Discharged;
            }

            data.file = file;

            // Query for economic data
            var economicData = await _context.Economics
                .Where(e => e.ReferenceNo == entity && e.Active == 1)
                .OrderByDescending(e => e.Id)
                .Select(e => new
                {
                    e.Id,
                    Source = e.SourceOfResidentIncome,
                    e.TypeOfWorkshopAtShelter,
                    Education = e.Education,
                    Course = e.NatureOfCourse,
                    e.CreatedAt
                })
                .ToListAsync();

            data.economic = economicData;

            // Return view with user and data
            return Ok(new { data });
        }
        [HttpGet("geteconomicbyid")]
        public IActionResult View(string entity, int id)
        {
            // Create an instance of the user controller to get user data


            // Create a dynamic object to hold the data
            dynamic data = new ExpandoObject();

            // Query the Entity table for specific columns and filter by reference_no
            var entityInfo = _context.Parents
                                     .Where(e => e.ReferenceNo == entity)
                                     .Select(e => new
                                     {
                                         e.ReferenceNo,
                                         e.Title,
                                         e.FirstName,
                                         e.LastName
                                     })
                                     .FirstOrDefault();

            data.info = entityInfo;

            // Query the Economic table to fetch data based on id and reference_no
            var economicData = _context.Economics
                                       .Where(e => e.Id == id && e.ReferenceNo == entity)
                                       .FirstOrDefault();

            data.economic = economicData;

            // Pass the user data and the data object to the view
            return Ok(new { data });
        }

        [HttpGet("geteconomicadd")]
        public async Task<IActionResult> geteconomicadd(string file, string entity)
        {
            // Assuming you have a User service that provides the current user's information


            // Fetch data from the database using Entity Framework
            var data = await _context.Parents
                .Join(_context.BasicInfos,
                    parents => parents.ReferenceNo,
                    basicInfo => basicInfo.ReferenceNo,
                    (parents, basicInfo) => new { parents, basicInfo })
                .Where(joined => joined.parents.ReferenceNo == entity && joined.parents.FileNo == file)
                .Select(joined => new
                {
                    joined.parents,
                    joined.basicInfo
                })
                .FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }

            // Pass data to the view
            return Ok(new { data });
        }

        [HttpPost("posteconomicadd")]
        public async Task<IActionResult> posteconomicadd(EconomicViewModel model)
        {


            if (ModelState.IsValid)
            {
                // Assuming you have an Economic entity and you map the model to the entity
                var economic = new Economic
                {
                    ReferenceNo = model.ReferenceNo,
                    NameOfResident = model.NameOfResident,
                    Age = model.Age,
                    Education = model.Education,
                    PreviousOccupation = model.PreviousOccupation,
                    SourceOfResidentIncome = model.SourceOfResidentIncome,
                    FamilyIncome = model.FamilyIncome,
                    OccupationOfBreadwinner = model.OccupationOfBreadwinner,
                    NoOfIndividualsEarn = model.NoOfIndividualsEarn,
                    LivingArrangementBeforeAdmission = model.LivingArrangementBeforeAdmission,
                    InterestedInContinuingEducation = model.InterestedInContinuingEducation,
                    AlreadyEnrolledInEducationalInstitute = model.AlreadyEnrolledInEducationalInstitute,
                    HasShelterAssistedInExternalInstituteEducation = model.HasShelterAssistedInExternalInstituteEducation,
                    SourceOfEducationalArrangements = model.SourceOfEducationalArrangements,
                    EnrolledToLearnNewSkills = model.EnrolledToLearnNewSkills,
                    EnrolledToEvent = model.EnrolledToEvent,
                    EnrolledCourseDetail = model.EnrolledCourseDetail,
                    NatureOfCourse = model.NatureOfCourse,
                    DurationOfCourse = model.DurationOfCourse,
                    CourseConductedBy = model.CourseConductedBy,
                    AttendedAnyWorkshopAtShelter = model.AttendedAnyWorkshopAtShelter,
                    DateOfWorkshopAtShelter = model.DateOfWorkshopAtShelter,
                    TypeOfWorkshopAtShelter = model.TypeOfWorkshopAtShelter,
                    EmployementOpportunityProvided = model.EmploymentOpportunityProvided,
                    DurationOfEmployement = model.DurationOfEmployment,
                    CreatedAt = DateTime.Now,
                    CreatedBy = User?.Identity.Name,
                Active = 1
                };

                // Save to the database
                _context.Economics.Add(economic);
                await _context.SaveChangesAsync();
            }

            // If the model state is not valid, return the form with errors
            return Ok(new { data = model });
        }





        [HttpGet("deleteeconomic")]
        public async Task<IActionResult> deleteeconomic(int id)
        {
            // Find the user by ID
            var Economic = await _context.Economics.FindAsync(id);

            if (Economic == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "Economics not found." });
            }

            // Remove the user from the database
            _context.Economics.Remove(Economic);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "Economics deleted successfully." });
        }
    }
}
