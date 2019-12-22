using EmailAgnostic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace EmailAgnostic.Controllers
{
    public class SignupController : Controller
    {
        // GET: Signup
        [HttpPost]
        public ActionResult SignupValidation(Signup signup)
        {
            //generating a guid code and sending it to the clients mail for verification
            Guid g = Guid.NewGuid();
            TempData["UserDetails"] = signup;
            TempData["code"] = null; 
            MailAddress fromAddress = new MailAddress("sumitjana92@hotmail.com", "Sumit");
            MailAddress toAddress = new MailAddress(signup.Email, signup.Name);
            const string fromPassword = "Outlook@jgec11";
            string body = System.IO.File.ReadAllText(@"C:\Users\sumit\source\MVC-SmallProjects\EmailAgnostic\EmailAgnostic\TextFile1.txt");
            //string format
            body = body.Replace("&Customer", signup.Name).Replace("&sample@email.com", signup.Email).Replace("&samplecode",g.ToString());
            const string subject = "Code verification";
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.live.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address,fromPassword)
            };
            using (MailMessage msg = new MailMessage(fromAddress, toAddress) {
                Subject = subject,
                Body = body
            })
            {
                smtpClient.Send(msg);
                msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                if ((int)msg.DeliveryNotificationOptions == 1)
                {
                    TempData["code"] = g.ToString();
                }
            }
            
            return View();
        }
        [HttpPost]
        public ActionResult CheckCode(CodeValidations codeValidation)
        {
            string actualCode = TempData["code"] as string;
            bool isAuthenticated = false;
            if (codeValidation.Code == actualCode)
            {
                isAuthenticated = true;
                return View();
            }
            return PartialView("~/Views/Shared/SomethingWrong.cshtml");
            
        }
        [HttpPost]
        public ActionResult SetPassword(ValidatePassword validatePassword)
        {
            if (validatePassword.Password == validatePassword.ReEnterPassword)//this should be done on the client side
            {
                Signup signup = TempData["UserDetails"] as Signup;
                if (signup == null)
                {
                    return View("~Views/Home/Index.cshtml");
                }
                UserDetails userDetails = new UserDetails()
                {
                    UserName = signup.Name,
                    UserEmail = signup.Email,
                    UserAddress = signup.Address,
                    UserPhnNo = signup.PhoneNumber,
                    UserPassword = validatePassword.ReEnterPassword,
                    UserCreationTime = DateTime.Now
                };//store this data in db
                return View("~/Views/Login.cshtml");
            }
            return View();
        }
    }
}