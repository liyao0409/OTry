using System;
using System.Threading.Tasks;
using OIfs;
using Orleans;
using Orleans.Runtime.Configuration;

namespace OClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Run();
            task.Wait();
        }

        private static async Task Run()
        {
            ClientConfiguration clientConfig = ClientConfiguration.LoadFromFile("ClientConfiguration.xml");
            IClientBuilder clientBuilder = ClientBuilder.CreateDefault();
            
            IClusterClient clusterClient = clientBuilder.UseConfiguration(clientConfig)
                .Build();
            await clusterClient.Connect();
            
            Console.WriteLine("输入手机号");
            var number = Console.ReadLine();
            var userService = clusterClient.GetGrain<IUserService>(1);
            var result = userService.Exist(number);
            
            Console.WriteLine(result.Result);

            await clusterClient.Close();
        }
    }
}