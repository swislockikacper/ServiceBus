using Microsoft.Extensions.Configuration;
using Sender;
using System;
using System.IO;
using System.Threading.Tasks;

namespace sender
{
    class Program
    {
        static void Main(string[] args)
            => MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var service = new TopicSenderService(configuration.GetConnectionString("ServiceBus"), configuration.GetConnectionString("TopicName"));
            await service.SendMessages();

            Console.ReadKey();

            await service.CloseConnection();
        }
    }
}
