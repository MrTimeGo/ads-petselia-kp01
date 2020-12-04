using System;
using static System.Console;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter N: ");
            int N;
            if (!int.TryParse(ReadLine(), out N) || N <= 0)
            {
                WriteLine("Entered wrong N");
                return;
            }
            //creating and filling source array
            int[] input_array = new int[N];
            FillRandom(input_array);
            
            //copying source array to sorted array
            int[] array_1 = new int[N];
            Array.Copy(input_array, array_1, N);

            //tracking swapped elements
            int[] swapped_elements_array_1 = new int[N];
            int swap_counter = 0;
            

            //sorting (1)
            for (int i = 0; i < array_1.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < array_1.Length; j++)
                {
                    if (CountDigits(array_1[j]) < CountDigits(array_1[min]))
                        min = j;
                }
                if (i != min)
                {
                    int swap = array_1[min];
                    array_1[min] = array_1[i];
                    array_1[i] = swap;
                    if (!CheckNumInArray(array_1[i], swapped_elements_array_1))
                    {
                        swapped_elements_array_1[swap_counter] = array_1[i];
                        swap_counter++;
                    }
                    if (!CheckNumInArray(array_1[min], swapped_elements_array_1))
                    {
                        swapped_elements_array_1[swap_counter] = array_1[min];
                        swap_counter++;
                    }
                }
            }
            int[] swapped_elements_array_2 = new int[N];
            Array.Copy(swapped_elements_array_1, swapped_elements_array_2, N);

            int[] array_2 = new int[N];
            Array.Copy(array_1, array_2, N);

            //sorting (2)
            for (int i = 0; i < array_2.Length - 1; i++)
            {
                int max = i;
                for (int j = i + 1; j < array_2.Length; j++)
                {
                    if (CountDigits(array_2[j]) == CountDigits(array_2[max]))
                        if (array_2[j] > array_2[max])
                            max = j;
                }
                if (i != max)
                {
                    int swap = array_2[max];
                    array_2[max] = array_2[i];
                    array_2[i] = swap;
                    if (!CheckNumInArray(array_2[i], swapped_elements_array_2))
                    {
                        swapped_elements_array_2[swap_counter] = array_2[i];
                        swap_counter++;
                    }
                    if (!CheckNumInArray(array_2[max], swapped_elements_array_2))
                    {
                        swapped_elements_array_2[swap_counter] = array_2[max];
                        swap_counter++;
                    }
                }
            }
            
            Write("Input array: ");
            WriteColorfulArray(input_array, swapped_elements_array_2);
            Write("(1): ");
            WriteColorfulArray(array_1, swapped_elements_array_1);
            Write("(2): ");
            WriteColorfulArray(array_2, swapped_elements_array_2);
        }
        static int CountDigits(int num)
        {
            int counter = 0;
            while (num != 0)
            {
                num = num/10;
                counter++;
            }
            return counter;
        }
        static void FillRandom(int[] array)
        {
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                int num = 0;
                int sign = rand.Next(0,2);
                while (CheckNumInArray(num, array) || num == 0)
                {
                    int digits = rand.Next(1, 10);
                    if (sign == 0)
                        num = -1 * rand.Next((int)Math.Pow(10, digits));
                    else
                        num = rand.Next((int)Math.Pow(10, digits));
                }
                array[i] = num;
            }
        }
        static bool CheckNumInArray(int num, int[] array)
        {
            foreach (int element in array)
            {
                if (num == element)
                    return true;
            }
            return false;
        }
        static void WriteArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i + 1 != array.Length) 
                    Write(array[i] + ", ");
                else
                    Write(array[i] + ".");
            }
            WriteLine();
        }
        static void WriteColorfulArray(int[] array, int[] swap_array)
        {
            if (CheckNumInArray(0, swap_array))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (!CheckNumInArray(array[i], swap_array))
                    {
                        ForegroundColor = ConsoleColor.Red;
                        if (i + 1 != array.Length)
                        {
                            Write(array[i]);
                            ResetColor();
                            Write(", ");
                        }
                        else
                        {
                            Write(array[i]);
                            ResetColor();
                            Write(".");
                        }
                    }
                    else
                    {
                        if (i + 1 != array.Length)
                        {
                            Write(array[i]);
                            Write(", ");
                        }
                        else
                        {
                            Write(array[i]);
                            Write(".");
                        }
                    }
                    ResetColor();
                }
                WriteLine();
            }
            else
            {
                ConsoleColor[] colors = new ConsoleColor[10] {ConsoleColor.Blue, ConsoleColor.DarkCyan, ConsoleColor.Green, ConsoleColor.DarkMagenta, ConsoleColor.DarkRed, ConsoleColor.Gray, ConsoleColor.DarkYellow, ConsoleColor.Magenta, ConsoleColor.DarkGray, ConsoleColor.Red};
                for (int i = 0; i < array.Length; i++)
                {
                    ForegroundColor = colors[CountDigits(array[i]) - 1];
                    if (i + 1 != array.Length)
                    {
                        Write(array[i]);
                        ResetColor();
                        Write(", ");
                    }
                    else
                    {
                        Write(array[i]);
                        ResetColor();
                        Write(".");
                    }
                    ResetColor();
                }
                WriteLine();
            }
        }
    }
}