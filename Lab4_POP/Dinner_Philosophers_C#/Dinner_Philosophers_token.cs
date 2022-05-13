using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp50
{
    class Program
    {
        static Semaphore Empty_Order = new Semaphore(0, 5);
        static Semaphore Access_Order = new Semaphore(1, 1);  


        static Semaphore[] readyDinner = new Semaphore[5];
        static Semaphore[] forks = new Semaphore[5];
        static List<int> dinner_order = new List<int>();
        static readonly int dinner_numbers = 7; 


        static void Main()
        {            
            for (int i = 0; i < 5; i++)
            {
                readyDinner[i] = new Semaphore(0, 100);
                forks[i] = new Semaphore(1, 1);
            }

            new Thread(() => Token()).Start();
            for (int i = 0; i < 5; i++)
            {                
                new Thread(() => philosopher(i)).Start();
            }
        }
        static void Token()
        {
            Console.WriteLine("Token start");
            for (int i = 1; i <dinner_numbers*5; i++)
            {
                Empty_Order.WaitOne();
                Access_Order.WaitOne();
                int phil_num = dinner_order[0];
                Console.WriteLine("Token go to {0} philosopher",phil_num);
                readyDinner[phil_num].Release();
                dinner_order.RemoveAt(0);
                Access_Order.Release();
            }
        }
        static void philosopher(int id)
        {
            int num_second_fork = 0;
            if (id == 4)
            {
                num_second_fork = 0;
            }
            else
            {
                num_second_fork = id + 1;
            }
            for (int i = 1; i < dinner_numbers; i++)
            {
                Console.WriteLine("philosopher {0} thinking",id);
                Access_Order.WaitOne();
                dinner_order.Add(id);
                Empty_Order.Release();
                Access_Order.Release();

                Console.WriteLine("philosopher {0} await token", id);
                readyDinner[id].WaitOne();                
                forks[id].WaitOne();
                forks[num_second_fork].WaitOne();
                Console.WriteLine("philosopher {0} eating {1} times", id,id);
                forks[num_second_fork].Release();
                forks[id].Release();                
                Access_Order.Release();
            }
        }
    }
}

