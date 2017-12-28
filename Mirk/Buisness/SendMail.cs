using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Mirk.Buisness
{
    public class SendMail
    {
        private static SendMail instance;

        public static SendMail getInstance()
        {
            if (instance == null)
            {
                instance = new SendMail();
            }
            return instance;
        }

        public void Send(string name,string email,string content)
        {
            SmtpClient smtpClient = new SmtpClient("mail.MyWebsiteDomainName.com", 25);

            smtpClient.Credentials = new System.Net.NetworkCredential("info@MyWebsiteDomainName.com", "myIDPassword");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.Body = content;
            mail.From = new MailAddress(email, name);
            mail.To.Add(new MailAddress("217634@supinfo.com"));

            smtpClient.Send(mail);
        }
    }
}