using System;
using System.Collections.Generic;
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

            while (!result.IsCompleted)
            {
                
            }
            if (result.Status == TaskStatus.Faulted)
            {
                foreach (var exceptionInnerException in result.Exception.InnerExceptions)
                {
                    Console.WriteLine(exceptionInnerException.Message);
                }
            }
            //try
            //{
            //    result.Wait();
            //}
            //catch (AggregateException e)
            //{
            //    foreach (var eInnerException in e.InnerExceptions)
            //    {
            //        Console.WriteLine(eInnerException.Message);
            //    }
            //    Console.WriteLine(e.Message);
            //}
            Console.WriteLine(result.Result);

            await clusterClient.Close();
        }
    }

    public class MyTaskScheduler : TaskScheduler
    {
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            throw new NotImplementedException();
        }

        protected override void QueueTask(Task task)
        {
            throw new NotImplementedException();
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            throw new NotImplementedException();
        }
    }
}