using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Dynamic;
using System.Linq;
namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ICallersService _callService;

        public MeetingsController(DastakDbContext context, ICallersService callService)
        {
            _context = context;
            _callService = callService;
        }



        [HttpGet("getmeeting")]
        public IActionResult getmeeting(string file, string entity)
        {
            // Create the view model
            var data = new MeetingViewModel
            {
                Entity = entity,
                File = file
            };

            // Fetch the discharged status from the entity
            var entityRecord = _context.Parents
                .Where(e => e.ReferenceNo == entity)
                .Select(e => new { e.Discharged })
                .FirstOrDefault();

            if (entityRecord != null)
            {
                data.Discharged = entityRecord.Discharged;
            }

            // Fetch the guest details from the Meeting table
            var guests = _context.Meetings
                .Where(m => m.ReferenceNo == entity && m.Active == 1)
                .OrderByDescending(m => m.Id)
                .Select(m => new GuestMeeting
                {
                    Id = m.Id,
                    Name = m.NameOfResident,
                    DateOfMeeting = m.DateOfMeeting,
                    GuestNames = m.GuestName,// JsonConvert.DeserializeObject<List<string>>(m.GuestName),
                    GuestRelations = m.GuestRelation,//JsonConvert.DeserializeObject<List<string>>(m.GuestRelation),
                    GuestCnic = m.GuestCnic,// JsonConvert.DeserializeObject<List<string>>(m.GuestCnic),
                    CreatedAt = m.CreatedAt
                })
                .ToList();

            data.Guests = guests;

            // Simulate user data (in real case, get it from a service or DB)


            return Ok(new { data });
        }

        [HttpGet("getmeetingadd")]
        public async Task<IActionResult> getmeetingadd(string file, string entity)
        {

            var userEmail = User?.Identity.Name;

            var data = await _context.Parents
                .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                .Select(e => new { e.FileNo, e.ReferenceNo, e.Title, e.FirstName, e.LastName })
                .FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new { data });
        }

        [HttpPost("postaddmeeting")]
        public async Task<IActionResult> postaddmeeting(MeetingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        //    var user = _userService.GetUserData();
            var meeting = new Meeting
            {
                ReferenceNo = model.ReferenceNo,
                NameOfResident = model.NameOfResident,
                DateOfMeeting = model.DateOfMeeting,
                City = model.City,
                Country = model.Country,
                AtShelter = model.AtShelter,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                GuestName = model.GuestNames,// != null ? JsonConvert.SerializeObject(model.GuestNames) : null,
                GuestCnic = model.GuestCnics,// != null ? JsonConvert.SerializeObject(model.GuestCnics) : null,
                GuestRelation = model.GuestRelations,// != null ? JsonConvert.SerializeObject(model.GuestRelations) : null,
                CreatedAt = DateTime.UtcNow,
    CreatedBy = User?.Identity.Name,
                Active = 1
            };

            _context.Meetings.Add(meeting);
            await _context.SaveChangesAsync();

            return Ok(new { data = model });
        }

    [HttpGet("postviewmeeting")]
    public IActionResult postviewmeeting(string entity, int id)
    {
        // Retrieve user data

        // Create a new data object


        // Retrieve entity information based on reference_no
        var Info = _context.Parents
            .Where(e => e.ReferenceNo == entity)
            .Select(e => new
            {
                e.ReferenceNo,
                e.Title,
                e.FirstName,
                e.LastName
            })
            .FirstOrDefault(); // Use FirstOrDefault to handle potential null values

        // Retrieve meeting information
        var Data = _context.Meetings
            .Where(m => m.Id == id && m.ReferenceNo == entity)
            .FirstOrDefault(); // FirstOrDefault used for single expected result

        var data = new
        {
            Info = Info,
            Data = Data
        };
        // Return the view with user data and the retrieved data
        return Ok(new { data });
    }

        [HttpGet("deletemeeting")]
        public async Task<IActionResult> deletemeeting(int id)
        {
            // Find the user by ID
            var Meeting = await _context.Meetings.FindAsync(id);

            if (Meeting == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "Meetings not found." });
            }

            // Remove the user from the database
            _context.Meetings.Remove(Meeting);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "Meetings deleted successfully." });
        }

    }
}























