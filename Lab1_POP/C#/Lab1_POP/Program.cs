using System;
using System.Threading;
namespace Lab1_POP
{
    class Program
    {
        
            static void Main(string[] args)
            {
                (new Program()).Start();
            }

            void Start()
            {
                (new Thread(Calcuator)).Start();
                (new Thread(Calcuator)).Start();
                (new Thread(Calcuator)).Start();

                Thread thread1 = new Thread(Calcuator);
                thread1.Start();

                (new Thread(Stoper)).Start();
            }

            void Calcuator()
            {
                long sum = 0;
                int step = 3;
                long terms = 0;
                do
                {
                    sum=sum+step;
                    terms++;
                } while (!canStop);
                Console.WriteLine("Сума дорівнює {0}, з кроком {1}, к-сть доданків {2}.",sum,step,terms);
            }

        private bool canStop = false;

        public bool CanStop { get => canStop; }

        public void Stoper()
        {
            Thread.Sleep(30 * 1000);
            canStop = true;
        }
    }
}

