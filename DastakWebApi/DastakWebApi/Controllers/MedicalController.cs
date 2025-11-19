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
using System.Linq;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MedicalController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ICallersService _callService;

        public MedicalController(DastakDbContext context, ICallersService callService)
        {
            _context = context;
            _callService = callService;
        }

        [HttpGet("getmedical")]
        public async Task<IActionResult> getmedical(string file, string entity)
        {
           

            var data = new MedicalDataViewModel();

            // Get Entity Data
            var entityDischarged = await _context.Parents
                .Where(e => e.ReferenceNo == entity)
                .Select(e => e.Discharged)
                .FirstOrDefaultAsync();

            data.Entity = entity;
            data.Discharged = entityDischarged;

            // Get Medical Assistance Data
            var medicalData = await (from medicalAssisstance in _context.MedicalAssisstances
                                     join medicalHistory in _context.MedicalHistories
                                     on medicalAssisstance.CaseId equals medicalHistory.CaseId
                                     where medicalAssisstance.ReferenceNo == entity && medicalAssisstance.Active == 1
                                     orderby medicalAssisstance.Id descending
                                     select new MedicalViewModel
                                     {
                                         Id = medicalAssisstance.Id,
                                         CaseId = medicalAssisstance.CaseId,
                                         Name = medicalAssisstance.NameOfDoctorAssisting,
                                         Contact = medicalAssisstance.ContactNo,
                                         Brief = medicalHistory.BriefOfHistory,
                                         CreatedAt = medicalHistory.CreatedAt
                                     }).ToListAsync();

            data.File = file;
            data.Medical = medicalData;

            return Ok( new {  data });
        }


        [HttpGet ("getmedicaladd")]
        
        public async Task<IActionResult> getmedicaladd(string file, string entity)
        {
            

            var data = await _context.Parents
                .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                .Select(e => new { e.FileNo, e.ReferenceNo, e.Title, e.FirstName, e.LastName })
                .FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }

            return Ok( new {  data });
        }

        [HttpGet("getmedicalview")]
        public IActionResult getmedicalview(string file, string entity, string caseId)
        {
            // Instantiate UserController and retrieve user data
        
            // Initialize a dynamic object for data
            dynamic data = new ExpandoObject();

            // Retrieve 'info' data from Entity model
            data.info = _context.Parents
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

            // Retrieve 'data' from History model with a join on MedicalAssistance
            data.medical = _context.MedicalHistories
                .Join(_context.MedicalAssisstances,
                      h => h.ReferenceNo,
                      ma => ma.ReferenceNo,
                      (h, ma) => new { h, ma })
                .Where(joined => joined.h.ReferenceNo == entity && joined.h.CaseId == caseId)
                .Select(joined => joined)
                .FirstOrDefault();

            // Return view with user and data
            return Ok( new {  data });
        }



        [HttpPost("postmedicaladd")]
        public async Task<IActionResult> postmedicaladd(MedicalViewModell model)
        {


            var history = new MedicalHistory
            {

                ReferenceNo = model.ReferenceNo,
                NameOfResident = model.NameOfResident,
                BriefOfHistory = model.BriefOfHistory,// !string.IsNullOrEmpty(model.BriefOfHistory) ? JsonConvert.SerializeObject(model.BriefOfHistory) : null,
                NatureOfChronicIllness = model.NatureOfChronicIllness,//!string.IsNullOrEmpty(model.NatureOfChronicIllness) ? JsonConvert.SerializeObject(model.NatureOfChronicIllness) : null,
                SubstancesInDrugAbused = model.SubstancesInDrugAbused,//!string.IsNullOrEmpty(model.SubstancesInDrugAbused) ? JsonConvert.SerializeObject(model.SubstancesInDrugAbused) : null,
                IntensityOfAbuse = model.IntensityOfAbuse,//!string.IsNullOrEmpty(model.IntensityOfAbuse) ? JsonConvert.SerializeObject(model.IntensityOfAbuse) : null,
                IsCurrentlySubstanceAbuser = model.IsCurrentlySubstanceAbuser,
                IntensityOfCurrentAbuse = model.IntensityOfCurrentAbuse,//!string.IsNullOrEmpty(model.IntensityOfCurrentAbuse) ? JsonConvert.SerializeObject(model.IntensityOfCurrentAbuse) : null,
                CurrentMedicalPrescription = model.CurrentMedicalPrescription,
                ExpectedDeliveryDate = model.ExpectedDeliveryDate,
                CreatedAt = DateTime.Now,
               CreatedBy = User?.Identity.Name,
                Active = 1
            };

            _context.MedicalHistories.Add(history);
            await _context.SaveChangesAsync();

            var maxId = await _context.MedicalHistories
                .Where(h => h.ReferenceNo == history.ReferenceNo)
                .OrderByDescending(h => h.Id)
                .Select(h => h.Id)
                .FirstOrDefaultAsync();

            var caseKey = $"{history.ReferenceNo}-{maxId}";
            history.CaseId = caseKey;
            _context.MedicalHistories.Update(history);
            await _context.SaveChangesAsync();


            var assistance = new MedicalAssisstance
            {
                ReferenceNo = model.ReferenceNo,
                NameOfResident = model.NameOfResident,
                DateWhenSought = model.DateWhenSought,
                NameOfDoctorAssisting = model.NameOfDoctorAssisting,
                NameOfClinicAssisting = model.NameOfClinicAssisting,
                MedicalAssistanceProvidedTo=model.MedicalAssistanceProvidedTo,
                Complaint = model.Complaint,
                Diagnosis = model.Diagnosis,
                City = model.City,
                Country = model.Country,
                Address = model.Address,
                ContactNo = model.ContactNo,
                ContactNo2 = model.ContactNo2,
                TreatmentSuggested = model.TreatmentSuggested,
                Notes = model.Notes,
                ShelterAgreedToConductTests = model.ShelterAgreedToConductTests,
                DetailOfTest = model.DetailOfTest,
                CreatedAt = DateTime.Now,
               CreatedBy = User?.Identity.Name,
                Active = 1,
                CaseId = caseKey
            };

            _context.MedicalAssisstances.Add(assistance);
            await _context.SaveChangesAsync();
                foreach (var item in model.ChildMedicalAssistances)
                {
                    var childs = new ChildMedicalAssistance
                    {
                        ReferenceNo = model.ReferenceNo,
                        Name = item.Name,
                        Age = item.Age,
                        MedicalAssistanceId = assistance.Id,
                        CreatedAt = DateTime.Now,
                        CreatedBy = User?.Identity.Name

                    };
                    _context.ChildMedicalAssistance.Add(childs);
                    await _context.SaveChangesAsync();
                }

            

            return Ok( new { data = model });
        }




       


     
      


        [HttpPost("postphysicaladd")]
        public async Task<IActionResult> postphysicaladd(PsychologicalAssistanceViewModel model )
        {
            

            if (ModelState.IsValid)
            {
                var assistance = new PsychologicalAssisstance
                {
                    ReferenceNo = model.ReferenceNo,
                    NameOfResident = model.NameOfResident,
                    Age = model.Age,
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
                  //  CreatedBy = userData.Email,
                    Active = 1
                };

                // Add and save to the database
                _context.PsychologicalAssisstances.Add(assistance);
                await _context.SaveChangesAsync();

            }

            return Ok(new { data = model });
        }

        [HttpGet("deletemedical")]
        public async Task<IActionResult> deletemedical(int id)
        {
            // Find the user by ID
            var MedicalAssisstance = await _context.MedicalAssisstances.FindAsync(id);

            if (MedicalAssisstance == null)
            {
                // Return a 404 if the user is not found
                return NotFound(new { message = "MedicalAssisstance not found." });
            }

            // Remove the user from the database
            _context.MedicalAssisstances.Remove(MedicalAssisstance);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return a 200 OK response with a success message
            return Ok(new { message = "MedicalAssisstances deleted successfully." });
        }





    }
}
