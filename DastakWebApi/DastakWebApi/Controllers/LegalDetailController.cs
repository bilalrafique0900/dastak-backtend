using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LegalDetailController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ILegalAssistanceService _legalService;

        public LegalDetailController(DastakDbContext context, ILegalAssistanceService legalService)
        {
            _context = context;
            _legalService = legalService;
        }
        [HttpGet("getlegalassistance")]
        public async Task<IActionResult> GetLegalAssistance()
        {
            // Querying the active users from the database
            var data = await _context.LegalAssistances
                .Where(u => u.Active == 1)
                .Select(u => new
                {
                    u.Id,
                    u.CreatedAt,
                    u.CaseId,
                    u.CaseRef,
                    u.NextDateOfHearing,
                    u.Court,
                    u.NatureOfLegalConcern,
                    u.StatusOfCase,
                    

                })
                .OrderByDescending(u => u.Id)
                .ToListAsync();

            return Ok(new { data });
        }

        [HttpGet("getlegalassistancebyreferenceno")]
        public async Task<IActionResult> GetLegalAssistanceByReference(string? entity)
        {
            if (!string.IsNullOrEmpty(entity))
            {
                var data = await (from la in _context.LegalAssistances
                                  join p in _context.Parents on la.ReferenceNo equals p.ReferenceNo
                                  where la.Active == 1 && la.ReferenceNo == entity
                                  orderby la.Id descending
                                  select new
                                  {
                                      la.Id,
                                      la.CreatedAt,
                                      la.CaseId,
                                      la.CaseRef,
                                      la.NextDateOfHearing,
                                      la.Court,
                                      la.NatureOfLegalConcern,
                                      la.StatusOfCase,
                                      FileNo = p.FileNo,
                                      ReferenceNo = p.ReferenceNo
                                  }).ToListAsync();

                return Ok(new { data });
            }
            else
            {
                var data = await (from la in _context.LegalAssistances
                                  join p in _context.Parents on la.ReferenceNo equals p.ReferenceNo
                                  where la.Active == 1
                                  orderby la.Id descending
                                  select new
                                  {
                                      la.Id,
                                      la.CreatedAt,
                                      la.CaseId,
                                      la.CaseRef,
                                      la.NextDateOfHearing,
                                      la.Court,
                                      la.NatureOfLegalConcern,
                                      la.StatusOfCase,
                                      FileNo = p.FileNo,
                                      ReferenceNo = p.ReferenceNo
                                  }).ToListAsync();

                return Ok(new { data });
            }
        }
        [HttpGet("getlegalassistancebyid")]
        public IActionResult GetById(int id)
        {
            // Get the logged-in user details (assuming UserService returns current user data)

            // var userData = _userService.GetCurrentUserData(id);

            // Fetch the user by id
            var alllegal = _legalService.GetLegalAssistanceById(id);

            if (alllegal == null || alllegal.Active != 1)
            {
                return NotFound("AllLegals not found or inactive.");
            }

            // Passing data to the view
            var data = alllegal;

            return Ok(new { data });
        }

            [HttpGet("getviewbasic")]
            public async Task<IActionResult> View(string file, string entity)
            {
                var parentEntity = await _context.Parents
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.FileNo == file && m.ReferenceNo == entity);

                if (parentEntity == null)
                    return NotFound("Parent not found.");

                var refNo = parentEntity.ReferenceNo;

                var basicInfo = await _context.BasicInfos.AsNoTracking().FirstOrDefaultAsync(m => m.ReferenceNo == refNo);
                var maritalInfo = await _context.MaritalInfos.AsNoTracking().FirstOrDefaultAsync(m => m.ReferenceNo == refNo);
                var referenceRecord = await _context.ReferencesRecords.AsNoTracking().FirstOrDefaultAsync(m => m.ReferenceNo == refNo);
                var admissionRecord = await _context.AdmissionRecords.AsNoTracking().FirstOrDefaultAsync(m => m.ReferenceNo == refNo);
                var contactInfo = await _context.ContactsInfos.AsNoTracking().FirstOrDefaultAsync(m => m.ReferenceNo == refNo);
                var document = await _context.Documents.AsNoTracking().FirstOrDefaultAsync(m => m.ReferenceNo == refNo);
                var possession = await _context.Possessions.AsNoTracking().FirstOrDefaultAsync(m => m.ReferenceNo == refNo);
                var communicableDisease = await _context.CommunicableDiseases.AsNoTracking().FirstOrDefaultAsync(m => m.ReferenceNo == refNo);
                var orientation = await _context.Orientations.AsNoTracking().FirstOrDefaultAsync(m => m.ReferenceNo == refNo);
                var additionalDetail = await _context.AdditionalDetails.AsNoTracking().FirstOrDefaultAsync(m => m.ReferenceNo == refNo);

                var viewModel = new
                {
                    Parent = parentEntity,
                    BasicInfo = basicInfo,
                    MaritalInfo = maritalInfo,
                    ReferenceRecord = referenceRecord,
                    AdmissionRecord = admissionRecord,
                    ContactInfo = contactInfo,
                    Document = document,
                    Possession = possession,
                    CommunicableDisease = communicableDisease,
                    Orientation = orientation,
                    AdditionalDetail = additionalDetail
                };

                return Ok(new { data = viewModel });
            }


            //[HttpGet("getviewbasic")]
            //public async Task<IActionResult> View(string file, string entity)
            //{
            //    // Retrieve the main parent entity
            //    var parentEntity = await _context.Parents.AsNoTracking().Where(m => m.FileNo == file && m.ReferenceNo == entity).FirstOrDefaultAsync();
            //    if (parentEntity == null)
            //    {
            //        return NotFound("Parent not found.");
            //    }

            //    // Retrieve related data using navigation properties or separate queries
            //    var basicInfo = await _context.BasicInfos.AsNoTracking().Where(m => m.ReferenceNo == parentEntity.ReferenceNo).FirstOrDefaultAsync();

            //    var maritalInfo = await _context.MaritalInfos.AsNoTracking().Where(m => m.ReferenceNo == parentEntity.ReferenceNo).FirstOrDefaultAsync();

            //    var referenceRecord = await _context.ReferencesRecords.AsNoTracking().Where(m => m.ReferenceNo == parentEntity.ReferenceNo).FirstOrDefaultAsync();
            //    var admissionRecord = await _context.AdmissionRecords.AsNoTracking().Where(m => m.ReferenceNo == parentEntity.ReferenceNo).FirstOrDefaultAsync();

            //    var contactInfo = await _context.ContactsInfos.AsNoTracking().Where(m => m.ReferenceNo == parentEntity.ReferenceNo).FirstOrDefaultAsync();
            //    var document = await _context.Documents.AsNoTracking().Where(m => m.ReferenceNo == parentEntity.ReferenceNo).FirstOrDefaultAsync();

            //    var possession = await _context.Possessions.AsNoTracking().Where(m => m.ReferenceNo == parentEntity.ReferenceNo).FirstOrDefaultAsync();

            //    var communicableDisease = await _context.CommunicableDiseases.AsNoTracking().Where(m => m.ReferenceNo == parentEntity.ReferenceNo).FirstOrDefaultAsync();

            //    var orientation = await _context.Orientations.AsNoTracking().Where(m => m.ReferenceNo == parentEntity.ReferenceNo).FirstOrDefaultAsync();
            //    var additionalDetail = await _context.AdditionalDetails.AsNoTracking().Where(m => m.ReferenceNo == parentEntity.ReferenceNo).FirstOrDefaultAsync();

            //    // Create a ViewModel to organize the data
            //    var viewModel = new
            //    {
            //        Parent = parentEntity,
            //        BasicInfo = basicInfo,
            //        MaritalInfo = maritalInfo,
            //        ReferenceRecord = referenceRecord,
            //        AdmissionRecord = admissionRecord,
            //        ContactInfo = contactInfo,
            //        Document = document,
            //        Possession = possession,
            //        CommunicableDisease = communicableDisease,
            //        Orientation = orientation,
            //        AdditionalDetail = additionalDetail
            //    };

            //    return Ok(new { data = viewModel });

            //}



            [HttpGet("get-deactivated")]
        public async Task<IActionResult> Deactivated()
        {
            // Fetching data from the database, including the DepartureDate from the Discharge table
            var data = _context.Parents
                .Join(_context.Discharges,
                      parent => parent.ReferenceNo,
                      discharge => discharge.ReferenceNo,
                      (parent, discharge) => new
                      {
                          Parent = parent,
                          Discharge = discharge
                      }).Join(
        _context.BasicInfos, // Joining with BasicInfo table
        combined => combined.Parent.ReferenceNo,
        basicInfo => basicInfo.ReferenceNo,
        (combined, basicInfo) => new
        {
           Parent= combined.Parent,
           Discharge=combined.Discharge,
            City = basicInfo.City // Adding City from BasicInfo
        }
    ).Where(e => e.Parent.Active == 1 && e.Parent.IsAdmitted == 1 && e.Parent.Discharged == 1)
                .OrderByDescending(e => e.Parent.Id)
                .Take(50)
                .Select(e => new
                {
                    e.Parent.Id,
                    e.Parent.ReferenceNo,
                    e.Parent.FileNo,
                    e.Parent.FirstName,
                    e.Parent.LastName,
                    e.Parent.IsAdmitted,
                    e.Parent.AssessmentRisk,
                    e.Parent.Discharged,
                    e.Discharge.DischargeDate, // Including the DepartureDate from the Discharge table
                    Status = "Closed File",
                    City=e.City
                })
                .ToList();

            // Processing each entity (commented out section in your original code)
            foreach (var obj in data)
            {
                if (obj != null)
                {
                    var parent =await _context.Parents
                        .Where(p => p.ReferenceNo == obj.ReferenceNo)
                        .OrderByDescending(p => p.Id)
                        .FirstOrDefaultAsync();

                    // Further processing if necessary
                }
            }

            // Returning the result with the fetched data and user info
            return Ok(new { data });
        }

        [HttpGet("getfeedback")]
        public async Task<IActionResult> Index(string ReferenceNo)
        {

            var data = _context.Parents
                .Where(p => p.ReferenceNo == ReferenceNo)
                .Select(p => new
                {
                    Parent = p,
                    Orientation = _context.Orientations.FirstOrDefault(o => o.ReferenceNo == p.ReferenceNo),
                    Discharge = _context.Discharges.FirstOrDefault(d => d.ReferenceNo == p.ReferenceNo),
                    Feedback = _context.Feedbacks.FirstOrDefault(f => f.ReferenceNo == p.ReferenceNo),
                    Meetings = _context.Meetings.FirstOrDefault(m => m.ReferenceNo == p.ReferenceNo),
                    ContactsInfo = _context.ContactsInfos.FirstOrDefault(ci => ci.ReferenceNo == p.ReferenceNo)
                })
                .FirstOrDefault();

            // Check if the data exists
            if (data == null)
            {
                return NotFound(); // Handle case when no data is found
            }

            // Returning the view with the fetched data and user info
            return Ok( new
            { data
            });
        }
        [HttpGet("getlegal")]
        public async Task<ActionResult> GetLegal(string file, string entity)
        {


     
                var data = _context.Parents
                    .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                    .Select(e => new
                    {
                        e.FileNo,
                        e.ReferenceNo,
                        e.Title,
                        e.FirstName,
                        e.LastName,
                        FullName=e.FirstName+" "+e.LastName
                    })
                    .FirstOrDefault();

                return Ok(new { data });
        }
        [HttpPost("addlegal")]
        public async Task<ActionResult> Add(LegalNoticeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var notice = new LegalNotice();

                notice.ReferenceNo = model.ReferenceNo;
                notice.LegalAdviceSought = model.LegalAdviceSought;
                notice.LegalAssistanceSought = model.LegalAssistanceSought;
                notice.LegalNoticeSent = model.LegalNoticeSent;
                notice.DateWhenLegalNoticeSent = model.DateWhenLegalNoticeSent;
                notice.LegalNoticeSentTo = model.LegalNoticeSentTo;
                notice.CreatedAt = DateTime.Now;
                notice.CreatedBy = User?.Identity.Name;
                notice.Active = 1;

                _context.LegalNotices.Add(notice);
              await  _context.SaveChangesAsync();

                // Get the ID of the newly inserted notice
                var id = _context.LegalNotices
                    .Where(n => n.ReferenceNo == notice.ReferenceNo)
                    .OrderByDescending(n => n.Id)
                    .Select(n => n.Id)
                    .FirstOrDefault();

                var key = notice.ReferenceNo + "-" + id;

                // Update notice with case_id
                var noticeToUpdate = _context.LegalNotices.Find(id);
                if (noticeToUpdate != null)
                {
                    noticeToUpdate.CaseId = key;
                    _context.Entry(noticeToUpdate).State = EntityState.Modified;
                  await  _context.SaveChangesAsync();
                }

                var assistance = new LegalAssistance();

                assistance.ReferenceNo = model.ReferenceNo;
                assistance.TypeOfAssistance = model.TypeOfAssistance ?? "New case";
                assistance.ReasonForWithdrawal = model.ReasonForWithdrawal;
                assistance.NatureOfLegalConcern = model.NatureOfLegalConcern;
                assistance.FirNo = model.FirNo;
                assistance.CaseNo = model.CaseNo;
                assistance.CaseFiledBy = model.CaseFiledBy;
                assistance.CaseFiledAgainst = model.CaseFiledAgainst;
                assistance.IsLawyerShelterAssigned = model.IsLawyerShelterAssigned;
                assistance.NameOfLawyer = model.NameOfLawyer;
                assistance.ContactOfLawyer = model.ContactOfLawyer;
                assistance.Court = model.Court;
                assistance.ProvinceOfCourt = model.ProvinceOfCourt;
                assistance.CityOfCourt = model.CityOfCourt;
                assistance.NextDateOfHearing = model.NextDateOfHearing;
                assistance.Remarks = model.Remarks;
                assistance.StatusOfCase = model.StatusOfCase;
                assistance.CaseId = key;
                assistance.CaseRef = key;
                assistance.CreatedAt = DateTime.Now;
                assistance.CreatedBy = User?.Identity.Name;
                assistance.Active = 1;

                _context.LegalAssistances.Add(assistance);
              await  _context.SaveChangesAsync();

                return Ok(new { data = model });
            }

            // If the model state is invalid, return the view with validation errors
            return Ok(new { data = model });
        }

        [HttpPost("editlegal")]
        public async Task<ActionResult> Edit(int id, LegalNoticeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the legal notice by id
                var noticeToUpdate = await _context.LegalNotices.FindAsync(id);
                if (noticeToUpdate == null)
                {
                    return NotFound(new { message = "Legal notice not found" });
                }

                // Update LegalNotice properties
                noticeToUpdate.ReferenceNo = model.ReferenceNo;
                noticeToUpdate.LegalAdviceSought = model.LegalAdviceSought;
                noticeToUpdate.LegalAssistanceSought = model.LegalAssistanceSought;
                noticeToUpdate.LegalNoticeSent = model.LegalNoticeSent;
                noticeToUpdate.DateWhenLegalNoticeSent = model.DateWhenLegalNoticeSent;
                noticeToUpdate.LegalNoticeSentTo = model.LegalNoticeSentTo;
                noticeToUpdate.UpdatedAt = DateTime.Now;
                noticeToUpdate.UpdatedBy = User?.Identity.Name;
                noticeToUpdate.Active = 1;

                _context.Entry(noticeToUpdate).State = EntityState.Modified;
               await _context.SaveChangesAsync();

                // Update LegalAssistance
                var assistanceToUpdate = _context.LegalAssistances
                    .FirstOrDefault(a => a.CaseId == noticeToUpdate.CaseId);

                if (assistanceToUpdate != null)
                {
                    assistanceToUpdate.ReferenceNo = model.ReferenceNo;
                    assistanceToUpdate.TypeOfAssistance = model.TypeOfAssistance ?? "New case";
                    assistanceToUpdate.ReasonForWithdrawal = model.ReasonForWithdrawal;
                    assistanceToUpdate.NatureOfLegalConcern = model.NatureOfLegalConcern;
                    assistanceToUpdate.FirNo = model.FirNo;
                    assistanceToUpdate.CaseNo = model.CaseNo;
                    assistanceToUpdate.CaseFiledBy = model.CaseFiledBy;
                    assistanceToUpdate.CaseFiledAgainst = model.CaseFiledAgainst;
                    assistanceToUpdate.IsLawyerShelterAssigned = model.IsLawyerShelterAssigned;
                    assistanceToUpdate.NameOfLawyer = model.NameOfLawyer;
                    assistanceToUpdate.ContactOfLawyer = model.ContactOfLawyer;
                    assistanceToUpdate.Court = model.Court;
                    assistanceToUpdate.ProvinceOfCourt = model.ProvinceOfCourt;
                    assistanceToUpdate.CityOfCourt = model.CityOfCourt;
                    assistanceToUpdate.NextDateOfHearing = model.NextDateOfHearing;
                    assistanceToUpdate.Remarks = model.Remarks;
                    assistanceToUpdate.StatusOfCase = model.StatusOfCase;
                    assistanceToUpdate.UpdatedAt = DateTime.Now;
                    assistanceToUpdate.UpdatedBy = User?.Identity.Name;
                    assistanceToUpdate.Active = 1;

                    _context.Entry(assistanceToUpdate).State = EntityState.Modified;
                   await _context.SaveChangesAsync();
                }

                return Ok(new { message = "Record updated successfully", data = model });
            }

            // If the model state is invalid, return the view with validation errors
            return BadRequest(new { message = "Invalid data", data = model });
        }


        [HttpPost("updatelegal")]
        public async Task<ActionResult> Update(int id, LegalNoticeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the legal notice by id
                var noticeToUpdate =await _context.LegalNotices.FindAsync(id);
                if (noticeToUpdate == null)
                {
                    return NotFound(new { message = "Legal notice not found" });
                }

                // Update LegalNotice properties
                noticeToUpdate.ReferenceNo = model.ReferenceNo;
                noticeToUpdate.LegalAdviceSought = model.LegalAdviceSought;
                noticeToUpdate.LegalAssistanceSought = model.LegalAssistanceSought;
                noticeToUpdate.LegalNoticeSent = model.LegalNoticeSent;
                noticeToUpdate.DateWhenLegalNoticeSent = model.DateWhenLegalNoticeSent;
                noticeToUpdate.LegalNoticeSentTo = model.LegalNoticeSentTo;
                noticeToUpdate.UpdatedAt = DateTime.Now; // New updated time
                noticeToUpdate.UpdatedBy = User?.Identity.Name; // Set if needed
                noticeToUpdate.Active = 1;

                _context.Entry(noticeToUpdate).State = EntityState.Modified;
             await   _context.SaveChangesAsync();

                // Update LegalAssistance
                var assistanceToUpdate = _context.LegalAssistances
                    .FirstOrDefault(a => a.CaseId == noticeToUpdate.CaseId);

                if (assistanceToUpdate != null)
                {
                    assistanceToUpdate.ReferenceNo = model.ReferenceNo;
                    assistanceToUpdate.TypeOfAssistance = model.TypeOfAssistance ?? "New case";
                    assistanceToUpdate.ReasonForWithdrawal = model.ReasonForWithdrawal;
                    assistanceToUpdate.NatureOfLegalConcern = model.NatureOfLegalConcern;
                    assistanceToUpdate.FirNo = model.FirNo;
                    assistanceToUpdate.CaseNo = model.CaseNo;
                    assistanceToUpdate.CaseFiledBy = model.CaseFiledBy;
                    assistanceToUpdate.CaseFiledAgainst = model.CaseFiledAgainst;
                    assistanceToUpdate.IsLawyerShelterAssigned = model.IsLawyerShelterAssigned;
                    assistanceToUpdate.NameOfLawyer = model.NameOfLawyer;
                    assistanceToUpdate.ContactOfLawyer = model.ContactOfLawyer;
                    assistanceToUpdate.Court = model.Court;
                    assistanceToUpdate.ProvinceOfCourt = model.ProvinceOfCourt;
                    assistanceToUpdate.CityOfCourt = model.CityOfCourt;
                    assistanceToUpdate.NextDateOfHearing = model.NextDateOfHearing;
                    assistanceToUpdate.Remarks = model.Remarks;
                    assistanceToUpdate.StatusOfCase = model.StatusOfCase;
                    assistanceToUpdate.UpdatedAt = DateTime.Now; // New updated time
                    assistanceToUpdate.UpdatedBy = User?.Identity.Name; // Set if needed
                    assistanceToUpdate.Active = 1;

                    _context.Entry(assistanceToUpdate).State = EntityState.Modified;
                   await _context.SaveChangesAsync();
                }

                return Ok(new { message = "Record updated successfully", data = model });
            }

            // If the model state is invalid, return the view with validation errors
            return BadRequest(new { message = "Invalid data", data = model });
        }
        [HttpPost("addCommunityConsultation")]
        public async Task<ActionResult> AddCommunityConsultation(CommunityConsultationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var community = new CommunityConsultation();
                community.Name = model.Name;
                community.ReferenceNo = model.ReferenceNo;
                community.LegalAdviceSought = model.LegalAdviceSought;
                community.LegalAssistanceSought = model.LegalAssistanceSought;
                community.LegalNoticeSent = model.LegalNoticeSent;
                community.DateWhenLegalNoticeSent = model.DateWhenLegalNoticeSent;
                community.LegalNoticeSentTo = model.LegalNoticeSentTo;
                community.TypeOfAssistance = model.TypeOfAssistance ?? "New case";
                community.ReasonForWithdrawal = model.ReasonForWithdrawal;
                community.NatureOfLegalConcern = model.NatureOfLegalConcern;
                community.FirNo = model.FirNo;
                community.CaseNo = model.CaseNo;
                community.CaseFiledBy = model.CaseFiledBy;
                community.CaseFiledAgainst = model.CaseFiledAgainst;
                community.IsLawyerShelterAssigned = model.IsLawyerShelterAssigned;
                community.NameOfLawyer = model.NameOfLawyer;
                community.ContactOfLawyer = model.ContactOfLawyer;
                community.Court = model.Court;
                community.ProvinceOfCourt = model.ProvinceOfCourt;
                community.CityOfCourt = model.CityOfCourt;
                community.NextDateOfHearing = model.NextDateOfHearing;
                community.Remarks = model.Remarks;
                community.StatusOfCase = model.StatusOfCase;
                community.CreatedAt = DateTime.Now;
                community.CreatedBy = User?.Identity.Name;
                community.Active = 1;

                _context.CommunityConsultation.Add(community);
                await _context.SaveChangesAsync();

                // Get the ID of the newly inserted notice
                var id = _context.CommunityConsultation
                    .Where(n => n.ReferenceNo == community.ReferenceNo)
                    .OrderByDescending(n => n.Id)
                    .Select(n => n.Id)
                    .FirstOrDefault();

                string key = community.ReferenceNo + "-" + id;

                // Update notice with case_id
                var communityToUpdate = _context.CommunityConsultation.Find(id);
                if (communityToUpdate != null)
                {
                    communityToUpdate.CaseId = key;
                    communityToUpdate.CaseRef = key;
                    _context.Entry(communityToUpdate).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

          
            }

            // If the model state is invalid, return the view with validation errors
            return Ok(new { data = model });
        }
        [HttpPost("UpdateCommunityConsultation")]
        public async Task<ActionResult> UpdateCommunityConsultation(CommunityConsultationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the legal notice by id
                var assistanceToUpdate = await _context.CommunityConsultation.FindAsync(model.Id);
                if (assistanceToUpdate == null)
                {
                    return NotFound(new { message = "Community Consultation not found" });
                }

                // Update LegalNotice properties
                assistanceToUpdate.ReferenceNo = model.ReferenceNo;
                assistanceToUpdate.LegalAdviceSought = model.LegalAdviceSought;
                assistanceToUpdate.LegalAssistanceSought = model.LegalAssistanceSought;
                assistanceToUpdate.LegalNoticeSent = model.LegalNoticeSent;
                assistanceToUpdate.DateWhenLegalNoticeSent = model.DateWhenLegalNoticeSent;
                assistanceToUpdate.LegalNoticeSentTo = model.LegalNoticeSentTo;
                assistanceToUpdate.UpdatedAt = DateTime.Now; // New updated time
                assistanceToUpdate.UpdatedBy = User?.Identity.Name; // Set if needed
                assistanceToUpdate.Active = 1;
                    assistanceToUpdate.ReferenceNo = model.ReferenceNo;
                    assistanceToUpdate.TypeOfAssistance = model.TypeOfAssistance ?? "New case";
                    assistanceToUpdate.ReasonForWithdrawal = model.ReasonForWithdrawal;
                    assistanceToUpdate.NatureOfLegalConcern = model.NatureOfLegalConcern;
                    assistanceToUpdate.FirNo = model.FirNo;
                    assistanceToUpdate.CaseNo = model.CaseNo;
                    assistanceToUpdate.CaseFiledBy = model.CaseFiledBy;
                    assistanceToUpdate.CaseFiledAgainst = model.CaseFiledAgainst;
                    assistanceToUpdate.IsLawyerShelterAssigned = model.IsLawyerShelterAssigned;
                    assistanceToUpdate.NameOfLawyer = model.NameOfLawyer;
                    assistanceToUpdate.ContactOfLawyer = model.ContactOfLawyer;
                    assistanceToUpdate.Court = model.Court;
                    assistanceToUpdate.ProvinceOfCourt = model.ProvinceOfCourt;
                    assistanceToUpdate.CityOfCourt = model.CityOfCourt;
                    assistanceToUpdate.NextDateOfHearing = model.NextDateOfHearing;
                    assistanceToUpdate.Remarks = model.Remarks;
                    assistanceToUpdate.StatusOfCase = model.StatusOfCase;
                assistanceToUpdate.Outcome = model.Outcome;
                assistanceToUpdate.UpdatedAt = DateTime.Now; // New updated time
                    assistanceToUpdate.UpdatedBy = User?.Identity.Name; // Set if needed
                    assistanceToUpdate.Active = 1;

                    _context.Entry(assistanceToUpdate).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                return Ok(new { message = "Record updated successfully", data = model });
            }

            // If the model state is invalid, return the view with validation errors
            return BadRequest(new { message = "Invalid data", data = model });
        }

        // ✅ GET Community Consultation by Id
        [HttpGet("getCommunityConsultationById")]
        public async Task<ActionResult<CommunityConsultation>> getCommunityConsultationById(int id)
        {
            var consultation = await _context.CommunityConsultation.FindAsync(id);

            if (consultation == null)
                return NotFound(new { message = "Community Consultation not found" });

            return Ok(new { data = consultation });
        }


        [HttpGet("getallcommunity")]
        public async Task<IActionResult> GetAllCommunity()
        {
            // Querying the active users from the database
            var data = await _context.CommunityConsultation
                .Where(u => u.Active == 1)
                .Select(u => new
                {
                    u.Id,
                    u.CreatedAt,
                    u.CaseId,
                    u.CaseRef,
                    u.NextDateOfHearing,
                    u.Court,
                    u.NatureOfLegalConcern,
                    u.StatusOfCase,
                    u.ReferenceNo,
                    u.Name

                })
                .OrderByDescending(u => u.Id)
                .ToListAsync();

            return Ok(new { data });
        }
       

        [HttpGet("gettdeletelegal")]
        public IActionResult gettdeletelegal(int id)
        {


            // Find the abuser record by ID and deactivate it
            var legal = _context.LegalAssistances.FirstOrDefault(a => a.Id == id);
            if (legal != null)
            {
                legal.Active = 0;
                // abuser.DeactivatedBy = userData.Email;
                legal.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();
            }

            // Redirect to the index page
            return Ok(new { message = "Delete Successfully..!" });
        }

        [HttpGet("gettdeleteallcommunity")]
        public IActionResult gettdeleteallcommunity(int id)
        {


            // Find the abuser record by ID and deactivate it
            var legal = _context.CommunityConsultation.FirstOrDefault(a => a.Id == id);
            if (legal != null)
            {
                legal.Active = 0;
                // abuser.DeactivatedBy = userData.Email;
                legal.UpdatedAt = DateTime.UtcNow;

                _context.SaveChanges();
            }

            // Redirect to the index page
            return Ok(new { message = "Delete Successfully..!" });
        }

    }
}
