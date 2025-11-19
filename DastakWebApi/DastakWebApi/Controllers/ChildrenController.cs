using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Dynamic;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ChildrenController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ILegalAssistanceService _legalService;

        public ChildrenController(DastakDbContext context, ILegalAssistanceService legalService)
        {
            _context = context;
            _legalService = legalService;
        }
       



      

        [HttpGet("getchild")]
        public async Task<IActionResult> Index(string file, string entity)
        {
            // Get the current user
         //   var currentUser = await _userManager.GetUserAsync(User);
          //  var userData = new
          //  {
            //    UserName = currentUser.UserName,
              //  Email = currentUser.Email
                // Add other user data you need
           // };

            // Create a data model
            var data = new
            {
                Entity = entity,

                // Fetching the 'discharged' field from Entity based on 'reference_no'
                Discharged = await _context.Parents
                    .Where(e => e.ReferenceNo == entity)
                    .Select(e => e.Discharged)
                    .FirstOrDefaultAsync(),

                // File value passed in
                File = file,

                // Fetching children data where reference_no matches parent and active is true
                Children = await _context.Children
                    .Where(c => c.ReferenceNo == entity && c.Active == 1)
                    .OrderByDescending(c => c.Id)
                    .Select(c => new { c.ChildReferenceNo, c.Age, c.Name })
                    .ToListAsync()
            };

            // Returning the view with the data and user information
            return Ok( new { data});
        }
        [HttpGet("getchildview")]
        public IActionResult View(string entity, string childentity)
        {
          
            // Create a dynamic object to hold data
            //var data = new
            //{
            //    Info = new object(),
            //    Data = new List<object>()
            //};

            // Fetch single entity info safely
            var Info = _context.Parents
                .Where(e => e.ReferenceNo == entity)
                .Select(e => new
                {
                    e.ReferenceNo,
                    e.Title,
                    e.FirstName,
                    e.LastName
                })
                .FirstOrDefault(); // Use FirstOrDefault to handle a single record

            // Check if the entity info exists
            if (Info == null)
            {
                // Handle case where no entity is found, redirect back with error message
                return Ok(new { message = "Entity not found." });
            }

            // Fetch child-related data
            var ChildInfo = (from c in _context.Children
                         join ch in _context.ChildHealths on c.ChildReferenceNo equals ch.ChildReferenceNo
                         join co in _context.ChildOrientations on c.ChildReferenceNo equals co.ChildReferenceNo
                         join cs in _context.ChildSchoolings on c.ChildReferenceNo equals cs.ChildReferenceNo
                         where c.ChildReferenceNo == childentity && c.ReferenceNo == entity
                         select new
                         {
                             c,
                             ch,
                             co,
                             cs
                         }).FirstOrDefault();
            var data = new
            {
                Info = Info,
                ChildInfo = ChildInfo
            };

            return Ok(new { data });
        }

      [HttpGet ("geteditchild")]
      public async Task<IActionResult> Edit(string file, string entity, string childentity)
            {
                //var user = await _userManager.GetUserAsync(User);
                //var userEmail = user.Email;

                var data =await _context.Parents
                    .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                    .Select(e => new EntityViewModel
                    {
                        FileNo = e.FileNo,
                        ReferenceNo = e.ReferenceNo,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        ChildData = _context.Children
                            .Where(c => c.ChildReferenceNo == childentity)
                            .Join(_context.ChildHealths, c => c.ChildReferenceNo, h => h.ChildReferenceNo, (c, h) => new { c, h })
                            .Join(_context.ChildSchoolings, ch => ch.c.ChildReferenceNo, s => s.ChildReferenceNo, (ch, s) => new { ch, s })
                            .Join(_context.ChildOrientations, chs => chs.ch.c.ChildReferenceNo, o => o.ChildReferenceNo, (chs, o) => new ChildViewModel
                            {
                                MotherName = chs.ch.c.MotherName,
                                Name = chs.ch.c.Name,
                                Age = chs.ch.c.Age,
                                Gender = chs.ch.c.Gender,
                                DischargeDate = chs.ch.c.DischargeDate,
                                HasBeenReferred = chs.ch.c.HasBeenReferred,
                                WhereHasBeenReferred = chs.ch.c.WhereHasBeenReferred,
                                UpdatedAt = chs.ch.c.UpdatedAt,
                                UpdatedBy = chs.ch.c.UpdatedBy,
                                Health = new HealthViewModel
                                {
                                    Hygiene = chs.ch.h.Hygiene,
                                    SoughtMedicalTreatment = chs.ch.h.SoughtMedicalTreatment,
                                    UnderPhysicalViolence = chs.ch.h.UnderPhysicalVoilence,
                                    RequireMedicalOrPsychologicalAssistance = chs.ch.h.RequireMedicalOrPsychologicalAssisstance,
                                    SpecialChild = chs.ch.h.SpecialChild,
                                    Residence = chs.ch.h.Residence
                                },
                                Schooling = new SchoolingViewModel
                                {
                                    GradeAssigned = chs.s.GradeAssigned,
                                    ShelterSchoolEntryDate = chs.s.ShelterSchoolEntryDate,
                                    ShelterSchoolLeavingDate = chs.s.ShelterSchoolLeavingDate,
                                    ImpactOnReadingAbility = chs.s.ImpactOnReadingAbility,
                                    ImpactOnWritingAbility = chs.s.ImpactOnWritingAbility,
                                    ImpactOnMathsAbility = chs.s.ImpactOnMathsAbility,
                                    ImpactOnSocialAbility = chs.s.ImpactOnSocialAbility,
                                    ImpactOnExtraCuricularAbility = chs.s.ImpactOnExtraCuricularAbility
                                },
                                Orientation = new OrientationViewModel
                                {
                                    AttendedTraining = o.AttendedTraining,
                                    NatureOfTraining = o.NatureOfTraining,
                                    Vaccinated = o.Vaccinated,
                                    TypeOfVaccination = o.TypeOfVaccination,
                                    NextDateOfVaccination = o.NextDateOfVaccination,
                                    IsChildMaleAbove10 = o.IsChildMaleAbove10,
                                    WhereMaleChildBeenSent = o.WhereMaleChildBeenSent
                                }
                            }).FirstOrDefault()
                    })
                    .FirstOrDefaultAsync();

                return Ok( new EditViewModel { Data = data });
            }

      [HttpPost ("posteditchild")]
      public async Task<IActionResult> Edit( string childentity, EntityViewModel model)
            {
                if (ModelState.IsValid)
                {
                    //var user = await _userManager.GetUserAsync(User);
                 //   var userEmail = user.Email;

                    // Update Child
                    var child = await _context.Children.FirstOrDefaultAsync(c => c.ChildReferenceNo == childentity);
                    if (child != null)
                    {
                        child.MotherName = model.ChildData.MotherName;
                        child.Name = model.ChildData.Name;
                        child.Age = model.ChildData.Age;
                        child.Gender = model.ChildData.Gender;
                        child.DischargeDate = model.ChildData.DischargeDate;
                        child.HasBeenReferred = model.ChildData.HasBeenReferred;
                        child.WhereHasBeenReferred = model.ChildData.WhereHasBeenReferred;
                        child.UpdatedAt = DateTime.Now;
                        child.UpdatedBy = User?.Identity.Name;
                        _context.Update(child);
                    }

                    // Update Health
                    var health = await _context.ChildHealths.FirstOrDefaultAsync(h => h.ChildReferenceNo == childentity);
                    if (health != null)
                    {
                        health.Hygiene = model.ChildData.Health.Hygiene;
                        health.SoughtMedicalTreatment = model.ChildData.Health.SoughtMedicalTreatment;
                        health.UnderPhysicalVoilence = model.ChildData.Health.UnderPhysicalViolence;
                        health.RequireMedicalOrPsychologicalAssisstance = model.ChildData.Health.RequireMedicalOrPsychologicalAssistance;
                        health.SpecialChild = model.ChildData.Health.SpecialChild;
                        health.Residence = model.ChildData.Health.Residence;
                        _context.Update(health);
                    }

                    // Update Schooling
                    var schooling = await _context.ChildSchoolings.FirstOrDefaultAsync(s => s.ChildReferenceNo == childentity);
                    if (schooling != null)
                    {
                        schooling.GradeAssigned = model.ChildData.Schooling.GradeAssigned;
                        schooling.ShelterSchoolEntryDate = model.ChildData.Schooling.ShelterSchoolEntryDate;
                        schooling.ShelterSchoolLeavingDate = model.ChildData.Schooling.ShelterSchoolLeavingDate;
                        schooling.ImpactOnReadingAbility = model.ChildData.Schooling.ImpactOnReadingAbility;
                        schooling.ImpactOnWritingAbility = model.ChildData.Schooling.ImpactOnWritingAbility;
                        schooling.ImpactOnMathsAbility = model.ChildData.Schooling.ImpactOnMathsAbility;
                        schooling.ImpactOnSocialAbility = model.ChildData.Schooling.ImpactOnSocialAbility;
                        schooling.ImpactOnExtraCuricularAbility = model.ChildData.Schooling.ImpactOnExtraCuricularAbility;
                        _context.Update(schooling);
                    }

                    // Update Orientation
                    var orientation = await _context.ChildOrientations.FirstOrDefaultAsync(o => o.ChildReferenceNo == childentity);
                    if (orientation != null)
                    {
                        orientation.AttendedTraining = model.ChildData.Orientation.AttendedTraining;
                        orientation.NatureOfTraining = model.ChildData.Orientation.NatureOfTraining;
                        orientation.Vaccinated = model.ChildData.Orientation.Vaccinated;
                        orientation.TypeOfVaccination = model.ChildData.Orientation.TypeOfVaccination;
                        orientation.NextDateOfVaccination = model.ChildData.Orientation.NextDateOfVaccination;
                        orientation.IsChildMaleAbove10 = model.ChildData.Orientation.IsChildMaleAbove10;
                        orientation.WhereMaleChildBeenSent = model.ChildData.Orientation.WhereMaleChildBeenSent;
                        _context.Update(orientation);
                    }

                    await _context.SaveChangesAsync();
                }

            return Ok(new { data = model });
        }

     [HttpGet ("getaddchild")]
     public ActionResult Add(string file, string entity)
    {
        //var userData = _userController.GetUserData();

        var data =_context.Parents
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

        return Ok( new { data });
    }

     [HttpPost ("postaddchild")]
     public ActionResult Add(ChildRequestModel model)
    {
       // var userData = _userController.GetUserData();
        var id =_context.Children
            .Where(c => c.ReferenceNo == model.ReferenceNo)
            .Count();

        id++;

        var child = new Child
        {
            ReferenceNo = model.ReferenceNo,
            ChildReferenceNo = model.ReferenceNo + "-" + id,
            ChildReferenceNo2 = !string.IsNullOrEmpty(model.ChildReferenceNo2) ? model.ChildReferenceNo2 : model.ReferenceNo + "-" + id,
            MotherName = model.MotherName,
            Name = model.Name,
            Age = model.Age,
            Gender = model.Gender,
            DischargeDate = model.DischargeDate,
            HasBeenReferred = model.HasBeenReferred,
            WhereHasBeenReferred = model.WhereHasBeenReferred,
            CreatedAt = DateTime.Now,
           CreatedBy = User?.Identity.Name,
            Active = 1
        };

        _context.Children.Add(child);
        _context.SaveChanges();

        var health = new ChildHealth
        {
            ChildReferenceNo = child.ChildReferenceNo,
            Hygiene = model.Hygiene,
            SoughtMedicalTreatment = model.SoughtMedicalTreatment,
            UnderPhysicalVoilence = model.UnderPhysicalViolence,
            RequireMedicalOrPsychologicalAssisstance = model.RequireMedicalOrPsychologicalAssistance,
            SpecialChild = model.SpecialChild,
            Residence = model.Residence
        };

        _context.ChildHealths.Add(health);
        _context.SaveChanges();

        var school = new ChildSchooling
        {
            ChildReferenceNo = child.ChildReferenceNo,
            GradeAssigned = model.GradeAssigned,
            ShelterSchoolEntryDate = model.ShelterSchoolEntryDate,
            ShelterSchoolLeavingDate = model.ShelterSchoolLeavingDate,
            ImpactOnReadingAbility = model.ImpactOnReadingAbility,
            ImpactOnWritingAbility = model.ImpactOnWritingAbility,
            ImpactOnMathsAbility = model.ImpactOnMathsAbility,
            ImpactOnSocialAbility = model.ImpactOnSocialAbility,
            ImpactOnExtraCuricularAbility = model.ImpactOnExtraCurricularAbility
        };

        _context.ChildSchoolings.Add(school);
        _context.SaveChanges();

        var orient = new ChildOrientation
        {
            ChildReferenceNo = child.ChildReferenceNo,
            AttendedTraining = model.AttendedTraining,
            NatureOfTraining = model.NatureOfTraining,// != null ? JsonConvert.SerializeObject(model.NatureOfTraining) : null,
            Vaccinated = model.Vaccinated,
            TypeOfVaccination = model.TypeOfVaccination,// != null ? JsonConvert.SerializeObject(model.TypeOfVaccination) : null,
            NextDateOfVaccination = model.NextDateOfVaccination,
            IsChildMaleAbove10 = model.IsChildMaleAbove10,
            WhereMaleChildBeenSent = model.WhereMaleChildBeenSent
        };

        _context.ChildOrientations.Add(orient);
        _context.SaveChanges();

        return Ok(new { data= model });
    }
 
     [HttpGet("gettdeletechild")]
     public IActionResult Disable(string childReferenceNo)
        {
            // Get user data


            // Update child record
            var child = _context.Children.FirstOrDefault(c => c.ChildReferenceNo == childReferenceNo);

            if (child != null)
            {
                child.Active = 0; // Assuming Active is a boolean
                child.DeactivatedBy = User?.Identity.Name;

                _context.SaveChanges(); // Save the changes to the database

                // Optionally handle success
            }
            return Ok(new { message = "Delete Successfully..!" });
        }



    }



}

