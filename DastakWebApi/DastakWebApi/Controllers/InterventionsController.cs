using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly IInterventionService _intervenService;

        public InterventionsController(DastakDbContext context, IInterventionService intervenService)
        {
            _context = context;
            _intervenService = intervenService;
        }
        [HttpGet("getintervention")]
        public async Task<ActionResult> Index(string? file, string? entity)
        {
            // Simulate getting user data, similar to the PHP example.
           

            // Create a dynamic object (C# equivalent of stdClass in PHP).
            dynamic data = new ExpandoObject();
if (!entity.IsNullOrEmpty() && !file.IsNullOrEmpty() ) {
                data.entity = entity;
                data.discharged =await _context.Parents
                    .Where(e => e.ReferenceNo == entity)
                    .Select(e => e.Discharged)
                    .FirstOrDefaultAsync();


                data.file = file;

                // Fetching interventions from the Intervention table.

                data.interventions =await _context.Interventions
                    .Where(i => i.ReferenceNo == entity && i.Active == 1)
                    .OrderByDescending(i => i.Id)
                    .Select(i => new {
                        i.Id,
                        i.ReferenceNo,
                        i.NatureOfIntervention,
                        i.DetailOfIntervention,
                        i.CreatedAt,
                        i.InterventionDate,
                        i.Complications
                    }).ToListAsync();
                return Ok(new { data = data });
            }
            else
            {
                data.entity = entity;
                data.discharged =await _context.Parents
                    .Select(e => e.Discharged)
                    .ToListAsync();


                data.file = file;

                // Fetching interventions from the Intervention table.

                data.interventions =await _context.Interventions
                    .Where(i =>i.Active == 1)
                    .OrderByDescending(i => i.Id)
                    .Select(i => new {
                        i.Id,
                        i.ReferenceNo,
                        i.NatureOfIntervention,
                        i.DetailOfIntervention,
                        i.CreatedAt,
                        i.InterventionDate,
                        i.Complications
                    }).ToListAsync();
                return Ok(new { data = data });
            }
            
           
        }
        [HttpGet("getviewintervention")]

        public ActionResult View(string entity, int id)
        {
            // Retrieve entity info
           var info= _context.Parents
                .Where(e => e.ReferenceNo == entity)
                .Select(e => new
                {
                    e.FileNo,
                    e.ReferenceNo,
                    e.Title,
                    e.FirstName,
                    e.LastName
                })
                .FirstOrDefault();

            // Retrieve intervention data
            var mydata = _context.Interventions
                .Where(i => i.ReferenceNo == entity && i.Id == id)
                .FirstOrDefault();
            var data = new
            {
                info = info,
                intervention = mydata
            };

            // Pass the user and data to the view
            return Ok( new {  data =data });
        }


        [HttpGet("getinterventionadd")]
        public ActionResult GetInterventionl(string file, string entity)
        {



            var data = _context.Parents
                .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                .Select(e => new
                {
                    e.FileNo,
                    e.ReferenceNo,
                    e.Title,
                    FullName = e.FirstName + " " + e.LastName
                })
                .FirstOrDefault();

            return Ok(new { data });
        }
        [HttpPost("addintervention")]
        public IActionResult Add(InterventionFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { model }); // return the view with validation errors
            }

            foreach (var detail in model.DetailsOfIntervention)
            {
                if (string.IsNullOrEmpty(detail.Detail))
                {
                    continue;
                }


                var intervention = new Intervention
                    {
                     ReferenceNo = model.ReferenceNo,
                        InterventionDate =Convert.ToString(model.InterventionDate),
                        NatureOfIntervention = detail.NatureOfIntervention,
                        DetailOfIntervention = detail.Detail,
                        AdditionalDetailsOfIntervention = detail.AdditionalDetails,
                        Complications = detail.Complications,
                        AdditionalDetailsOfComplications = detail.AdditionalComplications,
                        Outcome = detail.Outcome,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = User?.Identity.Name,
                    Active = 1
                    };
                    {
                  

                    // Save the intervention record to the database
                    _context.Interventions.Add(intervention);
                }

                _context.SaveChanges();
             
            }

            return Ok(new { data = model });
        }

        [HttpGet("getallinterventioncommunity")]
        public async Task<IActionResult> getallinterventioncommunity()
        {
            // Querying the active users from the database
            var data = await _context.InterventionCommunity
                .Where(u => u.Active == 1)
                .Select(u => new
                {
                    u.Id,
                    u.CreatedAt,
                    u.InterventionDate,
                    u.NatureOfIntervention,
                    u.DetailOfIntervention,
                    u.Complications,
                    u.ReferenceNo,
                    u.Name

                })
                .OrderByDescending(u => u.Id)
                .ToListAsync();

            return Ok(new { data });
        }

        [HttpPost("addinterventioncommunity")]
        public IActionResult addinterventioncommunity(InterventionCommunityFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { model }); // return the view with validation errors
            }

            foreach (var detail in model.DetailsOfIntervention)
            {
                if (string.IsNullOrEmpty(detail.Detail))
                {
                    continue;
                }


                var interventioncommunity = new InterventionCommunity
                {
                    FileNo = model.FileNo,
                    ReferenceNo = model.ReferenceNo,
                    Name =model.Name,
                    InterventionDate = model.InterventionDate,
                    NatureOfIntervention = detail.NatureOfIntervention,
                    DetailOfIntervention = detail.Detail,
                    AdditionalDetailsOfIntervention = detail.AdditionalDetails,
                    Complications = detail.Complications,
                    AdditionalDetailsOfComplications = detail.AdditionalComplications,
                    Outcome = detail.Outcome,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = User?.Identity.Name,
                    Active = 1
                };
                {


                    // Save the intervention record to the database
                    _context.InterventionCommunity.Add(interventioncommunity);
                }

                _context.SaveChanges();

            }

            return Ok(new { data = model });
        }
        [HttpPost("Updateinterventioncommunity")]
        public async Task<ActionResult> Updateinterventioncommunity(InterventionCommunityFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the legal notice by id
                var interventionToUpdate = await _context.InterventionCommunity.FindAsync(model.Id);
                if (interventionToUpdate == null)
                {
                    return NotFound(new { message = " Intervention Community not found" });
                }
                foreach (var detail in model.DetailsOfIntervention)
                {
                    if (string.IsNullOrEmpty(detail.Detail))
                    {
                        continue;
                    }

                    // Update LegalNotice properties
                    interventionToUpdate.ReferenceNo = model.ReferenceNo;
                    interventionToUpdate.FileNo = model.FileNo;
                    interventionToUpdate.Name = model.Name;
                    interventionToUpdate.InterventionDate = model.InterventionDate;
                    interventionToUpdate.NatureOfIntervention = detail.NatureOfIntervention;
                    interventionToUpdate.DetailOfIntervention = detail.Detail;

                    interventionToUpdate.Active = 1;
                    interventionToUpdate.ReferenceNo = model.ReferenceNo;
                    interventionToUpdate.AdditionalDetailsOfIntervention = detail.AdditionalDetails;
                    interventionToUpdate.Complications = detail.Complications;
                    interventionToUpdate.AdditionalDetailsOfComplications = detail.AdditionalComplications;



                    interventionToUpdate.Outcome = detail.Outcome;
                    
                    interventionToUpdate.CreatedAt = DateTime.Now; // New updated time
                    interventionToUpdate.CreatedBy = User?.Identity.Name; // Set if needed
                    interventionToUpdate.Active = 1;

                    _context.Entry(interventionToUpdate).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Record updated successfully", data = model });
                }
            }

            // If the model state is invalid, return the view with validation errors
            return BadRequest(new { message = "Invalid data", data = model });
        }

        [HttpGet("getinterventioncommunityById")]
        public async Task<ActionResult<InterventionCommunity>> getinterventioncommunityById(int id)
        {
            var consultation = await _context.InterventionCommunity.FindAsync(id);

            if (consultation == null)
                return NotFound(new { message = "Community   not found" });

            return Ok(new { data = consultation });
        }
        [HttpGet("deleteinterventions")]
        public async Task<IActionResult> deleteinterventions(int id)
        {
            // Find the user by ID
            var Intervention = await _context.Interventions.FindAsync(id);

            if (Intervention == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "Interventions not found." });
            }

            // Remove the user from the database
            _context.Interventions.Remove(Intervention);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "Interventions deleted successfully." });
        }
        [HttpGet("deleteinterventioncommunity")]
        public async Task<IActionResult> deleteinterventioncommunity(int id)
        {
            // Find the user by ID
            var Interventioncommunity = await _context.InterventionCommunity.FindAsync(id);

            if (Interventioncommunity == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "Interventions not found." });
            }

            // Remove the user from the database
            _context.InterventionCommunity.Remove(Interventioncommunity);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "Interventioncommunity deleted successfully." });
        }
       
    }
}
