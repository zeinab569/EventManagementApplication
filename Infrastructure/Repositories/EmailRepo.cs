using Core.Identity;
using Core.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Net.Mail;

namespace Infrastructure.Repositories
{
    public class EmailRepo : IEmailRepo
    {
        private readonly IConfiguration _configuration;

        public EmailRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(EmailModel emailModel)
        {
            var email = new MimeMessage();
            var from = _configuration["EmailSettings:From"];
            email.From.Add(new MailboxAddress("event management",from));
            email.To.Add(new MailboxAddress(emailModel.To, emailModel.To));
            email.Subject = emailModel.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailModel.Body)
            };
            using(var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_configuration["EmailSettings:SmtpServer"], 465, true);
                    client.Authenticate(_configuration["EmailSettings:From"], _configuration["EmailSettings:Password"]);
                    client.Send(email);
                }
                catch (Exception ex) 
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
