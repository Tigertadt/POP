package com.company;

public class Main {

    public static void main(String[] args) {

        System.out.println("Main thread started...");
        MyThread myThread = new MyThread(1);
        MyThread myThreads = new MyThread(2);

        new Thread(myThread,"MyThread").start();
        new Thread(myThreads,"MyThread").start();

        try{
            Thread.sleep(1100);

            myThread.disable();
        }
        catch(InterruptedException e){
            System.out.println("Thread has been interrupted");
        }
        System.out.println("Main thread finished...");
    }
}
