with Ada.Text_IO; use Ada.Text_IO;
procedure Main is

   dim : constant integer := 100000000;
   thread_num : constant integer := 5;

   arr : array(1..dim) of integer;

   procedure Init_Arr is
   begin
      for i in 1..dim loop
         arr(i) := i;
      end loop;


      arr(3) := -12;
   end Init_Arr;
    min_value : long_long_integer := 0;
    min_index : long_long_integer := 0;


   procedure part_min(start_index, finish_index: Integer; min_value, min_index : in out Integer) is
   begin
      for i in start_index..finish_index loop
         if arr(i) < min_value then
            min_value := arr(i);
            min_index := i;
         end if;
      end loop;
   end part_min;

   task type starter_thread is
      entry start(start_index, finish_index : in Integer);
   end starter_thread;

   protected part_manager is
      procedure set_part_min(min_value, min_index: Integer);
      entry get_min(min_value, min_index: out Integer);
   private
       global_min_value : Integer := Integer'Last;
      global_min_index: Integer := 0;
   end part_manager;

   protected body part_manager is
      procedure set_part_min(min_value, min_index: Integer) is
      begin
         if min_value < global_min_value then
            global_min_value := min_value;
            global_min_index := min_index;
         end if;
      end set_part_min;

      entry get_min(min_value, min_index: out Integer)  when tasks_count = thread_num is
      begin
         min_value := global_min_value;
         min_index := global_min_index;
      end get_min;

   end part_manager;

   task body starter_thread is
      min_value: Integer := Integer'Last;
      min_index: Integer := 0;
   begin
      accept start(start_index, finish_index : in Integer) do
                part_min(start_index => start_index, finish_index => finish_index,
                  min_value => min_value, min_index => min_index);
         min_store.set_min(min_value, min_index);
      end start;
      min := part_min(start_index  => start_index,
                      finish_index => finish_index);
      part_manager.set_part_min(min);
   end starter_thread;

   procedure parallel_min(threads_num: Integer) is
      min_value: Integer := Integer'Last;
      min_index: Integer := 0;
      threads: array(0..threads_num-1) of min_thread;
   begin
      for i in 1..thread_num loop
         thread(i).start(1 + (i - 1) * dim / thread_num, 1 + i * dim / thread_num);
      end loop;
      threads(threads_num-1).start((threads_num-1)*part_len, arr_len-1);
      min_store.get_min(min_value, min_index);
      Put_Line("Paralel min: arr(" & min_index'img &") = " & min_value'Img);
   end parallel_min;

begin
   Put_Line("number of threads:");
   thread_nums := Integer'Value(Get_Line);
   Init_Arr;
   part_min(0, arr_len-1, check_min_value, check_min_index);
   Put_Line("One thread min: arr("& check_min_index'img &") = " & check_min_value'Img);
   parallel_min(thread_nums);

end Main;
