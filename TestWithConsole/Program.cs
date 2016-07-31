

namespace TestWithConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting;
    using System.Threading;
    using BLL;
    using BLL.Entities;
    using BLL.Interfaces;
    

    public class Program
    {
        public static void Main(string[] args)
        {
            InitGroup.InitializeGroup();
            var master = InitGroup.Master as Master;
            var slaves = InitGroup.Slaves.Select(u => u as Slave).ToList();
            ShowServicesInfo(new[] {master});
            ShowServicesInfo(slaves);
            Console.WriteLine("\nPress enter to start: ");
            Console.WriteLine("\nIn the end press esc to finish: ");
            Console.ReadLine();
            RunSlaves(slaves);
            RunMaster(master);
            while (true)
            {
                var quit = Console.ReadKey();
                if (quit.Key == ConsoleKey.Escape)
                    break;
            }
        }

        private static void RunMaster(Master master)
        {
            Random rand = new Random();

            ThreadStart masterSearch = () =>
            {
                while (true)
                {
                    Console.Write("Current thread: " + Thread.CurrentThread.ManagedThreadId + ";(master)\n");
                    var serachresult = master.SearchForUsers(u => u.FirstName != null);
                    Console.Write("Master search results: ");
                    foreach (var result in serachresult)
                        Console.Write(result + " ");
                    Console.WriteLine();
                    Thread.Sleep(rand.Next(1000, 3000));
                }
            };

            ThreadStart masterAddDelete = () =>
            {
                var users = new List<BllUser>
                {
                    new BllUser { FirstName = "Natallia", LastName = "Nerovnya"},
                    new BllUser { FirstName = "Nata", LastName = "Nero"},
                };
                BllUser userToDelete = null;

                while (true)
                {
                    foreach (var user in users)
                    {
                        int addChance = rand.Next(0, 3);
                        if (addChance == 0)
                            master.Add(user);

                        Thread.Sleep(rand.Next(1000, 3000));
                        if (userToDelete != null)
                        {
                            int deleteChance = rand.Next(0, 3);
                            if (deleteChance == 0)
                                master.Delete(userToDelete);
                        }
                        userToDelete = user;
                        Thread.Sleep(rand.Next(1000, 3000));
                        Console.WriteLine();
                    }
                }
            };

            Thread masterSearchThread = new Thread(masterSearch) { IsBackground = true };
            Thread masterAddThread = new Thread(masterAddDelete) { IsBackground = true };
            masterAddThread.Start();
            masterSearchThread.Start();
        }

        private static void RunSlaves(List<Slave> slaves)
        {
            Random rand = new Random();

            foreach (var slave in slaves)
            {
                var slaveThread = new Thread(() =>
                {
                    while (true)
                    {
                        var userIds = slave.SearchForUsers(u => !string.IsNullOrWhiteSpace(u.FirstName));

                        Console.Write("Current Thread: " + Thread.CurrentThread.ManagedThreadId + ";(sl)\n");
                        Console.Write("Slave search results: ");
                        foreach (var user in userIds)
                            Console.Write(user + " ");
                        Console.WriteLine();
                        Thread.Sleep((int)(rand.NextDouble() * 3000));
                    }

                });
                slaveThread.IsBackground = true;
                slaveThread.Start();
            }
        }

        private static void ShowServicesInfo(IEnumerable<UserService> services)
        {
            var servicesList = services.ToList();
            Console.WriteLine("SERVICES INFO: \n");
            for (int i = 0; i < servicesList.Count; i++)
            {
                var service = servicesList[i];
                Console.Write($"Service {i} : type = ");
                if (service is Master)
                    Console.Write(" Master; ");
                else
                {
                    Console.Write(" Slave; ");
                }
                Console.Write("Current Domain: " + AppDomain.CurrentDomain.FriendlyName + "; ");
                Console.Write("IsProxy: " + RemotingServices.IsTransparentProxy(service) + "; ");
                Console.WriteLine();
            }
        }
    }
}