using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sender
{
    class TopicSenderService
    {
        private ITopicClient topicClient;

        public TopicSenderService(string connectionString, string topicName)
        {
            topicClient = new TopicClient(connectionString, topicName);
        }

        public async Task CloseConnection() 
            => await topicClient.CloseAsync();

        public async Task SendMessages()
        {
            var counter = 1;

            try
            {
                while (true)
                {
                    string messageBody = $"Message {counter} - {DateTime.Now}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    Console.WriteLine($"Sending message: {messageBody}");

                    await topicClient.SendAsync(message);

                    counter++;
                    Thread.Sleep(1000);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
