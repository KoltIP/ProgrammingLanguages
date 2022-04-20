using ProgrammingLanguages.RabbitMqService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace ProgrammingLanguages.RabbitMqService
{
    public class RabbitMqTask : IRabbitMqTask
    {
        private readonly IRabbitMq rabbitMq;

        public RabbitMqTask(IRabbitMq rabbitMq)
        {
            this.rabbitMq = rabbitMq;
        }

        public async Task SendEmail(EmailModel email)
        {
            await rabbitMq.PushAsync(RabbitMqTaskQueueNames.SEND_EMAIL, email);
        }
    }
}
