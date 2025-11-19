using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using File = DastakWebApi.Models.File;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [ApiController]
        [Route("[controller]")]
        public class NewAdmissionController : ControllerBase
        {
            private readonly DastakDbContext _context;
            private readonly IUserService _userService;

            public NewAdmissionController(DastakDbContext context, IUserService userService)
            {
                _context = context;
                _userService = userService;
            }
            [HttpPost("newaddmission")]
        public async Task<IActionResult> Add(EntityRequestModel req)
        {

            // File creation
            var file = new File
            {
                FileNo2 = !string.IsNullOrEmpty(req.FileNo2) ? req.FileNo2 : null,
                FileNo = null, // Will be updated later
                Active = 1,
                CreatedAt = DateTime.Now,
                CreatedBy = User?.Identity.Name,
            };

            _context.Files.Add(file);
            await _context.SaveChangesAsync();

            // Retrieve the latest inserted File ID
            var fileId = file.Id;

            // Update the FileNo with the fileId
            file.FileNo = fileId.ToString();
            _context.Files.Update(file);
            await _context.SaveChangesAsync();

            // Entity creation
            var entity = new Parent
            {
                FileNo = file.FileNo,
                ReferenceNo = file.FileNo,
                ReferenceNo2 = !string.IsNullOrEmpty(req.ReferenceNo2) ? req.ReferenceNo2 : null,
                Title = req.Title,
                FirstName = req.FirstName,
                LastName = req.LastName,
                AssessmentRisk = req.AssessmentRisk,
           
                EnsurePrivacy = req.EnsurePrivacy,
                Pending = 1,
                Active = 1,
                IsAdmitted = 0,
                Discharged = 0,
                IsReadmission = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = User?.Identity.Name,
            };

            _context.Parents.Add(entity);
            await _context.SaveChangesAsync();

            // Basic creation
            var basic = new BasicInfo
            {
                ReferenceNo = entity.ReferenceNo,
                DateOfBirth = req.DateOfBirth,
                Religion = req.Religion,
                BirthReligion = req.BirthReligion,
                Ethinicity = req.Ethinicity,
                PassportNo = req.PassportNo,
                FatherName = req.FatherName,
                FatherLivingStatus = req.FatherLivingStatus,
                MotherName = req.MotherName,
                MotherLivingStatus = req.MotherLivingStatus,
                GuardianName = req.GuardianName,
                GuardianRelation = req.GuardianRelation,
                Nationality = req.Nationality != null ? string.Join(",", req.Nationality) : null,
                Cnic = req.CNIC,
                DomicileCity = req.DomicileCity,
                DomicileProvince = req.DomicileProvince,
                Gender = req.Gender,
                LiteracyLevel = req.LiteracyLevel,
                Phone = req.Phone,
                Phone2 = req.Phone2,
                Address = req.Address,
                City = req.City,
                Country = req.Country,
                Age = req.DateOfBirth.HasValue ? (DateTime.Now.Year - req.DateOfBirth.Value.Year) : req.Age
            };

            basic.IsConvert = (short?)((basic.Religion != null && basic.BirthReligion != null && basic.Religion != basic.BirthReligion) ? 1 : 0);

            _context.BasicInfos.Add(basic);
            await _context.SaveChangesAsync();

            // Marital creation
            var marital = new MaritalInfo
            {
                ReferenceNo = entity.ReferenceNo,
                MaritalStatus = req.MaritalStatus,
                SeparatedSince = req.SeparatedSince,
                MaritalCategory = req.MaritalCategory,
                MaritalType = req.MaritalType,
                WifeOf = req.WifeOf,
                PartnerAbusedInDrug = req.PartnerAbusedInDrug,
                ProofOfMarriage = req.ProofOfMarriage != null ? string.Join(",", req.ProofOfMarriage) : null,
                HaveChildren = req.HaveChildren,
                TotalChildren = req.TotalChildren,
                ///AccompanyingChildren = req.AccompanyingChildren,
                AccompanyingChildrenName = req.AccompanyingChildrenName != null ? string.Join(",", req.AccompanyingChildrenName) : null,
                AccompanyingChildrenAge = req.AccompanyingChildrenAge != null ? string.Join(",", req.AccompanyingChildrenAge) : null,
                AccompanyingChildrenRelation = req.AccompanyingChildrenRelation != null ? string.Join(",", req.AccompanyingChildrenRelation) : null,
                CurrentlyExpecting = req.CurrentlyExpecting,
                ExpectedDeliveryDate = req.ExpectedDeliveryDate,
                AgeOfMarriage=req.AgeOfMarriage
            };

            _context.MaritalInfos.Add(marital);
            await _context.SaveChangesAsync();

            // References creation
            var references = new ReferencesRecord
            {
                ReferenceNo = entity.ReferenceNo,
                TypeOfReference = req.TypeOfReference != null && req.TypeOfReference.Any() ? string.Join(",", req.TypeOfReference) : null,
                ReferencialName = req.NameOfReference != null ? string.Join(",", req.NameOfReference) : null,
                ReferencialCity = req.CityOfReference != null ? string.Join(",", req.CityOfReference) : null,
                IsReferencial = req.IsReferential
            };

            _context.ReferencesRecords.Add(references);
            await _context.SaveChangesAsync();

            // Redirect to another page (or action) after successful save
            return Ok(new { fileNo = entity.FileNo, referenceNo = entity.ReferenceNo });
        }

        [HttpPost("readmission")]
        public async Task<IActionResult> ReAddmission(EntityRequestModel req)
        {

            // File creation
            var file = new File
            {
                FileNo2 = !string.IsNullOrEmpty(req.FileNo2) ? req.FileNo2 : null,
                FileNo = req.FileNo, // Will be updated later
                Active = 1,
                CreatedAt = DateTime.Now,
                CreatedBy = User?.Identity.Name,
            };

            _context.Files.Add(file);
            await _context.SaveChangesAsync();

            //// Retrieve the latest inserted File ID
            //var fileId = file.Id;

            //// Update the FileNo with the fileId
            //file.FileNo = fileId.ToString();
            //_context.Files.Update(file);
            //await _context.SaveChangesAsync();

            // Entity creation
            var entity = new Parent
            {
                FileNo = file.FileNo,
                ReferenceNo = req.ReferenceNo,
                ReferenceNo2 = !string.IsNullOrEmpty(req.ReferenceNo2) ? req.ReferenceNo2 : null,
                Title = req.Title,
                FirstName = req.FirstName,
                LastName = req.LastName,
                AssessmentRisk = req.AssessmentRisk,
                ResidenceBeforeReadmission =req.ResidenceBeforeReadmission,
                EnsurePrivacy = req.EnsurePrivacy,
                Pending = 1,
                Active = 1,
                IsAdmitted = 0,
                Discharged = 0,
                IsReadmission = req.IsReadmission,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = User?.Identity.Name,
            };

            _context.Parents.Add(entity);
            await _context.SaveChangesAsync();

            // Basic creation
            var basic = new BasicInfo
            {
                ReferenceNo = entity.ReferenceNo,
                DateOfBirth = req.DateOfBirth,
                Religion = req.Religion,
                BirthReligion = req.BirthReligion,
                Ethinicity = req.Ethinicity,
                PassportNo = req.PassportNo,
                FatherName = req.FatherName,
                FatherLivingStatus = req.FatherLivingStatus,
                MotherName = req.MotherName,
                MotherLivingStatus = req.MotherLivingStatus,
                GuardianName = req.GuardianName,
                GuardianRelation = req.GuardianRelation,
                Nationality = req.Nationality != null ? string.Join(",", req.Nationality) : null,
                Cnic = req.CNIC,
                DomicileCity = req.DomicileCity,
                DomicileProvince = req.DomicileProvince,
                Gender = req.Gender,
                LiteracyLevel = req.LiteracyLevel,
                Phone = req.Phone,
                Phone2 = req.Phone2,
                Address = req.Address,
                City = req.City,
                Country = req.Country,
                Age = req.DateOfBirth.HasValue ? (DateTime.Now.Year - req.DateOfBirth.Value.Year) : req.Age
            };

            basic.IsConvert = (short?)((basic.Religion != null && basic.BirthReligion != null && basic.Religion != basic.BirthReligion) ? 1 : 0);

            _context.BasicInfos.Add(basic);
            await _context.SaveChangesAsync();

            // Marital creation
            var marital = new MaritalInfo
            {
                ReferenceNo = entity.ReferenceNo,
                MaritalStatus = req.MaritalStatus,
                SeparatedSince = req.SeparatedSince,
                MaritalCategory = req.MaritalCategory,
                MaritalType = req.MaritalType,
                WifeOf = req.WifeOf,
                PartnerAbusedInDrug = req.PartnerAbusedInDrug,
                ProofOfMarriage = req.ProofOfMarriage,// != null ? string.Join(",", req.ProofOfMarriage) : null,
                HaveChildren = req.HaveChildren,
                TotalChildren = req.TotalChildren,
                ///AccompanyingChildren = req.AccompanyingChildren,
                AccompanyingChildrenName = req.AccompanyingChildrenName,// != null ? string.Join(",", req.AccompanyingChildrenName) : null,
                AccompanyingChildrenAge = req.AccompanyingChildrenAge,// != null ? string.Join(",", req.AccompanyingChildrenAge) : null,
                AccompanyingChildrenRelation = req.AccompanyingChildrenRelation,// != null ? string.Join(",", req.AccompanyingChildrenRelation) : null,
                CurrentlyExpecting = req.CurrentlyExpecting,
                ExpectedDeliveryDate = req.ExpectedDeliveryDate,
                AgeOfMarriage=req.AgeOfMarriage

            };

            _context.MaritalInfos.Add(marital);
            await _context.SaveChangesAsync();

            // References creation
            var references = new ReferencesRecord
            {
                ReferenceNo = entity.ReferenceNo,
                TypeOfReference = req.TypeOfReference,// != null && req.TypeOfReference.Any() ? string.Join(",", req.TypeOfReference) : null,
                ReferencialName = req.NameOfReference,// != null ? string.Join(",", req.NameOfReference) : null,
                ReferencialCity = req.CityOfReference,// != null ? string.Join(",", req.CityOfReference) : null,
                IsReferencial = req.IsReferential
            };

            _context.ReferencesRecords.Add(references);
            await _context.SaveChangesAsync();
            var data = new
            { fileNo = entity.FileNo, referenceNo = entity.ReferenceNo };
            // Redirect to another page (or action) after successful save
            return Ok(new { data=data });
        }

        [HttpGet("autofields")]
        public async Task<IActionResult> autofields()
        {
            var lastfile = _context.Files.OrderByDescending(m => m.Id).FirstOrDefault();
            if (lastfile != null)
            {
                var newData = new
                {

                    fileNo = $"{DateTime.Now:yyyy-MM-dd}-{lastfile.Id + 1}",
                    referenceNo = $"{DateTime.Now:yyyy-MM-dd}-{lastfile.Id + 1}-{1}"

                };
                return Ok(new { data = newData });
            }
            var data = new
            {

                fileNo = "",
                referenceNo = ""

            };
            return Ok(new { data = data });

        }
        [HttpGet("getinfo")]
        // GET: Edit
        public async Task<IActionResult> GetInfo(string entity)
        {
           // var userData = GetUserData(); // Implement this method to retrieve user data

            // Fetch data from the related tables using EF Core
            var data = await (from p in _context.Parents
                              join bi in _context.BasicInfos on p.ReferenceNo equals bi.ReferenceNo into basicInfoJoin
                              from bi in basicInfoJoin.DefaultIfEmpty()
                              join mi in _context.MaritalInfos on p.ReferenceNo equals mi.ReferenceNo into maritalInfoJoin
                              from mi in maritalInfoJoin.DefaultIfEmpty()
                              join rr in _context.ReferencesRecords on p.ReferenceNo equals rr.ReferenceNo into referencesJoin
                              from rr in referencesJoin.DefaultIfEmpty()
                              where p.ReferenceNo == entity
                              select new 
                              {
                                  p,
                                  bi,
                                  mi,
                                  rr
                              }).FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }

            var viewModel = new EntityEditViewModel
            {
                Id = data.p.Id,
                ReferenceNo = data.p.ReferenceNo,
                ReferenceNo2 = data.p.ReferenceNo2,
                Title = data.p.Title,
                FirstName = data.p.FirstName,
                LastName = data.p.LastName,
                AssessmentRisk = data.p.AssessmentRisk,
                EnsurePrivacy = data.p.EnsurePrivacy,
                ResidenceBeforeReadmission = data.p.ResidenceBeforeReadmission,

                // Basic info
                DateOfBirth =data.bi?.DateOfBirth,
                Religion = data.bi?.Religion,
                BirthReligion = data.bi?.BirthReligion,
                FatherName = data.bi?.FatherName,
                FatherLivingStatus = data.bi?.FatherLivingStatus,
                MotherName = data.bi?.MotherName,
                MotherLivingStatus = data.bi?.MotherLivingStatus,
                GuardianName = data.bi?.GuardianName,
                GuardianRelation = data.bi?.GuardianRelation,
                Ethinicity = data.bi?.Ethinicity,
                PassportNo = data.bi?.PassportNo,
                Nationality = data.bi?.Nationality,
                CNIC = data.bi?.Cnic,
                DomicileCity = data.bi?.DomicileCity,
                DomicileProvince = data.bi?.DomicileProvince,
                Gender = data.bi?.Gender,
                LiteracyLevel = data.bi?.LiteracyLevel,
                Phone = data.bi?.Phone,
                Phone2 = data.bi?.Phone2,
                Address = data.bi?.Address,
                City = data.bi?.City,
                Country = data.bi?.Country,
                Age = data.bi?.DateOfBirth != null ? DateTime.Now.Year - data.bi.DateOfBirth.Value.Year : (int?)null,
                IsConvert = data.bi?.Religion != data.bi?.BirthReligion,

                // Marital info
                MaritalStatus = data.mi?.MaritalStatus,
                SeparatedSince = data.mi?.SeparatedSince,
                MaritalCategory = data.mi?.MaritalCategory,
                MaritalType = data.mi?.MaritalType,
                WifeOf = data.mi?.WifeOf,
                PartnerAbusedInDrug = data.mi?.PartnerAbusedInDrug,
                ProofOfMarriage = data.mi?.ProofOfMarriage,
                HaveChildren = data.mi?.HaveChildren,
                TotalChildren = data.mi?.TotalChildren,
                AccompanyingChildrenName = data.mi?.AccompanyingChildrenName,
                AccompanyingChildrenAge = data.mi?.AccompanyingChildrenAge,
                AccompanyingChildrenRelation = data.mi?.AccompanyingChildrenRelation,
                CurrentlyExpecting = data.mi?.CurrentlyExpecting,
                ExpectedDeliveryDate = data.mi?.ExpectedDeliveryDate,

                // References
                TypeOfReference = data.rr?.TypeOfReference,
                ReferencialName = data.rr?.ReferencialName,
                ReferencialCity = data.rr?.ReferencialCity,
                IsReferencial = data.rr?.IsReferencial,
            };

            return Ok(new {data=viewModel});
        }

        [HttpPost("postinfo")]
        public async Task<IActionResult> PostInfo(EntityEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(model);
            }

            var entity = await _context.Parents.FirstOrDefaultAsync(e => e.ReferenceNo == model.ReferenceNo);
            if (entity != null)
            {
                entity.ReferenceNo2 = model.ReferenceNo2;
                entity.Title = model.Title;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.AssessmentRisk = model.AssessmentRisk;
                entity.EnsurePrivacy = model.EnsurePrivacy;
                entity.ResidenceBeforeReadmission = model.ResidenceBeforeReadmission;
                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = User?.Identity.Name; // Assuming you store the email in User.Identity.Name

                _context.Update(entity);
            }

            var basicInfo = await _context.BasicInfos.FirstOrDefaultAsync(b => b.ReferenceNo == model.ReferenceNo);
            if (basicInfo != null)
            {
                basicInfo.DateOfBirth = model.DateOfBirth;
                basicInfo.Religion = model.Religion;
                basicInfo.BirthReligion = model.BirthReligion;
                basicInfo.FatherName = model.FatherName;
                basicInfo.MotherName = model.MotherName;
                basicInfo.Country = model.Country;
                basicInfo.City = model.City;
                basicInfo.Nationality = model.Nationality;

                basicInfo.Age = model.DateOfBirth.HasValue ? DateTime.Now.Year - model.DateOfBirth.Value.Year : (int?)null;

                _context.Update(basicInfo);
            }

            var maritalInfo = await _context.MaritalInfos.FirstOrDefaultAsync(m => m.ReferenceNo == model.ReferenceNo);
            if (maritalInfo != null)
            {
                maritalInfo.MaritalStatus = model.MaritalStatus;
                maritalInfo.SeparatedSince = model.SeparatedSince;
                maritalInfo.MaritalCategory = model.MaritalCategory;
                maritalInfo.PartnerAbusedInDrug = model.PartnerAbusedInDrug;
                maritalInfo.AgeOfMarriage = model.AgeOfMarriage;

                _context.Update(maritalInfo);
            }

            var referencesRecord = await _context.ReferencesRecords.FirstOrDefaultAsync(r => r.ReferenceNo == model.ReferenceNo);
            if (referencesRecord != null)
            {
                referencesRecord.TypeOfReference = model.TypeOfReference;
                referencesRecord.ReferencialName = model.ReferencialName;
                referencesRecord.ReferencialCity = model.ReferencialCity;

                _context.Update(referencesRecord);
            }

            await _context.SaveChangesAsync();
            return Ok(new {data=model});
        }
    }






}
