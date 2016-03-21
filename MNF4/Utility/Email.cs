using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Configuration;
using System.Web;
using System.Web.Configuration;
using MNF4.Models;
using MNF4.ViewModels;

namespace MNF4.Utility
{
    public class Email
    {
        protected static Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration("~/web.config") as Configuration;
        protected static MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
        protected static string host = mailSettings.Smtp.Network.Host;
        protected static string from = mailSettings.Smtp.From;

        //constructor
        public Email()
        {
        }
        
        //
        // Inbound generic email form - requires EmailViewModel
        public void InboundMessage(EmailViewModel viewModel)
        {
            MailAddress fromAddress = new MailAddress(viewModel.EmailAddress, viewModel.FirstName + " " + viewModel.LastName);
            MailAddress toAddress = new MailAddress(ConfigurationManager.AppSettings["MNFMailAddress"],
                                                    ConfigurationManager.AppSettings["MNFMailDisplay"]);
            MailAddress ccAddress = new MailAddress(ConfigurationManager.AppSettings["MNFMailAddressCC"],
                                                    ConfigurationManager.AppSettings["MNFMailDisplayCC"]);

            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.To.Add(toAddress);   // can separate multiple with a comma
                    message.CC.Add(ccAddress);
                    message.From = fromAddress;
                    message.Subject = viewModel.Subject;
                    message.Body = viewModel.Comments;
                    message.IsBodyHtml = true;

#if !DEBUG
                    ClientSend(message);    // send message - at bottom of class
#endif
                }
            }
            catch (SmtpException ex)
            {
                // Write error
                var SubmitResults = "Error in InboundMessage() " + ex.Message + ex.InnerException;
            }
        }

        //
        // Contact Form submission driver
        public void SubmitForm(Contact contact)
        {

            MailAddress fromAddress = new MailAddress(contact.EmailAddress, contact.FirstName + " " + contact.LastName);
            MailAddress toAddress = new MailAddress(ConfigurationManager.AppSettings["ContactFormToAddress"],
                                                    ConfigurationManager.AppSettings["ContactFormToDisplay"]);
            MailAddress ccAddress = new MailAddress(ConfigurationManager.AppSettings["MNFMailAddressCC"],
                                                    ConfigurationManager.AppSettings["MNFMailDisplayCC"]);

            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.Subject = "MNF Contact Form Submission";
                    message.Body = @"The following contact was added as ClientID #: " + contact.ID + "<br /><br />"
                        + "First Name = " + contact.FirstName + "<br />"
                        + "Last Name = " + contact.LastName + "<br />"
                        + "Email Address = " + contact.EmailAddress + "<br />"
                        + "Address = " + contact.Address + "<br />"
                        + "City = " + contact.City + "<br />"
                        + "State = " + contact.State + "<br />"
                        + "Zip Code = " + contact.ZipCode + "<br />"
                        + "Phone number = " + contact.Phone + "<br />"
                        + "Opt-In = " + contact.optIn + "<br /><br />"
                        + "Comments = " + contact.Comments;

                    message.To.Add(toAddress);   // can separate multiple with a comma
                    message.CC.Add(ccAddress);
                    message.From = fromAddress;
                    message.IsBodyHtml = true;

                    ClientSend(message);    // send message - at bottom of class
                }

            }
            catch (SmtpException ex)
            {

                // Write error
                var SubmitResults = "Error in SubmitForm() " + ex.Message;
            }
        }

        //
        // Reply message sent in response to a Contact Form Submission
        public void SendReplyMessage(Contact contact)
        {
            MailAddress replyTo = new MailAddress(contact.EmailAddress, contact.FirstName + " " + contact.LastName);

            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.Subject = "Thank you for contacting Marquette Nutrition & Fitness";
                    message.Body = @"Thank you for contacting Marquette Nutrition and Fitness.  We have received your "
                                + "information and will be following up via email and telephone within two business days.  "
                                + Environment.NewLine + Environment.NewLine
                                + "Best regards," + Environment.NewLine + Environment.NewLine
                                + "Christine E. Marquette, RD, LD, CLT" + Environment.NewLine
                                + "ACSM Certified Health Fitness Specialist" + Environment.NewLine
                                + "Marquette Nutrition & Fitness, LLC" + Environment.NewLine
                                + "www.marquettenutrition.com" + Environment.NewLine
                                + "http://marquettenutrition.blogspot.com" + Environment.NewLine
                                + "www.facebook.com/marquette.nutrition.and.fitness" + Environment.NewLine
                                + "512.468.4338";

                    message.To.Add(replyTo);                    // original sender of form submission
                    message.From = new MailAddress(from);       // contact@marquettenutrition.com
                    message.IsBodyHtml = false;

                    ClientSend(message);
                }
            }
            catch (SmtpException ex)
            {
                // Write error
                var SubmitResults = "Error in SendReplyMessage() " + ex.Message + ex.InnerException;
            }
        }

        //
        // Send OptIn contact the requested document
        public void SendRequestedDocument(Contact contact, string document)
        {
            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["MNFMailAddress"],
                                                      ConfigurationManager.AppSettings["MNFMailDisplay"]);
            MailAddress toAddress = new MailAddress(contact.EmailAddress, contact.FirstName + " " + contact.LastName);

            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.Subject = "Here is the link to your requested document";
                    message.Body = @"Thank you for contacting Marquette Nutrition and Fitness.<br /><br />Here is the link to "
                                + "your requested document."
                                + "<br /><br />"
                                + "<a href='http://www.marquettenutrition.com/Content/Documents/" + document + "' target='_blank'>"
                                + document + "</a>"
                                + "<br /> <br />"
                                + "Best regards, " + "<br /><br />"
                                + "Christine E. Marquette, RD, LD, CLT" + " <br />"
                                + "ACSM Certified Health Fitness Specialist" + " <br />"
                                + "Marquette Nutrition & Fitness, LLC" + " <br />"
                                + "www.marquettenutrition.com" + " <br />"
                                + "http://marquettenutrition.blogspot.com" + " <br />"
                                + "www.facebook.com/marquette.nutrition.and.fitness" + " <br />"
                                + "512.468.4338";

                    message.To.Add(toAddress);        // original sender of form submission
                    message.From = fromAddress;     // contact@marquettenutrition.com
                    message.IsBodyHtml = true;

                    ClientSend(message);
                }
            }
            catch (SmtpException ex)
            {
                // Write error
                var SubmitResults = "Error in SendReplyMessage() " + ex.Message + ex.InnerException;
            }
        }

        public void SendWebinarInstructions(EmailViewModel viewModel, IHtmlString instructions)
        {
            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["MNFMailAddress"],
                                                      ConfigurationManager.AppSettings["MNFMailDisplay"]);
            MailAddress toAddress = new MailAddress(viewModel.EmailAddress, viewModel.FirstName + " " + viewModel.LastName);

            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.Subject = "PCOS Webinar Registration Information";
                    message.Body = instructions.ToHtmlString();
                    message.To.Add(toAddress);        // original sender of form submission
                    message.From = fromAddress;     // contact@marquettenutrition.com
                    message.IsBodyHtml = true;

                    ClientSend(message);
                }

            }
            catch (SmtpException ex)
            {
                // Write error
                var SubmitResults = "Error in SendReplyMessage() " + ex.Message + ex.InnerException;
            }
        }
    
        private void ClientSend(MailMessage message)
        {
            using (SmtpClient client = new SmtpClient(host))
            {
                client.UseDefaultCredentials = true;
#if !DEBUG
                client.Send(message);
#endif
            }
        }
    }
}