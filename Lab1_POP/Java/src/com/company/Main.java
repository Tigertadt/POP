package com.company;

public class Main {

    public static void main(String[] args) {
        BreakThread breakThread = new BreakThread();


        new Threads(1, breakThread).start();
        new Threads(2, breakThread).start();
        new Threads(3, breakThread).start();

        new Thread(breakThread).start();
    }

}
