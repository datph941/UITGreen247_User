using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Project_UIT247Green_User.Models
{
    public class MailUtils
    {
        public static async Task<string> SendGmail(string from, string to, string subject, string body, string gmail, string pass)
        {
            MailMessage mes = new MailMessage(from, to, subject, body);
            mes.BodyEncoding = System.Text.Encoding.UTF8;
            mes.SubjectEncoding = System.Text.Encoding.UTF8;
            mes.IsBodyHtml = true;

            mes.ReplyToList.Add(new MailAddress(from));
            mes.Sender = new MailAddress(from);

            using var smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(gmail, pass);
            try
            {
                await smtpClient.SendMailAsync(mes);
                return "Success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "That bai" + e.Message;
            }
        }
    }
}
