using System;
using System.Threading;

namespace ThreadSumSharp
{
    class Program
    {
        private static readonly int dim = 100;
        private static readonly int threadNum = 2;
        public int min = 100000;
        public int index = 0;


        private readonly Thread[] thread = new Thread[threadNum];

        static void Main(string[] args)
        {
            Program main = new Program();
            main.InitArr();
           
            Console.WriteLine("\nНайменше число масиву: {0}, його індекс: {1}", main.ParallelMin(), main.index_res);
            Console.ReadLine();
        }


        private int threadCount = 0;

        private long ParallelMin()
        {
            thread[0] = new Thread(StarterThread);
            thread[0].Start(new Bound(0, dim / 2));
            thread[1] = new Thread(StarterThread);
            thread[1].Start(new Bound(dim / 2, dim));
           

            lock (lockerForCount)
            {
                while (threadCount < threadNum)
                {
                    Monitor.Wait(lockerForCount);
                }

            }
            return min;
        }

        private readonly int[] arr = new int[dim];

        private void InitArr() //инициализация массива
        {
            Random rand = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(-1000, 1000);
                Console.Write("{0} ", arr[i] );
            }
        }

        class Bound
        {
            public Bound(int startIndex, int finishIndex) // метод определения размера массива
            {
                StartIndex = startIndex;
                FinishIndex = finishIndex;
            }

            public int StartIndex { get; set; }
            public int FinishIndex { get; set; }
        }

        private readonly object lockerForSum = new object();

        private void StarterThread(object param) // создание потока
        {
            if (param is Bound)
            {
                int mini = PartMin((param as Bound).StartIndex, (param as Bound).FinishIndex);

                lock (lockerForSum)
                {                    
                        CollectMin(mini,index);
                    
                }
                IncThreadCount();
            }
        }

        private readonly object lockerForCount = new object();
        private void IncThreadCount()
        {

            lock (lockerForCount)
            {          
                    threadCount++;
                    Monitor.Pulse(lockerForCount);           
                               
            }
        }

        private int min_res = 0, index_res = 0;     
        public void CollectMin(int min, int index)
        {
            if (this.min<min_res)
            {
                min_res = this.min;
                index_res = this.index;
            
            }
        }

        public int PartMin(int startIndex, int finishIndex)// определения найменьшего числа массива
        {           
            for (int i = startIndex; i < finishIndex; i++)
            {
                if (arr[i] < this.min)
                {
                    min = arr[i];
                    index = i;
                }
            }
            return min;

           

        }
    }
}
