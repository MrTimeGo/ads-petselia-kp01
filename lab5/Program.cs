using System;
using static System.Console;
using System.Collections.Generic;

namespace lab5
{
    class Program
    {
        static LinkedListNode<double> GetNodeByIndex(LinkedList<double> list, int index)
        {
            LinkedListNode<double> current = list.First;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current;
        }
        static void WriteList(LinkedList<double> list, LinkedList<ConsoleColor> colors)
        {
            LinkedListNode<double> current = list.First;
            LinkedListNode<ConsoleColor> currentColor = colors.First;
            ForegroundColor = currentColor.Value;
            Write(current.Value);
            ResetColor();
            for (int i = 0; i < list.Count - 1; i++)
            {
                current = current.Next;
                currentColor = currentColor.Next;
                Write(" --> ");
                ForegroundColor = currentColor.Value;
                Write(current.Value);
                ResetColor();
            }
            WriteLine();
        }
        static LinkedList<double> ReadAllNumbers(int N)
        {
            LinkedList<double> list = new LinkedList<double>();
            WriteLine("Enter values:");
            while (list.Count != N)
            {
                Write("> ");
                double number;
                if (!double.TryParse(ReadLine(), out number))
                {
                    WriteLine("Incorrect input");
                }
                else if (list.Contains(number))
                {
                    WriteLine("Number is already in list");
                }
                else
                {
                    list.AddLast(number);
                }
            }
            return list;
        }
        static void MergeSort(LinkedList<double> list, int low, int high)
        {
            if (low < high)
            {
                int mid = (low + high) / 2;
                MergeSort(list, low, mid);
                MergeSort(list, mid + 1, high);
                Merge(list, low, mid, high);
            }
        }
        static void Merge(LinkedList<double> list, int low, int mid, int high)
        {
            LinkedList<double> listCopy = new LinkedList<double>(list);
            int listIndex = low;
            int firstPartIndex = low;
            int secondPartIndex = mid + 1;
            while (listIndex != high + 1)
            {
                LinkedListNode<double> firstPartNode = GetNodeByIndex(listCopy, firstPartIndex);
                LinkedListNode<double> secondPartNode = GetNodeByIndex(listCopy, secondPartIndex);
                if (firstPartIndex == mid + 1)
                {
                    GetNodeByIndex(list, listIndex).Value = secondPartNode.Value;
                    secondPartIndex++;
                    listIndex++;
                }
                else if (secondPartIndex == high + 1)
                {
                    GetNodeByIndex(list, listIndex).Value = firstPartNode.Value;
                    firstPartIndex++;
                    listIndex++;
                }
                else if (Math.Abs(firstPartNode.Value) > Math.Abs(secondPartNode.Value))
                {
                    GetNodeByIndex(list, listIndex).Value = secondPartNode.Value;
                    secondPartIndex++;
                    listIndex++;
                }
                else if (Math.Abs(firstPartNode.Value) < Math.Abs(secondPartNode.Value))
                {
                    GetNodeByIndex(list, listIndex).Value = firstPartNode.Value;
                    firstPartIndex++;
                    listIndex++;
                }
            }
        }
        static void InsertionSort(LinkedList<double> list, int low, int high)
        {
            for (int i = low + 1; i <= high; i++)
            {
                double key = GetNodeByIndex(list, i).Value;
                int j = i - 1;
                while (j >= low && Math.Abs(GetNodeByIndex(list, j).Value) > Math.Abs(key))
                {
                    GetNodeByIndex(list, j + 1).Value = GetNodeByIndex(list, j).Value;
                    j--;
                }
                GetNodeByIndex(list, j + 1).Value = key;
            }
        }
        static void SortList(LinkedList<double> list, int M, LinkedList<ConsoleColor> colors, LinkedList<ConsoleColor> colorsSorted)
        {
            LinkedListNode<double> current = list.First;
            bool onSubSequence = false;
            int low = 0;
            int high = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if(current.Value < 0 && !onSubSequence)
                {
                    onSubSequence = true;
                    low = i;
                }
                else if ((current.Value >= 0 && onSubSequence) || (i == list.Count - 1 && current.Value < 0))
                {
                    high = i == list.Count - 1 && current.Value < 0 ? i : i - 1;
                    int numberOfNegatives = high - low + 1;
                    if (numberOfNegatives < M)
                    {
                        InsertionSort(list, low, high);
                        for (int j = 0; j < numberOfNegatives; j++)
                        {
                            colors.AddLast(ConsoleColor.Green);
                            colorsSorted.AddLast(ConsoleColor.Green);
                        }
                    }
                    else
                    {
                        MergeSort(list, low, high);
                        for (int j = 0; j < numberOfNegatives; j++)
                        {
                            colors.AddLast(ConsoleColor.Yellow);
                            colorsSorted.AddLast(ConsoleColor.Yellow);
                        }
                    }
                    onSubSequence = false;
                    if (current.Value >= 0)
                    {
                        colorsSorted.AddFirst(ConsoleColor.Gray);
                        colors.AddLast(ConsoleColor.Gray);
                    }
                }
                else if (current.Value >= 0)
                {
                    colorsSorted.AddFirst(ConsoleColor.Gray);
                    colors.AddLast(ConsoleColor.Gray);
                }
                current = current.Next;
            }
            current = list.First;
            bool firstNode = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (firstNode && current.Value >= 0)
                {
                    firstNode = false;
                }
                else if (current.Value >= 0)
                {
                    LinkedListNode<double> prev = current.Previous;
                    list.Remove(prev.Next);
                    list.AddFirst(current.Value);
                    current = prev;
                }
                current = current.Next;
            }
        }
        static void Main(string[] args)
        {
            bool exit = false;
            WriteLine("Welcome to lab5!\nIf you want to enter values by yourself, type \"value\", else if you want to choose control example, type \"control\". To exit the program, type \"exit\"");
            while (!exit)
            {
                Write("> ");
                string enter = ReadLine();
                if (enter == "value" || enter == "control")
                {
                    LinkedList<double> list;
                    if (enter == "value")
                    {
                        Write("Enter N: ");
                        if (!int.TryParse(ReadLine(), out int N) && N <= 0)
                        {
                            WriteLine("Incorect N");
                            continue;
                        }
                        list = ReadAllNumbers(N);
                    }
                    else
                    {
                        list = new LinkedList<double>(new double[] { 0, 1, -3, -1, -2, 5, -6, -5, 10 });
                    }
                    Write("Enter M: ");
                    if (!int.TryParse(ReadLine(), out int M) || M < 0)
                    {
                        WriteLine("Incorrect M");
                        continue;
                    }
                    LinkedList<double> sortedList = new LinkedList<double>(list);
                    LinkedList<ConsoleColor> colors = new LinkedList<ConsoleColor>();
                    LinkedList<ConsoleColor> colorsSorted = new LinkedList<ConsoleColor>();
                    SortList(sortedList, M, colors, colorsSorted);
                    WriteLine("Current list: ");
                    WriteList(list, colors);
                    WriteLine("Sorted list: ");
                    WriteList(sortedList, colorsSorted);
                }
                else if (enter == "exit")
                {
                    break;
                }
                else
                {
                    WriteLine("Incorrect command");
                }
            }
        }
    }
}