using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XandaApp.Data.Models;

namespace XandaApp.Infra.Services
{
    public interface IEmailSender
    {
        string SendEmailAsync(EmailRequest request, List<string> cc = null);
        Task<string> SendEmailAsync(string email, string code, string message);
    }
}
