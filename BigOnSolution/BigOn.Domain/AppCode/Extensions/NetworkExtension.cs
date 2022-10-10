using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System;

namespace BigOn.Domain.AppCode.Extensions
{
    public static partial class Extension
    {
        static public bool SendMail(this IConfiguration configuration, string toEmail, string textBody, string textSubject)
        {
            try
            {

                var client = new SmtpClient(configuration["emailAccount:smtpServer"], Convert.ToInt32(configuration["emailAccount:smtpPort"]));
                client.Credentials = new NetworkCredential(configuration["emailAccount:userName"], configuration["emailAccount:password"]);
                client.EnableSsl = true;

                var from = new MailAddress(configuration["emailAccount:userName"], configuration["emailAccount:displayName"]);
                var to = new MailAddress(toEmail);

                var message = new MailMessage(from, to);
                message.Subject = textSubject;
                message.Body = textBody;
                message.IsBodyHtml = true;

                client.Send(message);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
