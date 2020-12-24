using System;
using static System.Console;

namespace lab4
{
    class SLList
    {
        public Node head;
        public Node tail;

        public class Node
        {
            public int data;
            public Node next;

            public Node(int data)
            {
                this.data = data;
            }

            public Node(int data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }

        public SLList(int data)
        {
            head = new Node(data);
            tail = head;
        }

        public SLList()
        {
            head = null;
            tail = head;
        }

        public void AddFirst(int data)
        {
            Node newNode = new Node(data);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.next = head;
                head = newNode;
            }
        }

        public void AddToPosition(int data, int position)
        {
            Node newNode = new Node(data);

            if (position == 1 || head == null)
            {
                AddFirst(data);
            }
            else
            {
                Node current = head;

                int i = 1;
                while (i != position - 1 && current != tail)
                {
                    current = current.next;
                    i++;
                }
                if (current == tail)
                {
                    current.next = newNode;
                    tail = newNode;
                }
                else if (i == position - 1)
                {
                    newNode.next = current.next;
                    current.next = newNode;
                }
                else
                {
                    Console.WriteLine("Position of index is out of bounds");
                }
            }
        }

        public void AddLast(int data)
        {
            Node newNode = new Node(data);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.next = newNode;
                tail = newNode;
            }
        }

        public void DeleteFirst() 
        {
            if (head == null)
            {
                Console.WriteLine("There is no nodes to delete");
            }
            else if (head == tail)
            {
                head = null;
                tail = head;
            }
            else
            {
                head = head.next;
            }
        }

        public void DeleteFromPosition(int position) 
        {
            if (position == 1 || head == null)
            {
                DeleteFirst();
            }
            else
            {
                Node current = head;

                int i = 1;
                while (i != position - 1 && current != tail)
                {
                    current = current.next;
                    i++;
                }
                if (current.next == tail)
                {
                    current.next = null;
                    tail = current;
                }
                else if (i == position - 1)
                {
                    current.next = current.next.next;
                }
                else
                {
                    Console.WriteLine("Position of index is out of bounds");
                }
            }
        }

