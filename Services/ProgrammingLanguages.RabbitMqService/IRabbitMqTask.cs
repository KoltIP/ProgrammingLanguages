using ProgrammingLanguages.RabbitMqService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace ProgrammingLanguages.RabbitMqService
{
    public interface IRabbitMqTask
    {
        Task SendEmail(EmailModel email);
    }
}
