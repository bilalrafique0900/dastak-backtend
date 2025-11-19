using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly IUserService _userService;

        public UserController(DastakDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            // Querying the active users from the database
            var data = await _context.Users
                .Where(u => u.IsActive == 1)
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email,
                    Role = u.UserCategory
                })
                .OrderByDescending(u => u.Id)
                .ToListAsync();
            return Ok(new { data });
        }
        [HttpGet("getuserbyid")]
        public IActionResult Edit(int id)
        {
            // Get the logged-in user details (assuming UserService returns current user data)

            // var userData = _userService.GetCurrentUserData(id);

            // Fetch the user by id
            var user = _userService.GetUserById(id);

            if (user == null || user.IsActive != 1)
            {
                return NotFound("User not found or inactive.");
            }

            // Passing data to the view
            var data = user;

            return Ok(new { data });
        }

        [HttpPost("edit")]
        public IActionResult Edit(int id, User data)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, return back to the view with the validation errors
                return Ok(new { data });
            }

            // Get the logged-in user details
            // var loggedInUser = _userService.GetCurrentUserData();

            // Fetch the existing user
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update user details
            user.Name = data.Name;
            user.UserCategory = data.UserCategory;
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = User?.Identity.Name;

            // Save the updated user data
            _userService.UpdateUser(user);

            // Redirect to the index page
            return Ok(new { data = user });
        }

        [HttpPost("add")]
        public IActionResult Add(User data)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, return back to the view with the validation errors
                return Ok(new { data });
            }

            // Get the logged-in user details
            // var loggedInUser = _userService.GetCurrentUserData();
            data.IsActive = 1;
            data.CreatedAt = DateTime.Now;
           data.CreatedBy= User?.Identity.Name;
            // Fetch the existing user
            var user = _userService.GetUserByEmail(data.Email);
            if (user != null)
            {
                return NotFound("Email Already Exist.");
            }

            // Save  user data
            _userService.AddUser(data);

            // Redirect to the index page
            return Ok(new { data = data });
        }
        [HttpGet("delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Find the user by ID
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "User not found." });
            }

            // Remove the user from the database
            _context.Users.Remove(user);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "User deleted successfully." });
        }
    }

}
   