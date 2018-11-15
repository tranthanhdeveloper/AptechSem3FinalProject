using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace Web.Helper.EmailHelper
{
    public class MailHelper
    {
        public static bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                var host = WebConfigurationManager.AppSettings["SMTPHost"];
                var port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
                var fromEmail =WebConfigurationManager.AppSettings["FromEmailAddress"];
                var password = WebConfigurationManager.AppSettings["FromEmailPassword"];
                var fromName = WebConfigurationManager.AppSettings["FromName"];

                var smtpClient = new SmtpClient(host, port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromEmail, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Timeout = 100000
                };

                var mail = new MailMessage
                {
                    Body = content,
                    Subject = subject,
                    From = new MailAddress(fromEmail, fromName)
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                smtpClient.Send(mail);

                return true;
            }
            catch (SmtpException smex)
            {
               
                return false;
            }
        }
    }
}