        public void DeleteLast()
        {
            if (head != null && head.next != null)
            {
                Node current = head;

                while (current.next.next != null)
                {
                    current = current.next;
                }

                current.next = null;
                tail = current;
            }
            else if (head == null)
            {
                Console.WriteLine("There is no nodes to delete");
            }
            else
                head = null;
        }
        public int GetValue(int position)
        {
            Node current = head;
            for (int i = 1; i < position; i ++)
            {
                current = current.next;
            }
            return current.data;
        }
        public int GetLength()
        {
            if (head == null)
            {
                return 0;
            }
            else
            {
                Node current = head;
                int counter = 1;
                while (current.next != null)
                {
                    current = current.next;
                    counter++;
                }
                return counter;
            }
        }
        public void Print()
        {
            Node current = head;

            while (current != null)
            {
                Console.Write(current.data);
                if (current.next != null) Console.Write(" -> ");
                current = current.next;
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
        {
            SLList list = new SLList();
            WriteLine("Welcome to console!");
            WriteLine("\nType 'help' to get list of all commands\n");
            bool exit = false;
            while (!exit)
            {
                Write("> ");
                string input = ReadLine();
                if (input == "help")
                {
                    PrintHelp();
                }
                else if (input.Contains("add first"))
                {
                    AddFirst(input, list);
                    ForegroundColor = ConsoleColor.Green;
                    Write("Current list: ");
                    list.Print();
                    ResetColor();
                }
                else if (input.Contains("add to position"))
                {
                    AddToPosition(input, list);
                    ForegroundColor = ConsoleColor.Green;
                    Write("Current list: ");
                    list.Print();
                    ResetColor();
                }
                else if (input.Contains("add last"))
                {
                    AddLast(input, list);
                    ForegroundColor = ConsoleColor.Green;
                    Write("Current list: ");
                    list.Print();
                    ResetColor();
                }
                else if (input == "delete first")
                {
                    list.DeleteFirst();
                    ForegroundColor = ConsoleColor.Green;
                    Write("Current list: ");
                    list.Print();
                    ResetColor();
                }
                else if (input.Contains("delete from position"))
                {
                    DeleteFromPosition(input, list);
                    ForegroundColor = ConsoleColor.Green;
                    Write("Current list: ");
                    list.Print();
                    ResetColor();
                }
                else if (input == "delete last")
                {
                    list.DeleteLast();
                    ForegroundColor = ConsoleColor.Green;
                    Write("Current list: ");
                    list.Print();
                    ResetColor();
                }
                else if (input == "print")
                {
                    ForegroundColor = ConsoleColor.Green;
                    Write("Current list: ");
                    list.Print();
                    ResetColor();
                }
                else if (input.Contains("task1"))
                {
                    DoTask1(input, list);
                    ForegroundColor = ConsoleColor.Green;
                    Write("Current list: ");
                    list.Print();
                    ResetColor();
                }
                else if (input == "task2.1")
                    DoTask21(list);
                else if (input.Contains("task2.2"))
                {
                    DoTask22(input, list);
                    ForegroundColor = ConsoleColor.Green;
                    Write("Current list: ");
                    list.Print();
                    ResetColor();
                }
                else if (input == "exit")
                    exit = true;
                else
                    WriteLine("Unknown command.");
            }
        }

        static void DoTask1(string input, SLList list)
        {
            string[] subcommands = input.Split(' ');
            if (subcommands.Length != 2)
                WriteLine("Invalid number of arguments.");
            else if (subcommands[0] != "task1")
                WriteLine("Incorrect command.");
            else if (!int.TryParse(subcommands[1], out int num))
                WriteLine("Entered value is not an integer number.");
            else
            {
                bool isAnyOddNumberInList = false;
                for (int i = 1; i <= list.GetLength(); i++)
                {
                    if (list.GetValue(i) % 2 == 1)
                    {
                        list.AddToPosition(num, i);
                        isAnyOddNumberInList = true;
                        break;
                    }
                }
                if (isAnyOddNumberInList != true)
                {
                    list.AddFirst(num);
                }
            }
        }
        static void DoTask21(SLList list)
        {
            SLList deletedList = new SLList();
            int length = list.GetLength();
            if (length != 0)
            {
                int tailValue = list.tail.data;
                int counter = 0;
                for (int i = 1; i <= length; i++)
                {
                    int value = list.GetValue(i);
                    if (value < tailValue)
                    {
                        counter++;
                        list.DeleteFromPosition(i);
                        deletedList.AddLast(value);
                        WriteLine("Step {0}:", counter);
                        Write("Current list: ");
                        list.Print();
                        Write("List of deleted values: ");
                        deletedList.Print();
                        i--;
                        length--;
                    }
                }
            }
            ForegroundColor = ConsoleColor.Green;
            Write("Current list: ");
            list.Print();
            ForegroundColor = ConsoleColor.Red;
            Write("List of deleted values: ");
            deletedList.Print();
            ResetColor();
        }
        static void DoTask22(string input, SLList list)
        {
            string[] subcommands = input.Split(' ');
            if (subcommands.Length != 2)
                WriteLine("Invalid number of arguments.");
            else if (subcommands[0] != "task2.2")
                WriteLine("Incorrect command.");
            else if (!int.TryParse(subcommands[1], out int num))
                WriteLine("Entered value is not an integer number.");
            else
            {
                if (num % 2 == 0)
                {
                    list.AddFirst(num);
                }
                else
                {
                    list.AddLast(num);
                }
            }
        }
        static void PrintHelp()
        {
            WriteLine("Command list:");
            WriteLine("{0}{1}", "add first NUM  ".PadRight(40, '.'), "  insert value NUM to the head of the list".PadLeft(50, '.'));
            WriteLine("{0}{1}", "add to position NUM POSITION  ".PadRight(40, '.'), "  insert value NUM to place POSITION of the list".PadLeft(50, '.'));
            WriteLine("{0}{1}", "add last NUM  ".PadRight(40, '.'), "  insert value NUM to the end of the list".PadLeft(50, '.'));
            WriteLine("{0}{1}", "delete first  ".PadRight(40, '.'), "  deletes first value from the list".PadLeft(50, '.'));
            WriteLine("{0}{1}", "delete from position POSITION  ".PadRight(40, '.'), "  deletes value from place POSITION of the list".PadLeft(50, '.'));
            WriteLine("{0}{1}", "delete last  ".PadRight(40, '.'), "  deletes the last value of the list".PadLeft(50, '.'));
            WriteLine("{0}{1}", "print  ".PadRight(40, '.'), "  prints entire list".PadLeft(50, '.'));
            WriteLine("{0}{1}", "task1 NUM  ".PadRight(40, '.'), "  do task 1".PadLeft(50, '.'));
            WriteLine("{0}{1}", "task2.1  ".PadRight(40, '.'), "  do task 2.1".PadLeft(50, '.'));
            WriteLine("{0}{1}", "task2.2 NUM  ".PadRight(40, '.'), "  do task 2.2".PadLeft(50, '.'));
            WriteLine("{0}{1}", "exit  ".PadRight(40, '.'), "  stops the program".PadLeft(50, '.'));
        }
        static void AddFirst(string input, SLList list)
        {
            string[] subcommands = input.Split(' ');
            if (subcommands.Length != 3)
                WriteLine("Invalid number of arguments.");
            else if (subcommands[0] != "add" || subcommands[1] != "first")
                WriteLine("Incorrect command.");
            else if (!int.TryParse(subcommands[2], out int num))
                WriteLine("Entered value is not an integer number.");
            else
                list.AddFirst(num);
        }
        static void AddToPosition(string input, SLList list)
        {
            string[] subcommands = input.Split(' ');
            if (subcommands.Length != 5)
                WriteLine("Invalid number of arguments.");
            else if (subcommands[0] != "add" || subcommands[1] != "to" || subcommands[2] != "position")
                WriteLine("Incorrect command.");
            else if (!int.TryParse(subcommands[3], out int num))
                WriteLine("Entered value is not an integer number.");
            else if (!int.TryParse(subcommands[4], out int position) && position < 0)
                WriteLine("Entered position is incorrect.");
            else
                list.AddToPosition(num, position);
        }
        static void AddLast(string input, SLList list)
        {
            string[] subcommands = input.Split(' ');
            if (subcommands.Length != 3)
                WriteLine("Invalid number of arguments.");
            else if (subcommands[0] != "add" || subcommands[1] != "last")
                WriteLine("Incorrect command.");
            else if (!int.TryParse(subcommands[2], out int num))
                WriteLine("Entered value is not an integer number.");
            else
                list.AddLast(num);
        }
        static void DeleteFromPosition(string input, SLList list)
        {
            string[] subcommands = input.Split(' ');
            if (subcommands.Length != 4)
                WriteLine("Invalid number of arguments.");
            else if (subcommands[0] != "delete" || subcommands[1] != "from" || subcommands[2] != "position")
                WriteLine("Incorrect command.");
            else if (!int.TryParse(subcommands[3], out int position) && position < 0)
                WriteLine("Entered position is incorrect.");
            else
                list.DeleteFromPosition(position);
        }
    }
}