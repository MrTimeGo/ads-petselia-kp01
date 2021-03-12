using System;
using static System.Console;
using System.Threading;

namespace lab6
{
    class Stack
    {
        private int top;
        private int size;
        private int[] _array;

        public string name; // just for output

        public Stack(int capacity, string name)
        {
            this.name = name;
            this._array = new int[capacity];
            this.size = 0;
        }
        public void Push(int number)
        {
            if (this.size == this._array.Length)
            {
                throw new StackOverflowException();
            }
            this._array[this.size] = number;
            this.top = this._array[this.size];
            this.size++;
        }
        public int Pop()
        {
            if (this.size == 0)
            {
                throw new Exception("Empty stack");
            }
            this.size--;
            if (this.size != 0)
                this.top = this._array[size - 1];
            return this._array[size];
        }
        public int Peek()
        {
            if (this.size == 0)
            {
                throw new Exception("Empty stack");
            }
            return top;
        }
        public bool IsStackEmpty()
        {
            return this.size == 0;
        }
        public int Size
        { 
            get { return this.size; }
        }
    }
    class Program
    {
        static void FillStack(Stack stack, int N)
        {
            for (int i = N; i > 0; i--)
            {
                stack.Push(i);
            }
        }
        static void MoveDisks(Stack fromStack, Stack toStack, Stack buffStack, int N, int constN)
        {
            if (N > 0)
            {
                MoveDisks(fromStack, buffStack, toStack, N - 1, constN);
                toStack.Push(fromStack.Pop());
                WriteThreeStacks(fromStack, toStack, buffStack, constN); 
                MoveDisks(buffStack, toStack, fromStack, N - 1, constN);
            }
        }
        static Stack ChooseStack(string name, Stack stack1, Stack stack2, Stack stack3)
        {
            if (stack1.name == name)
            {
                return stack1;
            }
            else if (stack2.name == name)
            {
                return stack2;
            }
            return stack3;
        }
        static void WriteStack(Stack stack, int N)
        {
            Stack buffStack = new Stack(N, "buff");
            int startPositionX = CursorLeft;
            int width = N;
            for (int i = 2; i < N + 2; i++)
            {
                
                if (N - i + 2 > stack.Size)
                {
                    SetCursorPosition(N + startPositionX, i);
                    Write("N");
                }
                else
                {
                    int length = stack.Pop();
                    SetCursorPosition(N - length + startPositionX, i);
                    buffStack.Push(length);
                    for (int j = 0; j < length; j++)
                    {
                        Write("~");
                    }
                    Write("N");
                    for (int j = 0; j < length; j++)
                    {
                        Write("~");
                    }
                }
                if(i == N + 1)
                {
                    SetCursorPosition(startPositionX, i+1);
                    for (int j = 0; j < 2*N + 1; j++)
                    {
                        Write("N");
                    }
                    SetCursorPosition(N + startPositionX, i + 3);
                    Write(stack.name);
                }
                width--;
            }

            WriteLine();

            int stackLength = buffStack.Size;
            for (int i = 0; i < stackLength; i++)
            {
                stack.Push(buffStack.Pop());
            }
        }
        static void WriteThreeStacks(Stack stack1, Stack stack2, Stack stack3, int N)
        {
            Stack stackA = ChooseStack("A", stack1, stack2, stack3);
            Stack stackB = ChooseStack("B", stack1, stack2, stack3);
            Stack stackC = ChooseStack("C", stack1, stack2, stack3);

            Clear();
            WriteStack(stackA, N);
            SetCursorPosition(2 * N + 4, 0);
            WriteStack(stackB, N);
            SetCursorPosition(4 * N + 8, 0);
            WriteStack(stackC, N);
            Thread.Sleep(500);
        }
        static int ReadN()
        {
            Write("Enter number of disks: ");
            string input = ReadLine();
            int N;
            while (!int.TryParse(input, out N) || N <= 0)
            {
                WriteLine("Incorrect number. Try again.");
                Write("Enter number of disks: ");
                input = ReadLine();
            }
            return N;
        }
        static void Main()
        {
            bool exit = false;
            WriteLine("Welcome to lab6.");
            while (!exit)
            {
                WriteLine("To use contol example type \"control\". To set custom amount of disks type \"set\". To exit type \"exit\"");
                Write("> ");
                string input = ReadLine();
                if (input == "set" || input == "control")
                {
                    int N = input == "set" ? ReadN() : 5;
                    Stack stackA = new Stack(N, "A");
                    Stack stackB = new Stack(N, "B");
                    Stack stackC = new Stack(N, "C");

                    FillStack(stackA, N);
                    WriteThreeStacks(stackA, stackB, stackC, N);
                    MoveDisks(stackA, stackC, stackB, N, N);
                }
                else if (input == "exit")
                {
                    exit = true;
                }
                else
                {
                    WriteLine("Incorrect command");
                }
            }
        }
    }
}