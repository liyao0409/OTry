using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;

namespace OServer
{
    class Program
    {
        static void Main(string[] args)
        {
            StartSilo();
            Console.WriteLine("Hello World!");
        }

        static void StartSilo()
        {
            var clusterConfig = new ClusterConfiguration();
            clusterConfig.LoadFromFile("OrleansConfiguration.xml");

            var builder = new SiloHostBuilder()
                .UseConfiguration(clusterConfig)
                .ConfigureLogging(logging => logging.AddConsole())
                .ConfigureApplicationParts(appPartMg=>appPartMg.AddFromApplicationBaseDirectory())
                ;
            
            var siloHost = builder.Build();
            
            siloHost.StartAsync().Wait();
            //siloHost;
            
            //if (!siloHost.IsStarted)
            //{
            //    Console.WriteLine("failed to start");
            //}
            //else
            //{
            //    Console.WriteLine("started at :"+siloHost.NodeConfig.HostNameOrIPAddress+";port:"+siloHost.NodeConfig.Port);
            //}
            Console.WriteLine("Press Enter to close.");
            // wait here
            Console.ReadLine();

            // shut the silo down after we are done.
            siloHost.StopAsync().Wait();
        }
    }
}