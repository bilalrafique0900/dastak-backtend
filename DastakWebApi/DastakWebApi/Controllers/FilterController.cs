using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.Services;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ICallersService _callService;

        public FilterController(DastakDbContext context, ICallersService callService)
        {
            _context = context;
            _callService = callService;
        }

        [HttpPost("postfilter")]
        public IActionResult Filter(FilterRequest req)
        {
            //var userController = new UserController(); // Create instance of UserController
            //var userData = userController.GetUserData(); // Fetch user data

            // Construct LINQ query based on filter inputs
            var query = _context.Parents
                .Join(_context.BasicInfos, p => p.ReferenceNo, b => b.ReferenceNo, (p, b) => new { p, b })
                .Join(_context.AdmissionRecords, pb => pb.p.ReferenceNo, ar => ar.ReferenceNo, (pb, ar) => new { pb.p, pb.b, ar })
                .Join(_context.MaritalInfos, pbar => pbar.p.ReferenceNo, m => m.ReferenceNo, (pbar, m) => new { pbar.p, pbar.b, pbar.ar, m })
                .Join(_context.Discharges, pbar => pbar.p.ReferenceNo, d => d.ReferenceNo, (pbar, d) => new { pbar.p, pbar.b, pbar.ar, pbar.m, d })
                .Where(pbar => pbar.p.IsAdmitted == 1).AsNoTracking(); // Filtering admitted parents

            if (!string.IsNullOrEmpty(req.City))
            {
                query = query.Where(pbar => pbar.b.City == req.City);
            }

            if (!string.IsNullOrEmpty(req.Province))
            {
                query = query.Where(pbar => pbar.b.DomicileProvince == req.Province);
            }

            if (req.Age.HasValue)
            {
                query = query.Where(pbar => pbar.b.Age == req.Age.Value);
            }

            if (!string.IsNullOrEmpty(req.Reason))
            {
                query = query.Where(pbar => pbar.ar.ReasonForAdmission.Contains(req.Reason));
            }

            if (!string.IsNullOrEmpty(req.Nature))
            {
                query = query.Where(pbar => pbar.ar.NatureOfAssisstance.Contains(req.Nature));
            }

            if (!string.IsNullOrEmpty(req.Status))
            {
                query = query.Where(pbar => pbar.m.MaritalStatus == req.Status);
            }

            if (!string.IsNullOrEmpty(req.Category))
            {
                query = query.Where(pbar => pbar.m.MaritalCategory == req.Category);
            }

            if (req.StartDate.HasValue && req.EndDate.HasValue)
            {
                query = query.Where(pbar => pbar.ar.AdmissionDate >= req.StartDate.Value && pbar.ar.AdmissionDate <= req.EndDate.Value);
            }

            // Execute the query and select required fields
            var data = query.Select(pbar => new
            {
                pbar.p.ReferenceNo,
                pbar.p.FileNo,
                pbar.p.Title,
                pbar.p.Discharged,
                pbar.p.FirstName,
                pbar.p.LastName,
                pbar.d.DischargeDate
            }).Distinct().ToList();

            return Ok(new { data = data });
        }
    }

    // The FilterRequest model to capture form data
    


}





