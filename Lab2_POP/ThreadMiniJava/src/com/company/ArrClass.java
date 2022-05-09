package com.company;
import java.time.*;

public class ArrClass {
    private final int dim;
    private final int threadNum;
    public final int[] arr;
    public int index_res;
    private int min = Integer.MAX_VALUE;
    private int minIdx = -1;


    public ArrClass(int dim, int threadNum) {
        this.dim = dim;
        arr = new int[dim];
        this.threadNum = threadNum;
        for(int i = 0; i < dim; i++){
            arr[i] = (int) (Math.random()*100) ;
            arr[3] = -9;
            System.out.print(" "+arr[i]);
        }
    }

    public long partMin(int startIndex, int finishIndex){
        long min = 0;

        for(int i = startIndex; i < finishIndex; i++){
            if (arr[i] < this.min) {
                min = arr[i];
                this.index_res = i;
            }
        }
        return min;
    }


    synchronized public long getMin() {
        while (getThreadCount()<threadNum){
            try {
                wait();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        return min;
    }

    private int threadCount = 0;
    synchronized public void incThreadCount(){
        threadCount++;
        notify();
    }

    private int getThreadCount() {
        return threadCount;
    }

    private void ParallelMin(int[] arr,int threadNum)
    {

       ThreadMin[] threads  = new ThreadMin[threadNum];
        minIdx = -1;

            Object _lock = new Object();

            for (int thread1 = 0; thread1 < threadNum; thread1++)
            {
                var threadId = thread1;
                threads[thread1] = new Thread(() ->
                {
                for (int i = arr.length * threadId / threadNum; i < arr.length * (threadId + 1) / threadNum; i++)
                {
                    if (arr[i] < min)
                    {
                        synchronized (_lock)
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
                threads[threadId].run();
            }

    }
}