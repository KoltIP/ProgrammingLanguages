using ProgrammingLanguages.RabbitMqService.Models;
using ProgrammingLanguages.Settings.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailModel model);
        Task SendEmailAsync(string email, string subject, string message);
    }
}
