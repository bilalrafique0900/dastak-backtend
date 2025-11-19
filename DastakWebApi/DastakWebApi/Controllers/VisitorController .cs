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
    public class VisitorController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly IVisitorService _visitorService;

        public VisitorController(DastakDbContext context, IVisitorService visitorService)
        {
            _context = context;
            _visitorService = visitorService;
        }

        [HttpGet("getexternalvisitor")]
        public async Task<IActionResult> GetVisitor()
        {
            // Querying the active users from the database
            var data = await _context.Visitors
                .Where(u => u.Active == 1)
                .Select(u => new
                {
                    u.Id,
                    u.Date,
                    u.Name,
                    u.DetailOfVisitor,
                    u.Organisation,
                })
                .OrderByDescending(u => u.Id)
                .ToListAsync();
            return Ok(new { data });
        }
        [HttpGet("getvisitorbyid")]
        public IActionResult Edit(int id)
        {
            // Get the logged-in user details (assuming UserService returns current user data)

            // var userData = _userService.GetCurrentUserData(id);

            // Fetch the user by id
            var visitor = _visitorService.GetVisitorById(id);

            //if (visitor == null || visitor.Active != 1)
            //{
            //    return NotFound("User not found or inactive.");
            //}

            // Passing data to the view
            var data = visitor;

            return Ok(new { data });
        }



        [HttpGet("delete")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            // Find the user by ID
            var visitor = await _context.Visitors.FindAsync(id);

            if (visitor == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "Visitor not found." });
            }

            // Remove the user from the database
            _context.Visitors.Remove(visitor);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "Visitor deleted successfully." });
        }


        //[HttpGet]
        //public IActionResult Add()
        //{
        //    var userData = _userService.GetUserData();
        //    return View("Add", userData); // Assumes you have a view called "Add"
        //}

        [HttpPost ("postvisit")]
        public IActionResult postvisit(VisitorViewModel model) 
        {
            


                var visitor = new Visitor
                {
                    Date = model.Date,
                    ToDate = model.ToDate,
                    Time = model.Time,
                    Name = model.Name,//!= null ? JsonConvert.SerializeObject(model.Name) : null,
                    Designation = model.Designation,// != null ? JsonConvert.SerializeObject(model.Designation) : null,
                    Organisation = model.Organisation,
                    DetailOfVisitor = model.DetailOfVisitor,//!= null ? JsonConvert.SerializeObject(model.DetailOfVisitor) : null,
                    City = model.City,
                    Country = model.Country,
                    ContactNo = model.ContactNo,// != null ? JsonConvert.SerializeObject(model.ContactNo) : null,
                    ReasonForVisit = model.ReasonForVisit,
                    DetailOfVisit = model.DetailOfVisit,
                    NoOfPreviousVisits = model.NoOfPreviousVisits,
                    NoOfPlannedVisits = model.NoOfPlannedVisits,
                    CreatedAt = DateTime.Now,
                     CreatedBy = User?.Identity.Name,
            Active = 1
                };

                _context.Visitors.Add(visitor);
                _context.SaveChanges();

                return Ok(new { data = visitor });
            }
        }


    }


   