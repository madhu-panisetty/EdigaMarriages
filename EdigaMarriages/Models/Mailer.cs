using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace EdigaMarriages.Models
{
    public class Mailer
    {
        public static void SendMail(string subject, string message)
        {
            try
            {
                string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
                string mailTo = ConfigurationManager.AppSettings["mailTo"];
                string mailLogin = ConfigurationManager.AppSettings["mailLogin"];
                string mailPassword = ConfigurationManager.AppSettings["mailPassword"];
                string mailHost = ConfigurationManager.AppSettings["mailHost"];

                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.Host = mailHost;
                client.EnableSsl = false;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(mailLogin, mailPassword);

                MailMessage mm = new MailMessage(mailFrom, mailTo, subject, message);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            }
            catch (Exception)
            {
                //ignore errors in mail
                throw;
            }


        }
    }
}