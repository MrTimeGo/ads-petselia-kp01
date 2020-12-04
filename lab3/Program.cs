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
            
            Write("Input array: ");
            WriteColorfulArray(input_array);
            //copying source array to sorted array
            int[] array = new int[N];
            Array.Copy(input_array, array, N);
            

            //sorting (1)
            for (int i = 0; i < array.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (CountDigits(array[j]) < CountDigits(array[min]))
                        min = j;
                }
                if (i != min)
                {
                    int swap = array[min];
                    array[min] = array[i];
                    array[i] = swap;
                }
            }
            Write("(1):  ");
            WriteColorfulArray(array);
            
            //sorting (2)
            for (int i = 0; i < array.Length - 1; i++)
            {
                int max = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (CountDigits(array[j]) == CountDigits(array[max]))
                        if (array[j] > array[max])
                            max = j;
                }
                if (i != max)
                {
                    int swap = array[max];
                    array[max] = array[i];
                    array[i] = swap;
                }
            }
            Write("(2):  ");
            WriteColorfulArray(array);
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
                while (CheckNumInArray(num, array))
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
        static void WriteColorfulArray(int[] array)
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