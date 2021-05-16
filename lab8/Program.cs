using System;
using static System.Console;

namespace lab8
{
    class Program
    {
        static void Main()
        {
            RedBlackTree<string> tree = new RedBlackTree<string>();
            WriteLine("To get help, print 'help'");
            bool exit = false;
            while(!exit)
            {
                Write("> ");
                string command = ReadLine();
                if (command == "exit")
                {
                    exit = true;
                }
                else if (command == "help")
                {
                    PrintHelp();
                }
                else if (command.StartsWith("add "))
                {
                    string[] subcommands = command.Split(" ");
                    if (subcommands.Length != 2)
                    {
                        WriteLine("Unknown command");
                        continue;
                    }
                    if (!double.TryParse(subcommands[1], out double key))
                    {
                        WriteLine("Key should be a number");
                        continue;
                    }
                    try
                    {
                        tree.Insert(key);
                    }
                    catch
                    {
                        WriteLine($"Cannot add new node, because there is the same one.");
                    }
                }
                else if (command == "print")
                {
                    WriteLine();
                    tree.Print();
                    WriteLine();
                }
                else
                {
                    WriteLine("Unknown command");
                }
            }
        }
        static void PrintHelp()
        {
            WriteLine("add {key}\nprint\nexit");
        }
    }
}
