using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class FollowUpController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly IFollowService _follService;

        public FollowUpController(DastakDbContext context, IFollowService follService)
        {
            _context = context;
            _follService = follService;
        }
        [HttpGet("getfollowrecord")]
        public ActionResult Index(string file, string entity)
        {

            var followupData = _context.FollowUps
                  .Where(f => f.ReferenceNo == entity && f.Active == 1)
                  .OrderByDescending(f => f.Id)
                  .Select(f => new
                  {
                      f.Id,
                      f.ReferenceNo,
                      f.NameOfResident,
                      f.ContactNo,
                      f.CurrentResidence
                  })
                  .ToList();
            // Create an object for the data
            var data = new
            {
                entity = entity,
                file = file,
                followup = followupData
            };

            // Return the data to the view
            return Ok( new {  data = data });
        }

        [HttpGet ("getfollowdetail")]
        public async Task<IActionResult> Add(string file, string entity)
        {
            

            var data = await _context.Parents
                .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                .Select(e => new
                {
                    e.FileNo,
                    e.ReferenceNo,
                    e.Title,
                    e.FirstName,
                    e.LastName
                })
                .FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }

            var dischargeDate = await _context.Discharges
                .Where(d => d.ReferenceNo == entity)
                .Select(d => d.DischargeDate)
                .FirstOrDefaultAsync();

            var viewModel = new FollowupViewModel
            {
                FileNo = data.FileNo,
                ReferenceNo = data.ReferenceNo,
                Title = data.Title,
                FullName = data.FirstName + " " + data.LastName,
                
                DischargeDate = dischargeDate
            };

            return Ok(new { data = viewModel });
        }

        [HttpPost("postfollowdata")]
        public async Task<IActionResult> Add(FollowupFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(model); // Return the view with validation messages if the model is not valid
            }

            var followup = new FollowUp
            {
                ReferenceNo = model.ReferenceNo,
                NameOfResident = model.NameOfResident,
                FileNo = model.FileNo,
                ContactNo = model.ContactNo,
                FollowupDate = model.FollowupDate,
                DischargeDate = model.DischargeDate,
                CurrentResidence = model.CurrentResidence,
                StatusOfOriginalConcern = model.StatusOfOriginalConcern,
                BehaviourOfFamilyTowardsHer = model.BehaviourOfFamilyTowardsHer,
                CurrentlyEmployed = model.CurrentlyEmployed,
                RecommendedSomeoneElseToShelter = model.RecommendedSomeoneElseToShelter,
                ConsentToFurtherFollowup = model.ConsentToFurtherFollowup,
                FrequencyOfFollowUps = model.Frequency,
                CreatedAt = DateTime.Now,
                 CreatedBy =  User?.Identity.Name,
                Active = 1
            };

            _context.FollowUps.AddAsync(followup);
             _context.SaveChanges();

            return Ok(new { data=model });
        }


        [HttpGet("deletefollow")]
        public async Task<IActionResult> deletefollow(int id)
        {
            // Find the user by ID
            var Followup = await _context.FollowUps.FindAsync(id);

            if (Followup == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "Followups not found." });
            }

            // Remove the user from the database
            _context.FollowUps.Remove(Followup);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "Followups deleted successfully." });
        }




    }
}
