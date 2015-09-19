using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using scorecard.Models;


namespace scorecard.Models
{
    public class MessageHandler
    {
        public static Task SendEmailAsync(string to, string subject, string body)
        {
            int result = 0;

            try
            {
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"]);
                msg.ReplyToList.Add(msg.From);

                msg.To.Add(to);
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Send(msg);
            }
            catch (Exception)
            {
                // TODO: Logging here
                result = 1;
            }

            return Task.FromResult(result);
        }
    }
}