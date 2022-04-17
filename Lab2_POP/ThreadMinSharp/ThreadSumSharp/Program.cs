using System;
using System.Linq;
using System.Threading;

namespace ThreadSumSharp
{
    class Program
    {
        public static int dim = 10;
        
        public static int threadNum=4;       
        public int min = 10000;
        public int index = 0;


        private readonly Thread[] thread = new Thread[threadNum];

        static void Main(string[] args)
        {
            Program main = new Program();
            main.InitArr(dim);            
           
            Console.WriteLine("\nНайменше число масиву: {0}, його індекс: {1}", main.ParallelMin(), main.index_res);
            Console.ReadLine();
        }


        private int threadCount = 0;

        private long ParallelMin()
        {
            //int[] arr1 = new int[arr.Length/threadNum];

            //for (int i = 0; i<threadNum;i++)
            //{
            //    for(int j = 0;j<arr1.Length;j++)
            //    {

            //        arr1[j] = new int[]
            //        {                        
            //            arr[j * threadNum],
            //            arr[j * threadNum+1],
            //            arr[j * threadNum+2],
            //            arr[j * threadNum+3],
            //        };
            //        thread[i] = new Thread(StarterThread);
            //        thread[i].Start(new Bound(arr1[i], arr1[i + 1]));
            //    }

            //}

            int n = arr.Length / threadNum;
            int[] arr1 = new int[n];
            for(int j = 0; j<n;j++)
            {
                arr1[j] = arr[j];

                for (int i = 0; i < threadNum; i++)
                {
                    thread[i] = new Thread(StarterThread);
                    thread[i].Start(new Bound(arr1[0], arr1[arr1.Length-1]));
                }
            }
            

            //thread[0] = new Thread(StarterThread);
            //thread[0].Start(new Bound(0, arr.Length / 2));
            //thread[1] = new Thread(StarterThread);
            //thread[1].Start(new Bound(arr.Length / 2, arr.Length));




            lock (lockerForCount)
            {
                while (threadCount < threadNum)
                {
                    Monitor.Wait(lockerForCount);
                }

            }
            return min;
        }

        public readonly int[] arr = new int[dim];

        private int InitArr(int dim) //инициализация массива
        {
            int length = arr.Length;
            Random rand = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(0, 1000);
                arr[3] = -9;
                Console.Write("{0} ", arr[i] );
            }
            return length;
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
                index_res = this.index+1;
            
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
