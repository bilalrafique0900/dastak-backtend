using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Dynamic;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ILegalAssistanceService _legalService;

        public NotificationController(DastakDbContext context, ILegalAssistanceService legalService)
        {
            _context = context;
            _legalService = legalService;
        }
       




        [HttpGet("getnotification")]
        public async Task<IActionResult> Index()
        {
           
  

            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now.AddDays(7);

            // Fetching child orientation data with a join
            var vaccinations = await (from co in _context.ChildOrientations
                                      join c in _context.Children
                                      on co.ChildReferenceNo equals c.ChildReferenceNo
                                      where c.Active == 1 &&
                                            co.NextDateOfVaccination >= date1 &&
                                            co.NextDateOfVaccination <= date2
                                      select co).ToListAsync();

            // Fetching legal assistance data with a join
            var hearings = await (from la in _context.LegalAssistances
                                  join p in _context.Parents
                                  on la.ReferenceNo equals p.ReferenceNo
                                  where p.Active == 1 &&
                                        la.NextDateOfHearing >= date1 &&
                                        la.NextDateOfHearing <= date2
                                  select la).ToListAsync();

            var data = new
            {
                vaccinations,
                hearings
            };

            return Ok( new {  data = data });
        }

























    }
}
