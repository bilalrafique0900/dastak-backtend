using DastakWebApi.Data;
using DastakWebApi.HelperMethods;
using DastakWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using DastakWebApi.Models;

namespace DastakWebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DastakDbContext _context;
        private readonly IUserService _userService;
        private readonly JwtHandler _jwtHandler;
        private readonly IConfiguration _configuration;

        public AuthController(DastakDbContext context, IUserService userService, JwtHandler jwtHandler,IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _jwtHandler = jwtHandler;
            _configuration
                = configuration;
        }

         // Allow guests to access this method
        [HttpPost ("postauth")]
        public async Task<IActionResult> Login(string Email,string Password)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.Users.Where(m => m.Email == Email && m.Password == Password).FirstOrDefaultAsync();

                if (result != null)
                {
                    var data = new
                    {
                        Status = true,
                        Message = "Login Successfully",
                        user=result

                    };
                    return Ok(new { data = data });
                }
                else
                {
                    var data2 = new
                    {
                        Status = false,
                        Message = "Login Failure"
                    };
                    return Ok(new { data = data2 });
                }
            }
            var data3 = new
            {
                Status = false,
                    Message = "Invalid login attempt."
                };
                return Ok(new { data = data3 }); // Return to the login view if authentication fails
        }
        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string Email, string Passcode)
        {

      var lastloginactivity=_context.LoginActivities.Where(m=>m.Email == Email && m.Passcode==Passcode).FirstOrDefault();
           if (lastloginactivity != null)
            {
                var verifytime = lastloginactivity.CreatedAt.Value.AddMinutes(3);

                if (verifytime >= DateTime.Now)
                {
                    var user = await _context.Users.Where(m => m.Email == Email).FirstOrDefaultAsync();

                    var data = new
                    {
                        token = _jwtHandler.GetToken(user),
                        emailverified = true
                    };
                    return Ok(new { data = data });
                }
                else
                {
                    var data2 = new
                    {
                        token = string.Empty,
                        emailverified = false
                    };
                    return Ok(new { data = data2 });
                }

            }
            else
            {
                var data3 = new
                {
                    token = string.Empty,
                    emailverified = false
                };
                return Ok(new { data = data3 });
            }
        }


        [HttpPost("SendVerification")]        public async Task<IActionResult> SendVerification(string ToEmail)        {
            string Passcode = GenerateUniqueCode();            var loginactivity = new LoginActivity
            {
                Id = 0,
                Email = ToEmail,
                Passcode = Passcode,
                CreatedAt = DateTime.Now
            };           await _context.AddAsync(loginactivity);          await  _context.SaveChangesAsync();           bool isemailsent= SendEmail(ToEmail,Passcode);            if(isemailsent)            return Ok("Email Sent Successfully..!");            else
                return Ok("Email Sent Failure..!");        }        private bool SendEmail(string ToEmail, string Passcode)        {
            // SMTP server details (example for Gmail)
            string smtpHost = _configuration.GetValue<string>("Smtp:Host");  // SMTP server
            int smtpPort = _configuration.GetValue<int>("Smtp:Port");                  // SMTP port (Gmail uses 587 for TLS)
            string smtpUser = _configuration.GetValue<string>("Smtp:From");  // Your email
            string smtpPass = _configuration.GetValue<string>("Smtp:Password");   // Your email password (or app password if 2FA is enabled)

            // Email details
            string fromEmail = smtpUser;            string toEmail = ToEmail;            string subject = "Passcode";            string body = "Your passcode for Dastak login is: " + Passcode;            try            {                using (SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort))                {
                    // Enable SSL and set credentials
                    smtpClient.EnableSsl = true;                    smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);

                    // Compose the email
                    MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, body);                    smtpClient.Send(mailMessage);                    return true;                }            }            catch (Exception ex)            {
                //Console.WriteLine($"Failed to send email: {ex.Message}");
                return false;
            }        }

        private static string GenerateUniqueCode()
        {
            byte[] randomNumber = new byte[4]; // 4 bytes = 32 bits
            RandomNumberGenerator.Fill(randomNumber);

            // Convert to a 9-digit number by taking it modulo 1 billion (1000000000)
            int code = Math.Abs(BitConverter.ToInt32(randomNumber, 0)) % 1000000000;

            // Return code as a 9-digit string, padding with leading zeros if necessary
            return code.ToString("D9");
        }
    }

}
   