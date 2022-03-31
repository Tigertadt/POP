package com.company;

public class MyThread implements Runnable {

    private boolean isActive;
    private int id;

    public MyThread(int id) {
        this.id = id;
    }

    void disable(){
        isActive=false;
    }

    MyThread(){
        isActive = true;
    }

    public void run(){

        System.out.printf("%s started... \n", Thread.currentThread().getName());
        int counter=1; // счетчик циклов
        long sum = 0;
        int step = 3;
        long terms =0;
        while(isActive){
            sum = sum + step;
            terms++;
            try{

                Thread.sleep(400);
            }
            catch(InterruptedException e){
                System.out.println("Thread has been interrupted");
            }
        }
        System.out.printf("%s finished... \n", sum);
    }
}