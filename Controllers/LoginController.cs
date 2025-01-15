using AKUH_API.Models;
using AKUH_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Mail;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AKUH_API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LoginController : ControllerBase
    {
        LoginRepository _loginRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        public LoginController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _loginRepo = new LoginRepository();
        }
        public class LoginDetails
        {
            public int AttendeesID { get; set; }
        }
        [HttpGet]
        [Route("CustomerLogin/{email}")]
        public async Task<RspLogin> loginCustomer(string email)
        {
            try
            {
                
                int result = await _loginRepo.AttendeeRegisterOnLogin(email);

                if (result > 0)
                {
                    RspLogin loginResponse = new RspLogin
                    {
                        login = new EventAttendeesBLL()  
                    };
                    loginResponse.login.StatusID = 102;
                    loginResponse.login.AttendeesID = result;
                    loginResponse.status = 200;
                    loginResponse.description = "Login Successfully";

                    SendEmailtoAdmin(email);
                    return loginResponse;


                    
                   // Create the response for a successful registration
                   //RspLogin loginResponse = new RspLogin();
                   // loginResponse.login.AttendeesID = result;
                   // loginResponse.status = 200;
                   // loginResponse.description = "User Registered Successfully";
                   // {
                   //     AttendeesID = loginResponse.AttendeesID,
                   //     status = 200,
                   //     description = "User Registered Successfully"
                   // };
                   // SendEmailtoAdmin(obj);
                   // return loginResponse; // Directly return the object
                }
                else
                {
                    // Create the response for already existing user
                    RspLogin loginResponse = new RspLogin
                    {
                        
                        status = 200,
                        description = "Already Exist."
                    };
                    return loginResponse; // Directly return the object
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return new RspLogin
                {
                    
                    status = 500,
                    description = $"Internal Server Error: {ex.Message}"
                };
            }
        }

        [HttpPost]
        [Route("user/register")]
        public async Task<ActionResult<RspAttendees>> AttendeeRegister(AttendeeRegsiterBLL obj)
        {
            try
            {
                int result = await _loginRepo.AttendeeRegister(obj);

                if (result > 0)
                {
                    // Assuming UserResponse has a UserId property, adjust accordingly
                    RspUser userResponse = new RspUser { UserId = result, Status = "200", Description = "User Registered Successfully" };
                    SendEmailtoAdmin(obj.Email);
                    return Ok(userResponse);
                }

                else
                {
                    RspUser userResponse = new RspUser { UserId = 0, Status = "0", Description = "Already Exist." };
                    return Ok(userResponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("LoginWithPasscode/{passcode}")]
        public async Task<RspLoginAdmin> LoginWithPasscode(string passcode)
        {
            var data = _loginRepo.LoginWithPasscode(passcode);
            return await data;
        }
       

        [HttpPost]
        [Route("Customer/edit")]

        public async Task<ActionResult<RspEditUser>> CustomerEdit(UserBLL user)
        {
            try
            {
                int result = await _loginRepo.CustomerEdit(user);

                if (result > 0)
                {
                    // Assuming UserResponse has a UserId property, adjust accordingly
                    RspEditUser userResponse = new RspEditUser { Status = "200", Description = "User Edited Successfully" };
                    return Ok(userResponse);
                }
                else
                {
                    RspEditUser userResponse = new RspEditUser { Status = "0", Description = "Failed to Edit user." };
                    return BadRequest(userResponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Customertoken/insert")]
        public async Task<ActionResult<RspToken>> InsertToken(PushTokenBLL token)
        {
            try
            {
                int result = await _loginRepo.InsertCustomerToken(token);

                if (result > 0)
                {
                    RspToken tokenResponse = new RspToken { status = 200, description = "Token Added Successfully" };
                    return Ok(tokenResponse);
                }
                else
                {
                    RspToken tokenResponse = new RspToken { status = 0, description = "Token Already Exist." };
                    return BadRequest(tokenResponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetUser/{AttendeesID}")]
        public RspCustomerLogin Customerlogin(int AttendeesID)
        {
            return _loginRepo.GetUserbyID(AttendeesID);
        }

        [HttpGet]
        [Route("GetDataQR/{UserID}")]
        public RspQR GetDataQR(int UserID)
        {
            return _loginRepo.GetDataQR(UserID);
        }

        [HttpGet]
        [Route("user/{email}/forget")]
        public RspForgetPwd forget(string email)
        {
            var result = _loginRepo.ForgetPassword(email);
            if (result.Status == "1")
            {
                SendEmailtoCust(result.Password, email);
            }
            
            return result;

        }
        [HttpPost]
        [Route("sendEmail")]
        public void SendEmailtoCust(string Password, string Email)
        {
            string ToEmail, SubJect, cc, Bcc;
            SubJect = "Password Updated – 10th Annual Surgical Conference";
              
            string webRootPathA = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "forgetPwdEmail.txt");
            string BodyEmail = System.IO.File.ReadAllText(webRootPathA);

            BodyEmail = BodyEmail.Replace("#Password#", Password);
            BodyEmail = BodyEmail.Replace("#Email#", Email);

            cc = "";
            Bcc = "akuhevents@gmail.com";

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(Email);
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
                    smtp.Credentials = new NetworkCredential("akuhevents@gmail.com", "xkezqmvksalkwwom");
                    smtp.Send(mail);
                }

            }
            catch (Exception)
            {
            }
            

        }

       
        [HttpPost]
        [Route("adminEmail")]
        public void SendEmailtoAdmin(string email)
        {
            string ToEmail, SubJect, cc, Bcc;
            SubJect = "You Received New Registration";
            string mobile = "";
            DateTime dt = DateTime.UtcNow.AddMinutes(300);
            string items = "";

            string webRootPathA = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "custEmail.txt");
            string BodyEmail = System.IO.File.ReadAllText(webRootPathA);

            //Customer
            BodyEmail = BodyEmail.Replace("#RegistrationDate#", DateTime.UtcNow.AddMinutes(300).ToString("dd-MM-yyyy"));
            
            

            string webRootPathB = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Template", "userRegistered.txt");
            string BodyEmailadmin = System.IO.File.ReadAllText(webRootPathB);

            //Admin
            BodyEmailadmin = BodyEmailadmin.Replace("#RegistrationDate#", DateTime.UtcNow.AddMinutes(300).ToString("dd-MM-yyyy"));
           

            cc = "";
            Bcc = "akuhevents@gmail.com";

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("akuhevents@gmail.com");
                mail.Subject = "10th Annual Surgical Conference";
                string Body = BodyEmail;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("akuhevents@gmail.com", "xkezqmvksalkwwom");
                    smtp.Send(mail);
                }

            }
            catch (Exception)
            {
            }
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("akuhevents@gmail.com");
                mail.From = new MailAddress("akuhevents@gmail.com");
                mail.Subject = SubJect;
                string Body = BodyEmailadmin;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("akuhevents@gmail.com", "xkezqmvksalkwwom");
                    smtp.Send(mail);
                }

            }
            catch (Exception)
            {
            }
        }
    }
}