using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AllCallersController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ICallersService _callService;

        public AllCallersController(DastakDbContext context, ICallersService callService)
        {
            _context = context;
            _callService = callService;
        }
        [HttpGet("getcallers")]
        public async Task<IActionResult> GetCallers()
        {
            // Querying the active users from the database
            var data = await _context.Callers
                .Where(u => u.Active == 1)
                .Select(u => new
                {
                    u.Id,
                    u.Date,
                    u.Name,
                    u.ContactNo,
                    
                })
                .OrderByDescending(u => u.Id)
                .ToListAsync();
            return Ok(new { data });
        }
        [HttpGet("getgenralinquirys")]
        public async Task<IActionResult> GetGeneralInquirys()
        {
            // Querying the active users from the database
            var data = await _context.GeneralInquirys
                .Where(u => u.Active == 1)
                .Select(u => new
                {
                    u.Id,
                    u.Date,
                    u.Time,
                    u.ModeOfInquiry,

                })
                .OrderByDescending(u => u.Id)
                .ToListAsync();
            return Ok(new { data });
        }
        [HttpGet("getcallersbyid")]
        public IActionResult GetById(int id)
        {
            // Get the logged-in user details (assuming UserService returns current user data)

            // var userData = _userService.GetCurrentUserData(id);

            // Fetch the user by id
            var allcaller = _callService.GetCallersById(id);

            if (allcaller == null || allcaller.Active != 1)
            {
                return NotFound("AllCallers not found or inactive.");
            }

            // Passing data to the view
            var data = allcaller;

            return Ok(new { data });
            
        }
        
        [HttpGet("delete")]
        public async Task<IActionResult> DeleteCaller(int id)
        {
            // Find the user by ID
            var allcaller = await _context.Callers.FindAsync(id);

            if (allcaller == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "Callers not found." });
            }

            // Remove the user from the database
            _context.Callers.Remove(allcaller);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "Callers deleted successfully." });
        }
        [HttpGet("deletegeneral")]
        public async Task<IActionResult> Deletegeneral(int id)
        {
            // Find the user by ID
            var generalInquiry = await _context.GeneralInquirys.FindAsync(id);

            if (generalInquiry == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "GeneralInquiry not found." });
            }

            // Remove the user from the database
            _context.GeneralInquirys.Remove(generalInquiry);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "GeneralInquiry deleted successfully." });
        }
    }
}
