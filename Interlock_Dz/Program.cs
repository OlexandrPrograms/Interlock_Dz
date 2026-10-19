using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interlock_Dz
{
    internal class Program
    {
        static object[] forks = new object[5];
        static int finishedCount = 0;

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
                forks[i] = new object();

            Thread[] threads = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                int id = i;
                threads[i] = new Thread(() => Philosopher(id));
                threads[i].Start();
            }

            while (finishedCount < 5) Thread.Sleep(100);

            Console.WriteLine("Всі поїли!");
        }

        static void Philosopher(int id)
        {
            int left = id;
            int right = (id + 1) % 5;

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Філософ {id + 1} думає");
                Thread.Sleep(new Random().Next(1000, 2000));

                Console.WriteLine($"Філософ {id + 1} бере ліву вилку");
                lock (forks[left])
                {
                    Console.WriteLine($"Філософ {id + 1} бере праву вилку");
                    lock (forks[right])
                    {
                        Console.WriteLine($"Філософ {id + 1} їсть");
                        Thread.Sleep(new Random().Next(1000, 2000));

                        Console.WriteLine($"Філософ {id + 1} кладе праву вилку");
                    }
                    Console.WriteLine($"Філософ {id + 1} кладе ліву вилку");
                }
            }

            Interlocked.Increment(ref finishedCount);
        }
    }
}
