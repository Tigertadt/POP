package com.company;

public class Threads extends Thread {
    private final int id;
    private final BreakThread breakThread;
    public Threads(int id, BreakThread breakThread) {
        this.id = id;
        this.breakThread = breakThread;

    }

    @Override
    public void run() {
        long sum = 0;
        int step = 3;
        long terms =0;
        boolean isStop = false;
        do{
            sum= sum+ step;
            terms++;
            isStop = breakThread.isCanBreak();
        } while (!isStop);
        System.out.println(id + " - " + sum + "; Step " + step + "; Terms - "+ terms);
    }

}
