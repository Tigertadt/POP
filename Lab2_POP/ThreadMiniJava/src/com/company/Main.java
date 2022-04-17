package com.company;

public class Main {


    public static void main(String[] args) {
        int dim = 100;
        int threadNum = 2;
        int min = 100000;
        int index =0 ;
        ArrClass arrClass = new ArrClass(dim, threadNum);


        System.out.println("Найменше число масиву: \n"+ arrClass.ParallelMin()+ " його індекс:  "+ arrClass.collectMin(min,index));


    }
}