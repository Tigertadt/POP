with Ada.Text_IO, GNAT.Semaphores;
use Ada.Text_IO, GNAT.Semaphores;

with Ada.Containers.Indefinite_Doubly_Linked_Lists;
use Ada.Containers;

procedure Main is
   package String_Lists is new Indefinite_Doubly_Linked_Lists (String);
   use String_Lists;

   procedure Starter (Storage_Size : in Integer; Item_Numbers : in Integer) is
      Storage : List;

      Access_Storage : Counting_Semaphore (1, Default_Ceiling);
      Full_Storage   : Counting_Semaphore (Storage_Size, Default_Ceiling);
      Empty_Storage  : Counting_Semaphore (0, Default_Ceiling);

      task type Producer(Item_Numbers : Integer);

      task type Consumer(Item_Numbers : Integer);

      task body Producer is
      begin
         for i in 1 .. Item_Numbers loop
            Full_Storage.Seize;
            Access_Storage.Seize;

            Storage.Append ("item " & i'Img);
            Put_Line ("Producer added item " & i'Img);

            Access_Storage.Release;
            Empty_Storage.Release;
            delay 0.5;
         end loop;

      end Producer;

      task body Consumer is
      begin
         for i in 1 .. Item_Numbers loop
            Empty_Storage.Seize;
            Access_Storage.Seize;

            Storage.Delete_First;

              Put_Line ("Consumer took " & i'Img);

            Access_Storage.Release;
            Full_Storage.Release;

            delay 0.5;
         end loop;

      end Consumer;



     t1: Producer (Item_Numbers);
     t2:  Producer (Item_Numbers);
     t3: Producer (Item_Numbers);
     t4: Consumer (Item_Numbers);
     t5: Consumer (Item_Numbers);
	  t6: Consumer (Item_Numbers);

   begin
      null;
   end Starter;

begin
   Starter (3, 41);
   null;
end main;
