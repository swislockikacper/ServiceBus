using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sender
{
    class ServiceBusSenderService
    {
        static IQueueClient queueClient;

        public ServiceBusSenderService(string connectionString, string queueName)
        {
            queueClient = new QueueClient(connectionString, queueName);
        }

        public async Task CloseConnection()
            => await queueClient.CloseAsync();

        public async Task SendMessagesAsync()
        {
            var counter = 1;

            try
            {
                while (true)
                {
                    string messageBody = $"Message {counter} - {DateTime.Now}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    Console.WriteLine($"Sending message: {messageBody}");

                    await queueClient.SendAsync(message);

                    Thread.Sleep(1000);
                    counter++;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
            finally
            {
                await CloseConnection();
            }
        }
    }
}
