using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace ProgrammingLanguages.RabbitMqService
{
    public delegate Task OnDataReceiveEvent<T>(T data);
    public interface IRabbitMq
    {
        Task Subscribe<T>(string queueName, OnDataReceiveEvent<T> onReceive);

        Task PushAsync<T>(string queueName, T data);
    }
}
