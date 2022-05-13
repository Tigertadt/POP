with Ada.Text_IO; use Ada.Text_IO;
procedure Main is

   dim : constant integer := 1000;
   thread_num : constant integer := 5;

   arr : array(1..dim) of integer;

   procedure Init_Arr is
   begin
      for i in 1..dim loop
         arr(i) := i;
      end loop;


   end Init_Arr;

   function part_min(start_index, finish_index : in integer) return long_long_integer is
      min : long_long_integer := 0;
   begin
      for i in start_index..finish_index-1 loop
         if long_long_integer(arr(i)) < min then
            min := long_long_integer(arr(i));
         end if;
      end loop;
      return min;
   end part_min;

   function part_min_index(start_index, finish_index : in integer) return long_long_integer is
      min : long_long_integer := 0;
      min_index: Long_Long_Integer := 0;
   begin
      for i in start_index..finish_index-1 loop
         if long_long_integer(arr(i)) < min then
            min := long_long_integer(arr(i));
            min_index := Long_Long_Integer(i);
         end if;
      end loop;
      return min_index;
   end part_min_index;


   task type starter_thread is
      entry start(start_index, finish_index : in Integer);
   end starter_thread;

   protected part_manager is
      procedure set_part_min(min, min_index : in Long_Long_Integer);
      entry get_min(min, min_index : out Long_Long_Integer);
   private
      tasks_count : Integer := 0;
      min1 : Long_Long_Integer:=0;
      min_index1 : Long_Long_Integer:=0;
   end part_manager;

   protected body part_manager is
      procedure set_part_min(min,min_index : in Long_Long_Integer) is
      begin
          if long_long_integer(min) < min1 then
            min1 := long_long_integer(min);
             end if;
         min_index1 := Long_Long_Integer(part_min_index(1,dim));
         tasks_count := tasks_count + 1;
      end set_part_min;

      entry get_min(min,min_index: out Long_Long_Integer) when tasks_count = thread_num is
      begin
         min := min1;
         min_index := min_index1;
      end get_min;

   end part_manager;

   task body starter_thread is
      min : Long_Long_Integer := 0;
      min_index : Long_Long_Integer := 0;
      start_index, finish_index : Integer;
   begin
      accept start(start_index, finish_index : in Integer) do
         starter_thread.start_index := start_index;
         starter_thread.finish_index := finish_index;
      end start;
      min := part_min(start_index  => start_index,
                      finish_index => finish_index);
      min_index := part_min_index(start_index  => start_index,
                      finish_index => finish_index);
      part_manager.set_part_min(min, min_index);
   end starter_thread;

   procedure parallel_min(thread_num: Integer) is
      min : long_long_integer := 0;
      min_index : long_long_integer := 0;
      thread : array(1..thread_num) of starter_thread;
   begin

      for i in 1..thread_num loop
         thread(i).start(1 + (i - 1) * dim / thread_num, 1 + i * dim / thread_num);
      end loop;

      part_manager.get_min(min,min_index);
      Put_Line("Paralel min: arr("&min_index'img &") = " & min'Img);

   end parallel_min;

begin
   Init_Arr;

   Ada.Text_IO.Put_Line(part_min_index(1,dim)'img & part_min(1, dim)'img);
    parallel_min(thread_num);

end Main;
