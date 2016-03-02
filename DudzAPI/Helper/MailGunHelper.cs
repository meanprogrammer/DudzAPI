using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace DudzAPI.Helper
{
    public class MailGunHelper
    {
        const string MAILGUN_SMTP_SERVER = "smtp.mailgun.org";
        const string USERNAME = "MAILGUN_SMTP_LOGIN";
        const string MAILGUN_SMTP_PASSWORD = "2a7c6564971ea54cba0b119a6b98604c";
        const int MAILGUN_SMTP_PORT = 587;
        public static void SendMailLog(string value)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(MAILGUN_SMTP_SERVER);

                mail.From = new MailAddress("your_email_address@gmail.com");
                mail.To.Add("valiantdudz@hotmail.com");
                mail.Subject = "Log";
                mail.Body = value;

                SmtpServer.Port = MAILGUN_SMTP_PORT;
                SmtpServer.Credentials = new System.Net.NetworkCredential(USERNAME, MAILGUN_SMTP_PASSWORD);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }
    }
}