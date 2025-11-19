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
    public class AbuserController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ILegalAssistanceService _legalService;

        public AbuserController(DastakDbContext context, ILegalAssistanceService legalService)
        {
            _context = context;
            _legalService = legalService;
        }
       



        [HttpGet("getabuse")]
        public IActionResult Getabuse(string file, string entity)
        {
   
            // Creating a dynamic object for data
            dynamic data = new ExpandoObject();

            // Setting the entity
            data.entity = entity;

            // Fetching 'discharged' from Entity model where reference_no matches the entity
            var dischargedEntity = _context.Parents
                .Where(e => e.ReferenceNo == entity)
                .Select(e => new { e.Discharged })
                .FirstOrDefault();

            data.discharged = dischargedEntity?.Discharged;

            // Setting the file
            data.file = file;

            // Fetching abuser data
            var abusers = _context.AllegedAbusers
                .Where(a => a.ReferenceNo == entity && a.Active == 1)
                .OrderByDescending(a => a.Id)
                .Select(a => new
                {
                    a.Id,
                    Name = a.AbuserName,
                    Type = a.TypeOfAbuse,
                    a.Address,
                    Relation = a.Relationship,
                    a.Contact
                })
                .ToList();

            // Decoding JSON type of abuse field (if it's stored as JSON)
            foreach (var abuser in abusers)
            {
                data.Type = JsonConvert.DeserializeObject<List<string>>(abuser.Type);
            }

            // Adding abuser data to the dynamic object
            data.abuser = abusers;

            // Returning the view with user data and the dynamic object
            return Ok(new { data });
        }



        [HttpGet ("getabuserview")]
        public async Task<IActionResult> Getabuserview(string entity,int id)
        {
           


                var data = await _context.AllegedAbusers
                    .Where(a => a.Id == id && a.ReferenceNo == entity)
                    .FirstOrDefaultAsync();

                return Ok( new {data });
        }


        [HttpGet ("getabuseradd")]
        public IActionResult getabuseradd(string file, string entity)
        {
         

            var data = _context.Parents
                .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                .Select(e => new
                {
                    e.FileNo,
                    e.ReferenceNo,
                    e.Title,
                    e.FirstName,
                    e.LastName
                })
                .FirstOrDefault();

            return Ok(new { data }); // Pass the ViewModel to the view
        }

        [HttpPost ("postabuseradd")]
        public IActionResult postabuseradd( AllegedAbuserViewModel viewModel)
        {
            var Email = User?.Identity.Name;

            var abuser = new AllegedAbuser
            {
                ReferenceNo = viewModel.ReferenceNo,
                ResidentName = viewModel.ResidentName,
                AbuserName = viewModel.AbuserName,
                FatherName = viewModel.FatherName,
                TypeOfAbuse = viewModel.TypeOfAbuse,   //!= null ? JsonConvert.SerializeObject(viewModel.TypeOfAbuse) : null,
                TypeOfEconomicAbuse = viewModel.TypeOfEconomicAbuse,   // != null ? JsonConvert.SerializeObject(viewModel.TypeOfEconomicAbuse) : null,
                ReasonOfInflictingAbuse = viewModel.ReasonOfInflictingAbuse,  // != null ? JsonConvert.SerializeObject(viewModel.ReasonOfInflictingAbuse) : null,
                ReasonOfToleratingAbuse = viewModel.ReasonForToleratingAbuse  ,// != null ? JsonConvert.SerializeObject(viewModel.ReasonForToleratingAbuse) : null,
                NatureOfPhysicalAbuse = viewModel.NatureOfPhysicalAbuse,  // != null ? JsonConvert.SerializeObject(viewModel.NatureOfPhysicalAbuse) : null,
                NatureOfBodilyInjury = viewModel.NatureOfBodilyInjury,  // != null ? JsonConvert.SerializeObject(viewModel.NatureOfBodilyInjury) : null,
                SexualAbuseInflictedBy = viewModel.SexualAbuseInflictedBy,  // != null ? JsonConvert.SerializeObject(viewModel.SexualAbuseInflictedBy) : null,
                NatureOfSexualAbuse = viewModel.NatureOfSexualAbuse ,  //!= null ? JsonConvert.SerializeObject(viewModel.NatureOfSexualAbuse) : null,
                HasSufferedVerbalAbuse = viewModel.HasSufferedVerbalAbuse ?? 0,
                TypeOfVerbalAbuse = viewModel.TypeOfVerbalAbuse ,  //!= null ? JsonConvert.SerializeObject(viewModel.TypeOfVerbalAbuse) : null,
                HasBeenThreatened = viewModel.HasBeenThreatened ?? 0,
                NatureOfThreats = viewModel.NatureOfThreats , //!= null ? JsonConvert.SerializeObject(viewModel.NatureOfThreats) : null,
                Address = viewModel.Address,
                Address2 = viewModel.Address2,
                City = viewModel.City,
                Country = viewModel.Country,
                Contact = viewModel.Contact,
                Relationship = viewModel.Relationship,
                RelationDuration = viewModel.RelationDuration,
                Qualification = viewModel.Qualification,
                Profession = viewModel.Profession,
                DetailOfAttemptedAbuse = viewModel.DetailOfAttemptedAbuse,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Email,
            Active = 1
            };

            _context.AllegedAbusers.Add(abuser);
            _context.SaveChanges();

            return Ok(new { data = viewModel });
        }


        [HttpGet ("gettdeleteabuser")]
        public IActionResult getdeleteabuser(int id)
        {
         

            // Find the abuser record by ID and deactivate it
            var abuser = _context.AllegedAbusers.FirstOrDefault(a => a.Id == id);
            if (abuser != null)
            {
                abuser.Active = 0;
               // abuser.DeactivatedBy = userData.Email;
                abuser.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();
            }

            // Redirect to the index page
            return Ok( new { message = "Delete Successfully..!" });
        }







      

    }
}
