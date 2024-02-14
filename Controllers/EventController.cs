using AKUH_API.Models;
using AKUH_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using WebAPICode.Helpers;

namespace AKUH_API.Controllers
{     
    [ApiController]
    [Route("[Controller]")]
    public class EventController : ControllerBase
    {        
        EventRepository _eventRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        public EventController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;           
            _configuration = configuration;
            _eventRepo = new EventRepository();
        }
        [HttpGet]
        [Route("All")]
        public async Task<RspEvent> GetAllEvent()
        {
            var data = _eventRepo.GetAllEvents();
            return await data;
        }
        [HttpGet]
        [Route("AllEventsByUserID/{UserID}")]
        public async Task<RspMyEvent> MyEvents(int UserID)
        {
            var data = _eventRepo.MyEvents(UserID);
            return await data;
        }
        [HttpPost]
        [Route("Register")]

        public async Task<ActionResult<RspAttendees>> Register(AttendeesBLL? attendees)
        {
            RspAttendees rspAttendees = new RspAttendees();
            try
            {
                int result = await _eventRepo.RegisterEvent(attendees);

                if (result > 0)
                {

                    //_eventRepo.SendEmailtoCustAdmin(attendees, result);
                    SendEmailtoCustAdmin(attendees, result);
                    rspAttendees.attendees = attendees;
                    rspAttendees.attendees.AttendeesID = result;
                    rspAttendees.attendees.StatusID = 1;
                    rspAttendees.status = 200;
                    rspAttendees.description = "User is Registered Successfully.";                    
                    return Ok(rspAttendees);
                }
                 
                else
                {
                    rspAttendees.attendees = attendees;
                    rspAttendees.status = 0;
                    rspAttendees.description = "Already Exist.";                     
                    return BadRequest(rspAttendees);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internet Connection Slow Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("UploadScreenshot")]
        
        public async Task<int> UploadScreenshot([FromBody] AttendeesBLL obj)
        {
            return _eventRepo.UploadSS(obj, _hostingEnvironment);
        }
        
        [HttpPost]
        [Route("UpdateAttendees")]

        public async Task<ActionResult<RspUpdtAttendees>> UpdateAttendees(AttendeesUpdtBLL? attendees)
        {
            RspUpdtAttendees rspAttendees = new RspUpdtAttendees();
            try
            {
                int result = await _eventRepo.UpdateAttendees(attendees);

                if (result == 1)
                {
                    rspAttendees.attendees = attendees;
                    //rspAttendees.attendees.AttendeesID = result;                    
                    rspAttendees.status = 200;
                    rspAttendees.description = "Attendee is Present.";
                    return Ok(rspAttendees);
                }
                if (result == 2)
                {
                    rspAttendees.attendees = attendees;
                    //rspAttendees.attendees.AttendeesID = result;                    
                    rspAttendees.status = 200;
                    rspAttendees.description = "Attendee is already Present.";
                    return Ok(rspAttendees);
                }
                else
                {
                    rspAttendees.attendees = attendees;
                    rspAttendees.status = 0;
                    rspAttendees.description = "Something Went Wrong.";
                    return BadRequest(rspAttendees);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internet Connection Slow: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("eventemail")]
        public void SendEmailtoCustAdmin(AttendeesBLL obj, int attendeesID)
        {

            var data = _eventRepo.GetAllDataForEmail(attendeesID);

            string ToEmail, SubJect, cc, Bcc;
            ToEmail = data.Email;
            SubJect = "You are registered in  - " + data.EventName;
            string mobile = data.PhoneNo;
            DateTime dt = DateTime.UtcNow.AddMinutes(300);
            string items = "";

            string webRootPath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "custEmail.txt");
            string BodyEmail = System.IO.File.ReadAllText(webRootPath);

            string webRootPathA = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "adminEmail.txt");
            string BodyEmailadmin = System.IO.File.ReadAllText(webRootPathA);

            BodyEmail = BodyEmail.Replace("#RegistrationDate#", data.Createdon.ToString());
            BodyEmail = BodyEmail.Replace("#EventStartDate#", data.FromDate.ToString("dd-MM-yyyy"));
            BodyEmail = BodyEmail.Replace("#EventEndDate#", data.ToDate.ToString("dd-MM-yyyy"));
            BodyEmail = BodyEmail.Replace("#EventTime#", data.EventTime.ToString());
            BodyEmail = BodyEmail.Replace("#CustomerName#", data.FullName.ToString());

            if (mobile != null)
            {
                BodyEmail = BodyEmail.Replace("#CustomerContact#", data.PhoneNo.ToString());
            }
            else
            {
                BodyEmail = BodyEmail.Replace("#CustomerContact#", "N/A");
            }


            //Admin
            BodyEmailadmin = BodyEmailadmin.Replace("#RegistrationDate#", data.Createdon.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#EventStartDate#", data.FromDate.ToString("dd-MM-yyyy"));
            BodyEmailadmin = BodyEmailadmin.Replace("#EventEndDate#", data.ToDate.ToString("dd-MM-yyyy"));
            BodyEmailadmin = BodyEmailadmin.Replace("#EventTime#", data.EventTime.ToString());
            BodyEmailadmin = BodyEmailadmin.Replace("#CustomerName#", data.FullName.ToString());
            if (mobile != null)
            {
                BodyEmailadmin = BodyEmailadmin.Replace("#CustomerContact#", data.PhoneNo.ToString());
            }
            else
            {
                BodyEmail = BodyEmail.Replace("#CustomerContact#", "N/A");
            }


            cc = "";
            Bcc = "akuhevents@gmail.com";
            
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(ToEmail);
                mail.From = new MailAddress("akuhevents@gmail.com");                 
                mail.Subject = SubJect;
                string Body = BodyEmail;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = false;                     
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";                     
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("akuhevents@gmail.com", "ueuzxvrsgtaxdbev");                     
                    smtp.Send(mail);                    
                }

                 
            }
            catch (Exception ex)
            {

            }
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("akuhevents@gmail.com");
                mail.From = new MailAddress("akuhevents@gmail.com");
                mail.Subject = SubJect;
                string Body = BodyEmail;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("akuhevents@gmail.com", "ueuzxvrsgtaxdbev");
                    smtp.Send(mail);
                }
                 
            }
            catch (Exception)
            {
            }
        }
       

    }
}