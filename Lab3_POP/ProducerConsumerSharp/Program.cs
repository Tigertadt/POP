using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace ProducerConsumerSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Starter(10,50,3,3);

            Console.ReadKey();
        }


        private void Starter(int storageSize, int itemNumbers, int producers, int consumers)
        {
            Access = new Semaphore(1, 1);
            Full = new Semaphore(storageSize, storageSize);
            Empty = new Semaphore(0, storageSize);

            for (int i = 0;i<producers-1;i++)
            {
                Thread threadProducer = new Thread(Producer);
                threadProducer.Start(itemNumbers/producers);
            }

           


            for (int i = 0; i < consumers - 1; i++)
            {
                Thread threadConsumer = new Thread(Consumer);
                threadConsumer.Start(itemNumbers/consumers);
            }
          
        }

        private Semaphore Access;
        private Semaphore Full;
        private Semaphore Empty;

        private readonly List<string> storage = new List<string>();


        private void Producer(Object itemNumbers)
        {
            int maxItem = 0;
            if (itemNumbers is int)
            {
                maxItem = (int)itemNumbers;
            }
            for (int i = 0; i < maxItem; i++)
            {
                Full.WaitOne();
                Access.WaitOne();

                storage.Add("item " + i);
                Console.WriteLine("Added item " + i);

                Access.Release();
                Empty.Release();

            }
        }

        private void Consumer(Object itemNumbers)
        {
            int maxItem = 0;
            if (itemNumbers is int)
            {
                maxItem = (int)itemNumbers;
            }
            for (int i = 0; i < maxItem; i++)
            {
                Empty.WaitOne();
                Thread.Sleep(1000);
                Access.WaitOne();

                string item = storage.ElementAt(0);
                storage.RemoveAt(0);

                Console.WriteLine($"Consumer id {Environment.CurrentManagedThreadId} removed item {item}");
                //------------------
                //Normaly it mast be changed
                Full.Release();



                Access.Release();
                //-------------               
            }
            Console.WriteLine($"Consumer id {Environment.CurrentManagedThreadId} finished");
        }
    }
}
