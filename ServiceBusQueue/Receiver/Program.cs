﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Receiver
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

            var service = new ServiceBusReceiverService(configuration.GetConnectionString("ServiceBus"), configuration.GetConnectionString("QueueName"));
            service.ReadMessages();

            Console.ReadKey();

            await service.CloseConnection();
        }
    }
}
