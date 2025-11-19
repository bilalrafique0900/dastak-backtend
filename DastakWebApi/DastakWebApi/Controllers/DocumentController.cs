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
    public class DocumentController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly ILegalAssistanceService _legalService;

        public DocumentController(DastakDbContext context, ILegalAssistanceService legalService)
        {
            _context = context;
            _legalService = legalService;
        }
       




        [HttpGet("getdocument")]
        public IActionResult getdocument(string file, string entity)
        {
            // Get user data (you may have a service to handle this)
        

            // Prepare data object
            var data = new
            {
                File = file,
                Entity = entity,
                Document = _context.Documentfiles
                    .Where(d => d.ReferenceNo == entity && d.Active == 1)
                    .OrderByDescending(d => d.Id)
                    .Select(d => new { d.Id, d.Name, d.Detail, d.Image })
                    .ToList()
            };

            // Pass the data to the view
            return Ok( new {  data });
        }
        [HttpGet("getdocumentview")]
        public IActionResult getdocumentview(string entity, int id)
        {
            // Assuming UserController has a method GetUserData


            // Initialize a data object
            var data = new
            {
                Info = _context.Parents
                    .Where(e => e.ReferenceNo == entity)
                    .Select(e => new { e.FileNo, e.ReferenceNo, e.Title, e.FirstName, e.LastName })
                    .FirstOrDefault(),

                Document = _context.Documentfiles
                    .Where(d => d.ReferenceNo == entity && d.Id == id)
                    .FirstOrDefault()
            };

            // Pass the data and userData to the view
            return Ok( new { data });
        }


        [HttpGet("getadddocument")]
        public IActionResult detadddocument(string file, string entity)
        {
          

            // Fetch the data
            var data = _context.Parents
                .Where(e => e.FileNo == file && e.ReferenceNo == entity)
                .Select(e => new {
                    e.FileNo,
                    e.ReferenceNo,
                    e.Title,
                    e.FirstName,
                    e.LastName,
                    e.AdmissionAt
                })
                .FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            // Return the view with user data and entity data
            return Ok(new { data });
        }

        [HttpPost("postadddocument")]
        
    public IActionResult Add([FromForm] DocumentUploadModel model, IFormFile image)
        {

            //var guid =  Guid.NewGuid();
           

            var document = new Documentfile
            {
                ReferenceNo = model.ReferenceNo,
                Name = model.Name,
                Detail = model.Detail,
                Image = image.FileName,
                CreatedAt = DateTime.Now,
              CreatedBy = User?.Identity.Name,
            Active = 1
            };

            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            // Check if the directory exists, and create it if it doesn't
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Combine the directory path with the file name to get the full path
            var path = Path.Combine(directoryPath, image.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            _context.Documentfiles.Add(document);
            _context.SaveChanges();

            return Ok(new { data = model });
        }


        [HttpGet("deletedocument")]
        public IActionResult Delete(int id)
            {
                // Get user data (if needed for any logic)
              

                // Use Entity Framework to find the document by its ID
                var document = _context.Documentfiles.FirstOrDefault(d => d.Id == id);

                if (document != null)
                {
                    // Set the 'Active' property to false (assuming 'Active' is a bool)
                    document.Active = 0;

                    // Save changes to the database
                    _context.SaveChanges();
                }
                else
                {
                    // Handle the case where the document isn't found
                   // return RedirectToAction("Index").WithWarning("Document not found.");
                }

                // Redirect to the document index action, passing file and entity as route values
                return Ok( new { message="Record Deleted Successfully..!" });
            }

        }

    }
