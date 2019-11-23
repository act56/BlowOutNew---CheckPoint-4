using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using BlowOutNew.DAL;
using BlowOutNew.Models;

namespace BlowOutNew.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.Support = "Please call Support at<strong> < u > 801 - 555 - 1212.</ u ></ strong > Thank you!";

            return View();
        }


        public ActionResult Email(string firstName, string lastName, string emailAddress)
        {
            ViewBag.Name = firstName + " " + lastName;
            ViewBag.Email = emailAddress;

            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }


        
        //pulls the contact page for collecting contact information
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Support = "Please call Support at <strong><u>801-555-1212.</u></strong> Thank you!";
            return View();
        }

        //Sending smtp emails
        public ActionResult Email(ContactRequest contact)
        {
            ViewBag.Name = contact.firstName + " " + contact.lastName;
            ViewBag.Email = contact.emailAddress;

            return View("Email",contact);
        }

        //this code set up the email to send
        public void SendEmail(string toAddress, string fromAddress,
                      string subject, string message)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    const string email = "is403test@gmail.com";
                    const string password = "trashtan143";

                    var loginInfo = new System.Net.NetworkCredential(email, password);


                    mail.From = new MailAddress(fromAddress);
                    mail.To.Add(new MailAddress(toAddress));
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;

                    try
                    {
                        using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtpClient.EnableSsl = true;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = loginInfo;
                            smtpClient.Send(mail);
                        }

                    }
                    finally
                    {
                        //dispose the client
                        mail.Dispose();
                    }

                }
            }
            catch (SmtpFailedRecipientsException ex)
            {
                foreach (SmtpFailedRecipientException t in ex.InnerExceptions)
                {
                    var status = t.StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                        status == SmtpStatusCode.MailboxUnavailable)
                    {
                        Response.Write("Delivery failed - retrying in 5 seconds.");
                        System.Threading.Thread.Sleep(5000);
                        //resend
                        //smtpClient.Send(message);
                    }
                    else
                    {
                        Response.Write("Failed to deliver message to {0}"
                                          /*t.FailedRecipient*/);
                    }
                }
            }
            catch (SmtpException Se)
            {
                // handle exception here
                Response.Write(Se.ToString());
            }

            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

        }

        //this code validates the inputs from the model and prepares the email
        [HttpPost]
        public ActionResult Contact(ContactRequest contact)
        {
            if (ModelState.IsValid)
            {
                //prepare email
                var toAddress = "is403test@gmail.com";
            var fromAddress = contact.emailAddress;
            var subject = "BlowOut Instrument Rental Inquiry: " + contact.subject;
            var message = new StringBuilder();
            message.Append("Name: " + contact.firstName + contact.lastName + "\n");
            message.Append("Email: " + contact.emailAddress + "\n");
            message.Append("phone: " + contact.phoneNumber + "\n");
            message.Append("Subject: " + contact.subject + "\n\n");
            message.Append(contact.Message);

            //start email Thread
            var tEmail = new Thread(() =>
           SendEmail(toAddress, fromAddress, subject, contact.Message));
            tEmail.Start();

                ViewBag.Name = contact.firstName + " " + contact.lastName;
                ViewBag.Email = contact.emailAddress;

                return View("Email",contact);
            }

            

            return View(contact);
        }
    }
}