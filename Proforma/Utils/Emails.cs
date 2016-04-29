using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Proforma.Utils
{
    public class Emails
    {
        public static void SendEmail(string from, string from_name, string to, string cc, string bcc, string subject, string body, bool isHtml)
        {
            SmtpClient mailClient = new SmtpClient("smtp.gmail.com");
            mailClient.Credentials = new NetworkCredential("nilesh.manglekar.proforma@gmail.com", "nilesh123#@!");
            mailClient.Port = 587;//Config.SmptSettings.Port;

            MailMessage message = new MailMessage();
            if (!string.IsNullOrEmpty(from_name))
            {
                message.From = new MailAddress(from, from_name);
            }
            else
            {
                // message.From = new MailAddress(Formatter.UnFormatSqlInput(from));
            }

            message.To.Add(new MailAddress(to));

            if (!string.IsNullOrEmpty(cc))
            {
                message.CC.Add(cc);
            }

            if (!string.IsNullOrEmpty(bcc))
            {
                message.Bcc.Add(bcc);
            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            mailClient.EnableSsl = true;
            mailClient.Send(message);
        }
    }
}