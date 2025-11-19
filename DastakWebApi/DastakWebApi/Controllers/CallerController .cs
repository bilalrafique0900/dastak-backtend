using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Dynamic;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CallerController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ILegalAssistanceService _legalService;

        public CallerController(DastakDbContext context, ILegalAssistanceService legalService)
        {
            _context = context;
            _legalService = legalService;
        }
  

        //[HttpGet]
        //public IActionResult Add()
        //{
        //    var userData = _userService.GetUserData();
        //    return View("Add", userData); // Renders the 'Add' view and passes user data to it
        //}

        [HttpPost ("postcall")]
        public IActionResult Add(CallerViewModel model)
        {
         //   if (ModelState.IsValid)
            {
                var caller = new Caller
                {
                    Date = model.Date,
                    Time = model.Time,
                    Name = model.Name,
                    Designation = model.Designation,
                    Organisation = model.Organisation,
                    DetailOfCaller = model.DetailOfCaller,
                    City = model.City,
                    Country = model.Country,
                    ContactNo = model.ContactNo,
                    ReasonForCall = model.ReasonForCall,
                    DetailOfCall = model.DetailOfCall,
                    Outcome = model.Outcome,
                  
                    CreatedAt = DateTime.Now,
                   CreatedBy = User?.Identity.Name,
                Active = 1
                };

                // Save to database (Assuming you have a dbContext to handle this)
                _context.Callers.Add(caller);
                _context.SaveChanges();

                return Ok(new { data = model });

              
            }

  
        }
        [HttpPost("postgeneralinquiry")]
        public IActionResult add(GeneralInquiryViewModel model)
        {
            //   if (ModelState.IsValid)
            {
                var general = new GeneralInquiry
                {
                    Date = model.Date,
                    Time = model.Time,
                    
                    ModeOfInquiry = model.ModeOfInquiry,
                    

                    CreatedAt = DateTime.Now,
                    CreatedBy = User?.Identity.Name,
                    Active = 1
                };

                // Save to database (Assuming you have a dbContext to handle this)
                _context.GeneralInquirys.Add(general);
                _context.SaveChanges();

                return Ok(new { data = model });


            }


        }
    }

}




  






      

    

