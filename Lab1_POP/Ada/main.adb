with Ada.Text_IO;

procedure Main is

   can_stop : boolean := false;
   pragma Atomic(can_stop);

   task type break_thread;
   task type main_thread;

   task body break_thread is
   begin
      delay 30.0;
      can_stop := true;
   end break_thread;

   task body main_thread is
      sum : Long_Long_Integer := 0;
      step : Long_Long_Integer:= 3;
      terms : Long_Long_Integer := 0;
   begin
      loop
         sum := sum + step;
         terms:=terms+1;
         exit when can_stop;
      end loop;
      delay 1.0;

      Ada.Text_IO.Put_Line(sum'Img%terms'Img);


   end main_thread;

   b1 : break_thread;
   t1 : main_thread;
   t2 : main_thread;
   t3 : main_thread;
   t4 : main_thread;
begin
   null;
end Main;
