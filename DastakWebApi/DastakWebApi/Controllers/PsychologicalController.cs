using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Linq;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PsychologicalController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ICallersService _callService;

        public PsychologicalController(DastakDbContext context, ICallersService callService)
        {
            _context = context;
            _callService = callService;
        }
      


        [HttpGet("getphysical")]
        public IActionResult Index(string file, string entity)
        {
        
            dynamic data = new ExpandoObject();

            // Set the entity.
            data.entity = entity;

            // Query to get the discharged value from the Entity table.
            var dischargedQuery = _context.Parents

                .Where(e => e.ReferenceNo == entity)
                .Select(e => e.Discharged)
                .FirstOrDefault();

            data.discharged = dischargedQuery;

            // Set the file value.
            data.file = file;

            // Query to get the assistance data where the reference_no matches and active is true.
            var assistanceData = _context.PsychologicalAssisstances
                .Where(a => a.ReferenceNo == entity && a.Active == 1)
                .OrderByDescending(a => a.Id)
                .ToList();

            data.physical = assistanceData;

            // Return the view with the user and data object.
            return Ok(new { data });
        }


        [HttpGet("getphysicalbyid")]
        public async Task<IActionResult> getphysicalbyid(string file, string entity, int id)
        {
            

            // Create an object to hold the data
            var data = new
            {
                Info = await _context.Parents
                    .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                    .Select(e => new
                    {
                        e.FileNo,
                        e.ReferenceNo,
                        e.Title,
                        e.FirstName,
                        e.LastName
                    })
                    .FirstOrDefaultAsync(),

                Assistance = await _context.PsychologicalAssisstances
                    .Where(a => a.ReferenceNo == entity && a.Id == id)
                    .FirstOrDefaultAsync()
            };

            // Pass the data to the view
            return Ok( new { data });
        }


     
        [HttpGet("getphysicaladd")]
        public async Task<IActionResult> Add(string file, string entity)
        {
            

            var data = await (from parents in _context.Parents
                              join basicInfo in _context.BasicInfos on parents.ReferenceNo equals basicInfo.ReferenceNo
                              where parents.ReferenceNo == entity && parents.FileNo == file
                              select new { parents, basicInfo }).FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound(); // Handle data not found
            }

            return Ok( new {  data });
        }


        [HttpPost("postphysicaladd")]
        public async Task<IActionResult> postphysicaladd(PsychologicalAssistanceViewModel model)
        {
                var assistance = new PsychologicalAssisstance
                {
                    ReferenceNo = model.ReferenceNo,
                    NameOfResident = model.NameOfResident,
                    Age = model.Age,
                    PsychologicalAssistanceProvidedTo = model.PsychologicalAssistanceProvidedTo,
                    PsychologicalAssessment = model.PsychologicalAssessment,
                    WhatArrangementsMadeForImmidiateAssisstance = model.WhatArrangementsMadeForImmediateAssistance,
                    NatureOfAssistance = model.NatureOfAssistance,
                    SoughtAt = model.SoughtAt,
                    ProvidedAt = model.ProvidedAt,
                    NameOfConsultant = model.NameOfConsultant,
                    LocationOfConsultant = model.LocationOfConsultant,
                    Contact = model.Contact,
                    Notes = model.Notes,
                    ConductedAt = model.ConductedAt,
                    StartedAt = model.StartedAt,
                    EndedAt = model.EndedAt,
                    CreatedAt = DateTime.Now,
                    CreatedBy = User?.Identity.Name,
                    Active = 1
                };
                // Add and save to the database
                _context.PsychologicalAssisstances.Add(assistance);
                await _context.SaveChangesAsync();


            if(model.ChildPsychologicalAssistances.Count > 0)
                foreach (var item in model.ChildPsychologicalAssistances)
                {
                    var childs = new ChildpsychologicalAssistance
                    {
                        ReferenceNo = model.ReferenceNo,
                        Name = item.Name,
                        ChildAge = item.ChildAge,
                        PsychologicalAssistanceId = assistance.Id,
                        CreatedAt = DateTime.Now,
                        CreatedBy = User?.Identity.Name
                    
                    };
                    _context.ChildPsychologicalAssistance.Add(childs);
                    await _context.SaveChangesAsync();
                }
                return Ok(new { data = model });

        }



        [HttpGet("delete")]
        public async Task<IActionResult> DeletePsychologicalAssisstance(int id)
        {
            // Find the user by ID
            var PsychologicalAssisstance = await _context.PsychologicalAssisstances.FindAsync(id);

            if (PsychologicalAssisstance == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "PsychologicalAssisstances not found." });
            }

            // Remove the user from the database
            _context.PsychologicalAssisstances.Remove(PsychologicalAssisstance);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "PsychologicalAssisstances deleted successfully." });
        }


    


    }
}
