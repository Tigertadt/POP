using System;
using System.Linq;
using System.Threading;

namespace ThreadSumSharp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var arr = new int[10];
            var random = new Random();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next();
                arr[9] = -3;
            }

            FindMain(arr, 1);
            FindMain(arr, 2);
            FindMain(arr, 3);
            FindMain(arr, 8);
            FindMain(arr, 9);
        }

        private static void FindMain(int[] arr, int threadsNum)
        {
            var threads = new Thread[threadsNum];

            var min = int.MaxValue;
            var minIdx = -1;
            object _lock = new();

            for (int threadId = 0; threadId < threads.Length; threadId++)
            {
                var threadIdLocal = threadId;
                threads[threadId] = new Thread(() =>
                {
                    for (int i = arr.Length * threadIdLocal / threadsNum; i < arr.Length * (threadIdLocal + 1) / threadsNum; i++)
                    {
                        if (arr[i] < min)
                        {
                            lock (_lock)
                            {
                                if (arr[i] < min)
                                {
                                    min = arr[i];
                                    minIdx = i;
                                }
                            }
                        }
                    }
                });

                threads[threadId].Start();
            }

            foreach (var item in threads)
            {
                item.Join();
            }

            Console.WriteLine($"{threadsNum} threads: min is {min} (index {minIdx})");
        }


    }
}
