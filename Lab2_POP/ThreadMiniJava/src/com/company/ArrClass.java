package com.company;

public class ArrClass {
    private final int dim;
    private final int threadNum;
    public final int[] arr;
    public int index_res;

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

    private long min = 0;

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

    synchronized public int collectMin(long min, int index){
        this.min = min;
        this.index_res  = index;
        return index;
    }

    private int threadCount = 0;
    synchronized public void incThreadCount(){
        threadCount++;
        notify();
    }

    private int getThreadCount() {
        return threadCount;
    }

    public long ParallelMin(){
        ThreadMin[] threadMins = new ThreadMin[threadNum];

        int n = arr.length / threadNum;
        int[] arr1 = new int[n];
        for(int j = 0; j<n;j++)
        {
            arr1[j] = arr[j];

            for (int i = 0; i < threadNum; i++)
            {
                threadMins[i] = new ThreadMin(arr1[0], arr1[arr1.length-1], this);
                threadMins[i].start();
            }
        }



        return getMin();
    }
}