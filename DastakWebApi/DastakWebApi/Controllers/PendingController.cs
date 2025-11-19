using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PendingController : ControllerBase
    {
        private readonly DastakDbContext _context;

        public PendingController(DastakDbContext context)
        {
            _context = context;
        }
        [HttpGet("pending")]
        public async Task<IActionResult> Pending(string? entity = null)
        {

            // Query for data depending on the 'entity' value (reference_no)
            var query = _context.Files
                .Where(f => f.Active == 1)
                .Join(
                    _context.Parents.Where(p => p.Active == 1 && p.Pending == 1),
                    file => file.FileNo,
                    parent => parent.FileNo,
                    (file, parent) => new
                    {
                        file.Id,
                        file.FileNo,
                        parent.ReferenceNo,
                        parent.AssessmentRisk,

                        parent.Title,
                        parent.FirstName,

                        parent.LastName,
                        Status = "Pending File",



                    })
                .Join(
        _context.BasicInfos, // Joining with BasicInfo table
        combined => combined.ReferenceNo,
        basicInfo => basicInfo.ReferenceNo,
        (combined, basicInfo) => new
        {
            combined.Id,
            combined.FileNo,
            combined.ReferenceNo,
            combined.AssessmentRisk,
            combined.Title,
            combined.FirstName,
            combined.LastName,
            combined.Status,
            City = basicInfo.City // Adding City from BasicInfo
        }
    );

            // If 'entity' is provided, filter the query by 'reference_no'
            if (!string.IsNullOrEmpty(entity))
            {
                query = query.Where(p => p.ReferenceNo == entity);
            }

            // Fetch data and order by 'id' in descending order
            var data = query.OrderByDescending(f => f.Id).ToList();

            // Check if no data is found
            if (!data.Any())
            {
                return Ok(new { data = (object?)null });
            }

            // Return data along with user info
            return Ok(new { data = data });
        }

        [HttpGet("getpendingdata")]
        public async Task<IActionResult> Add(string file, string entity)
        {


            var data = await _context.Parents
                .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                .Select(e => new
                {
                    e.FileNo,
                    e.ReferenceNo,
                    e.Title,
                    FullName = e.FirstName + " " + e.LastName
                })
                .ToListAsync();

            if (data == null)
            {
                return NotFound();
            }


            return Ok(new { data });
        }
        [HttpGet("getpendingname")]
        public async Task<ActionResult> getpendingname(string file, string entity)
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
        // POST: api/Entity/DiscardPending
        [HttpGet("deletepending")]
        public async Task<IActionResult> deletepending(string file, string entity)
        {

            // Find the entity based on file number and reference number
            var parent = await _context.Parents
                .FirstOrDefaultAsync(e => e.FileNo == file && e.ReferenceNo == entity);

            if (parent == null)
            {
                return NotFound("Entity not found.");
            }

            // Update the entity fields
            parent.Pending = 0;
            //parent.DiscardedBy = currentUser; // assuming 'currentUser' is the user's email or ID

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a redirect or success response
            return Ok("Parent updated and pending status discarded.");
        }
        [HttpPost("postproceeddata")]
        public async Task<IActionResult> ProceedAdmission(ProceedAdmissionViewModel model)
        {

            string flag = "";

            if (model.IsAdmitted == true)
            {
                flag = "1";
                if (!model.IsReadmission)
                {
                    //model.FileNo = !string.IsNullOrEmpty(model.AdmissionDate)
                    //    ? model.AdmissionDate + "-" + model.FileNo
                    //    : DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + model.FileNo;

                    //model.ReferenceNo = model.FileNo + "-1";

                    var file = _context.Files.FirstOrDefault(f => f.FileNo == model.FileNo);
                    if (file != null)
                    {
                        file.FileNo = model.FileNo;
                        _context.Files.Update(file);
                    }

                    var entity = _context.Parents.FirstOrDefault(e => e.FileNo == model.FileNo && e.ReferenceNo == model.ReferenceNo);
                    if (entity != null)
                    {
                        entity.FileNo = model.FileNo;
                        entity.ReferenceNo = model.ReferenceNo;
                        entity.Pending = 0;
                        entity.IsAdmitted = 1;
                        entity.AdmissionAt = DateTime.Parse(model.AdmissionDate);
                        entity.UpdatedAt = DateTime.Now;
                        entity.UpdatedBy = User?.Identity.Name;

                        _context.Parents.Update(entity);
                    }
                }
                else if (model.IsReadmission)
                {
                    //model.ReferenceNo = !string.IsNullOrEmpty(model.AdmissionDate)
                    //    ? model.AdmissionDate + "-" + model.ReferenceNo
                    //    : DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + model.ReferenceNo;

                    //model.ReferenceNo = model.ReferenceNo + "-1";
                    //model.ReferenceNo

                    var entity = _context.Parents.FirstOrDefault(e => e.FileNo == model.FileNo && e.ReferenceNo == model.ReferenceNo);
                    if (entity != null)
                    {
                        entity.ReferenceNo = model.ReferenceNo;
                        entity.Pending = 0;
                        entity.IsAdmitted = 1;
                        entity.AdmissionAt = DateTime.Parse(model.AdmissionDate);
                        entity.UpdatedAt = DateTime.Now;
                        entity.UpdatedBy = User?.Identity.Name;

                        _context.Parents.Update(entity);

                        _context.Parents.Where(e => e.FileNo == model.FileNo && e.ReferenceNo != model.ReferenceNo)
                            .ToList()
                            .ForEach(e => e.Active = 0);
                    }
                }

                UpdateReferenceNos(model.ReferenceNo);

                SaveAdmissionDetails(model, model.ReferenceNo);

                return Ok(new { fileNo = model.FileNo, referenceNo = model.ReferenceNo });
            }
            else
            {
                var entity = _context.Parents.FirstOrDefault(e => e.FileNo == model.FileNo && e.ReferenceNo == model.ReferenceNo);
                if (entity != null)
                {
                    entity.Pending = 0;
                    entity.IsAdmitted = 0;

                    _context.Parents.Update(entity);
                }

                return Ok(new { data = model });
            }
        }



        private void UpdateReferenceNos(string referenceNo)
        {
            _context.BasicInfos.Where(b => b.ReferenceNo == referenceNo).ToList().ForEach(b => b.ReferenceNo = referenceNo);
            _context.MaritalInfos.Where(m => m.ReferenceNo == referenceNo).ToList().ForEach(m => m.ReferenceNo = referenceNo);
            _context.ReferencesRecords.Where(r => r.ReferenceNo == referenceNo).ToList().ForEach(r => r.ReferenceNo = referenceNo);
        }

        private void SaveAdmissionDetails(ProceedAdmissionViewModel model, string referenceNo)
        {
            var admission = new AdmissionRecord
            {
                ReferenceNo = referenceNo,

                ReasonForAdmission = model.ReasonForAdmission,
                NatureOfAssisstance = model.NatureOfAssistance,
                IsAbused = model.IsAbused,
                InterviewDate = DateTime.Parse(model.InterviewDate),
                AdmissionDate = DateTime.Parse(model.AdmissionDate),
                ReasonOfRefuse = model.ReasonOfRefuse,
                IsReferedToOtherShelter = model.IsReferredToOtherShelter,
                WhereHasSheRefered = model.WhereHasSheReferred
            };

            _context.AdmissionRecords.Add(admission);
            _context.SaveChanges();

            var contact = new ContactsInfo
            {
                ReferenceNo = referenceNo,
                FamilyPhone = model.FamilyPhone,
                FamilyName = model.FamilyName,
                FamilyRelation = model.FamilyRelation,
                EmergencyPhone = model.EmergencyPhone,
                EmergencyName = model.EmergencyName,
                EmergencyRelation = model.EmergencyRelation

            };

            _context.ContactsInfos.Add(contact);
            _context.SaveChanges();

            var document = new Document
            {
                ReferenceNo = referenceNo,
                ListOfDocuments = model.ListOfDocuments,// != null ? string.Join(",", model.ListOfDocuments) : null,
                Photocopied = model.Photocopied
            };

            _context.Documents.Add(document);
            _context.SaveChanges();

            var disease = new CommunicableDisease
            {
                ReferenceNo = referenceNo,
                HasScreened = model.HasScreened,
                Diseases = model.Diseases != null ? string.Join(",", model.Diseases) : null
            };

            _context.CommunicableDiseases.Add(disease);
            _context.SaveChanges();

            var possession = new Possession
            {
                ReferenceNo = referenceNo,
                Items = model.Items,// != null ? string.Join(",", model.Items) : null,
                Quantities = model.Quantities,// != null ? string.Join(",", model.Quantities) : null,
                InPossesstionOf = model.InPossessionOf,// != null ? string.Join(",", model.InPossessionOf) : null,
                HasSignedAuthorizationLetter = model.HasSignedAuthorizationLetter,
            };

            _context.Possessions.Add(possession);
            _context.SaveChanges();

            var additional = new AdditionalDetail
            {
                ReferenceNo = referenceNo,
                Details = model.Details
            };

            _context.AdditionalDetails.Add(additional);
            _context.SaveChanges();

            var orientation = new Orientation
            {
                ReferenceNo = referenceNo,
                HasBeenOriented= (short?)(model.HasBeenOriented == true ? 1 : 0),
                GivenCopyOfRules = (short?)(model.GivenCopyOfRules == true ? 1 : 0),
                EnsuredConfidentialityOfData = (short?)(model.EnsuredConfidentialityOfData == true ? 1 : 0),
                GivenCopyOfRights = (short?)(model.GivenCopyOfRights == true ? 1 : 0),
            };

            _context.Orientations.Add(orientation);
            _context.SaveChanges();
        }
    }

}

