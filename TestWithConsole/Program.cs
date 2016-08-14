namespace TestWithConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using BLL;
    using BLL.Entities;

    public class Program
    {
        public static void Main(string[] args)
        {
            InitGroup.InitializeGroup();
            var master = InitGroup.Master as Master;
            var slaves = InitGroup.Slaves.Select(u => u as Slave).ToList();
            
            Console.WriteLine("\nPress enter to start: ");
            Console.WriteLine("\nIn the end press esc to finish: ");
            Console.ReadLine();
            RunSlaves(slaves);
            RunMaster(master);
            while (true)
            {
                var quit = Console.ReadKey();
                if (quit.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        private static void RunMaster(Master master)
        {
            Random rand = new Random();

            ThreadStart masterSearch = () =>
            {
                while (true)
                {
                    var serachresult = master.GetAllUsers();
                    Console.Write("Users id( from master with thread id {0}): ", Thread.CurrentThread.ManagedThreadId);
                    foreach (var result in serachresult)
                    {
                        Console.Write(result.Id + " ");
                    }
                       
                    Console.WriteLine();
                    Thread.Sleep(rand.Next(1000, 3000));
                }
            };

            ThreadStart masterAddDelete = () =>
            {
                var users = new List<BllUser>
                {
                    new BllUser { FirstName = "Natallia", LastName = "Nerovnya" },
                    new BllUser { FirstName = "Nata", LastName = "Nero" }
                };

                while (true)
                {
                    foreach (var user in users)
                    {
                        int chance = rand.Next(0, 3);
                        if (chance == 0 || chance == 1)
                        {
                            master.Add(user);
                        }
                        else
                        {
                            if (master.GetAllUsers().Count != 0)
                            {
                                var firstUser = master.GetAllUsers().First();
                                master.Delete(firstUser);
                            }
                        }

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
                        var userIds = slave.GetAllUsers().Select(u => u.Id);

                        Console.Write("User id(from slave with thread id {0}): ", Thread.CurrentThread.ManagedThreadId);
                        foreach (var user in userIds)
                        {
                            Console.Write(user + " ");
                        }
                            
                        Console.WriteLine();
                        Thread.Sleep((int)(rand.NextDouble() * 3000));
                     }
                });
                slaveThread.IsBackground = true;
                slaveThread.Start();
            }
        }
    }
}