using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DastakVisitController : ControllerBase
    {
        private readonly DastakDbContext _context;


        public DastakVisitController(DastakDbContext context)
        {
            _context = context;

        }
        [HttpGet("getdastak")]
        public async Task<IActionResult> GetDastakVisit()
        {
            // Querying the active users from the database
            var data = await _context.DastakVisits
                .Where(u => u.Active == 1)
                .Select(u => new
                {
                    u.Id,
                    u.CreatedAt,
                    u.Date,
                    u.Name,
                    u.ObjectiveOfVisit,

                })
                .OrderByDescending(u => u.Id)
                .ToListAsync();
            return Ok(new { data });
        }


        [HttpPost ("postdastakvisit")]
        public IActionResult postdastakvisit(DastakVisitModel model) // Assuming a model class exists
        {
            //   var userData = _userController.GetUserData();
                var visitor = new DastakVisit
                {
                    Date = model.Date,
                    Name = model.Name,// JsonConvert.SerializeObject(model.Name), // Serialize the name to JSON
                    ObjectiveOfVisit = model.ObjectiveOfVisit,
                    Location = model.Location,
                    DetailOfVisit = model.DetailOfVisit,
                    City = model.City,
                    Country = model.Country,
                    NumberOfPreviousVisits=model.NoOfPreviousVisits,
                    NumberOfPlannedVisits = model.NoOfPlannedVisits,
                    CreatedAt = DateTime.Now,
                    CreatedBy = User?.Identity.Name,
            Active = 1
                };

                // Uncomment and adapt if you have start_time and end_time
                // visitor.StartTime = model.StartTime;
                // visitor.EndTime = model.EndTime;
                // visitor.Duration = (visitor.EndTime - visitor.StartTime).TotalSeconds;

                // Save to database (using your preferred method, e.g., Entity Framework)


                _context.DastakVisits.Add(visitor);
                _context.SaveChanges();

                    return Ok(new { data = visitor });
            }

        [HttpGet("getdastakbyid")]
        public IActionResult getdastakbyid(int id)
        {
            // Initialize the UserController and retrieve user data
            //var userController = new UserController();
           // var userData = userController.GetUserData();

            // Query the database for Dastakvisit with the given id
            var data = _context.DastakVisits
                               .Where(d => d.Id == id)
                               .ToList();

            // Return the view with the user and data
            return Ok( new {  data });
        }

        [HttpGet("deletedastakvisit")]
        public async Task<IActionResult> DeleteDastakVisit(int id)
        {
            // Find the user by ID
            var dastakVisit = await _context.DastakVisits.FindAsync(id);

            if (dastakVisit == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "DastakVisit not found." });
            }

            // Remove the user from the database
            _context.DastakVisits.Remove(dastakVisit);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "DastakVisit deleted successfully." });
        }
     

    }
}

