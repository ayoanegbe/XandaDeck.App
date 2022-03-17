using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Threading;
using XandaApp.Data.Models;
using XandaApp.Infra.Services;

namespace XandaApp.Infra.Services
{
    public class EmailSender : IEmailSender
    {
        private EmailSettings _settings;

        public EmailSender(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public string SendEmailAsync(EmailRequest request, List<string> cc = null)
        {

            var result = string.Empty;

            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    MailMessage mail = new MailMessage(_settings.HostIP, request.RecieverEmailAddress);
                    client.Port = int.Parse(_settings.PortNumber);
                    client.Host = _settings.Host;
                    client.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);
                    client.EnableSsl = true;
                    mail.From = new MailAddress(_settings.HostIP, _settings.DisplayName);
                    mail.Subject = request.Subject;
                    mail.IsBodyHtml = true;

                    if (cc != null && cc.Count > 0)
                    {
                        foreach (var item in cc)
                        {
                            if (item != null)
                            {
                                mail.CC.Add(item);
                            }
                        }
                    }

                    mail.CC.Add(_settings.CcEmail);
                    //mail.To.Add(new MailAddress(request.RecieverEmailAddress));
                    mail.Sender = new MailAddress(_settings.HostIP, _settings.DisplayName);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    var bodyMessage = request.Body;

                    mail.Body = bodyMessage;

                    client.Send(mail);
                   
                    client.Dispose();

                    result = "success";

                }

            }
            catch (Exception ex)
            {
                result = ex.ToString();
                throw ex;
            }

            return result;
        }

        public Task<string> SendEmailAsync(string email, string code, string message)
        {
            throw new NotImplementedException();
        }
    }
}
