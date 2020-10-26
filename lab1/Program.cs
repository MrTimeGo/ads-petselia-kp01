using System;
using static System.Console;
using static System.Math;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            WriteLine("Part 1");
            Write("Enter x: ");
            int x = int.Parse(ReadLine());
            Write("Enter y: ");
            int y = int.Parse(ReadLine());
            Write("Enter z: ");
            int z = int.Parse(ReadLine());

            if (y == 1)
            {
                WriteLine("Entered y is out of range of valid numbers");
            }
            else if (z == 0)
            {
                WriteLine("Entered z is out of range of valid numbers");
            }
            else
            {
                double a = Pow(x + y*y + 2*z, (double)1/3)/(Abs(1-y)*z*z);
                if (a == 0)
                {
                    WriteLine("Calulated a is out of range of valid numbers, can't calculate b");
                }
                else
                {
                    double b = Sin(x*x/a + y*z*z*z);
                    WriteLine("a = " + a);
                    WriteLine("b = " + b);
                }
            }
            WriteLine("-----------------------");
            //2
            WriteLine("Part 2");
            WriteLine();
            Write("Enter day: ");
            int d = int.Parse(ReadLine());
            Write("Enter month: ");
            int m = int.Parse(ReadLine());
            if (isDateCorrect(d, m))
            {
                int counter = 0;
                for (int i = 1; i < m; i++)
                {
                    if(i == 2)
                    {
                        counter += 28;
                    }
                    else
                    {
                        if (Check31(i))
                        {
                            counter += 31;
                        }
                        else
                        {
                            counter += 30;
                        }
                    
                    }
                }
                counter += d;
                int day_of_week = counter % 7;
                string day;
                if(day_of_week == 0)
                {
                    day = "Tuesday";
                }
                else if(day_of_week == 1)
                {
                    day = "Wednesday";
                }
                else if(day_of_week == 2)
                {
                    day = "Thursday";
                }
                else if(day_of_week == 3)
                {
                    day = "Friday";
                }
                else if(day_of_week == 4)
                {
                    day = "Saturday";
                }
                else if(day_of_week == 5)
                {
                    day = "Sunday";
                }
                else
                {
                    day = "Monday";
                }
                WriteLine(day);
            }
            else
            {
                WriteLine("Entered date is incorrect");
            }
        }
        static bool Check31(int m)
        {
            if (m <= 7)
            {
                if (m % 2 != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (m % 2 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static bool isDateCorrect(int d, int m)
        {
            if (m == 2)
            {
                if(d > 28)
                {
                    return false;
                }        
            }
            else
            {
                if (Check31(m))
                {
                    if(d > 31)
                    {
                        return false;
                    }
                }
                else
                {
                    if(d > 30)
                    {
                        return false;
                    }
                }
            }
            if (m >= 1)
            {
                if (m <= 12)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
