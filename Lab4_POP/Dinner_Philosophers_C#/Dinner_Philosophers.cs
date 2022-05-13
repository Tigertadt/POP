using System;
using System.Threading;

namespace Dinner_Philosophers
{
    class Program
    {
        static void Main(string[] args)
        {
            Semaphore[] forks = new Semaphore[6];
            for (int i = 0; i < forks.Length; i++)
            {
                forks[i] = new Semaphore(1, 1);
            }

            for (int i = 0; i < 5; i++)
            {
                new Thread(() => Philosopher(i, 5,forks)).Start();
            }
        }


        static void Philosopher(int id, int dinnernumbers,Semaphore[] forks)
        {            
            for (int i = 0; i<dinnernumbers; i++)
            {
                int second_fork = i + 1;
                if (id < 5)
                {
                    Console.WriteLine("philosopher {0} thinking", id);
                    forks[id].WaitOne();
                    Console.WriteLine("philosopher {0} await {1} fork", id, second_fork);
                    forks[second_fork].WaitOne();
                    Console.WriteLine("philosopher {0} eating {1} times", id, i + 1);
                    forks[id].Release();
                    forks[second_fork].Release();
                }
                else
                {
                    Console.WriteLine("philosopher {0} thinking", id);
                    forks[1].WaitOne();
                    Console.WriteLine("philosopher {0} wait {1} fork", id, id);
                    forks[5].WaitOne();
                    Console.WriteLine("philosopher {0} eating {1} times", id, i+1);
                    forks[5].Release();
                    forks[1].Release();
                }
            }
           
        }
    }
}
