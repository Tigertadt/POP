package com.company;

public class Producer implements Runnable{
    private final int itemNumbers;
    private final int itemNumbers1;
    private final Manager manager;

    public Producer(int itemNumbers,int itemNumbers1, Manager manager) {
        this.itemNumbers = itemNumbers;
        this.itemNumbers1 = itemNumbers1;
        this.manager = manager;

        new Thread(this).start();
    }

    @Override
    public void run() {
        for (int i = itemNumbers; i < itemNumbers1; i++) {
            try {
                manager.full.acquire();
                manager.access.acquire();

                manager.storage.add("item " + i);
                System.out.println("Producer " + Thread.currentThread().getId() + " added item " + i);

                manager.access.release();
                manager.empty.release();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}