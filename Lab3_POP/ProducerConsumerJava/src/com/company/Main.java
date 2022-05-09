package com.company;

public class Main {

    public static void main(String[] args) {
        Main main = new Main();
        int storageSize = 3;
        int itemNumbers = 10;
        int producers = 2;
        int consumers = 5;
        main.starter(storageSize, itemNumbers,producers,consumers);
    }

    private void starter(int storageSize, int itemNumbers,int producers,int consumers) {
        Manager manager = new Manager(storageSize);

        for (int i = 0;i<producers;i++)
        {
           new Producer(itemNumbers/producers*i,itemNumbers/producers*(i+1),manager);
        }

        for(int i = 0;i<producers;i++)
        {
            new Consumer((i + 1) * itemNumbers / consumers - i * itemNumbers / consumers, manager);
        }


    }
}