using System;
using static System.Console;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter N = M: ");
            int N = int.Parse(ReadLine());
            
            if (N > 0 && N % 2 == 1)
            {
                int[,] matrix = new int[N,N];
                Write("If you want to generate control matrix, type '1'. If you want to generate random matrix, type '2': ");
                string choise = ReadLine();
                if (choise == "1")
                {
                    matrix = GenerateControlMatrix(N, N);
                }
                else if (choise == "2")
                {
                    matrix = GenerateRandomMatrix(N, N);
                }
                else
                {
                    WriteLine("You entered wrong number, rerun app");
                    return;
                }

                WriteMatrix(matrix);

                int[] result_array = new int[N*N];

                int start_position = (N - 1)/2;
                int i = start_position;
                int j = start_position;

                int min = int.MaxValue;
                int i_min = 0;
                int j_min = 0;
                
                int step = 1;
                int num = 0;
                for (int z = 1; z < 2*N; z++)
                {
                    if (z % 2 == 1)
                    {
                        if (step % 2 == 1)
                        {
                            for (int counter = 0; counter < step; counter++)
                            {
                                result_array[num] = matrix[i,j];
                                if (min > matrix[i,j])
                                {
                                    min = matrix[i,j];
                                    i_min = i;
                                    j_min = j;
                                }
                                num++;
                                i--;
                            }
                        }
                        else
                        {
                            for (int counter = 0; counter < step; counter++)
                            {
                                result_array[num] = matrix[i,j];
                                if (min > matrix[i,j])
                                {
                                    min = matrix[i,j];
                                    i_min = i;
                                    j_min = j;
                                }
                                num++;
                                i++;
                            }
                        }
                    }
                    else
                    {
                        if (step % 2 == 1)
                        {
                            for (int counter = 0; counter < step; counter++)
                            {
                                result_array[num] = matrix[i,j];
                                if (min > matrix[i,j])
                                {
                                    min = matrix[i,j];
                                    i_min = i;
                                    j_min = j;
                                }
                                num++;
                                j++;
                            }
                        }
                        else
                        {
                            for (int counter = 0; counter < step; counter++)
                            {
                                result_array[num] = matrix[i,j];
                                if (min > matrix[i,j])
                                {
                                    min = matrix[i,j];
                                    i_min = i;
                                    j_min = j;
                                }
                                num++;
                                j--;
                            }
                        }
                        step ++;
                    }
                }

                WriteLine("Result array:");
                WriteArray(result_array);

                WriteLine("Min value: {0}, Index: [{1},{2}]", min, i_min, j_min);
                if (i_min + j_min == N - 1)
                {
                    WriteLine("Element is on the secondary diagonal");
                }
                else if (i_min + j_min > N - 1)
                {
                    WriteLine("Element is under the secondary diagonal");
                }
                else
                {
                    WriteLine("Element is above the secondary diagonal");
                }
            }
            else
            {
                WriteLine("You entered incorrect number, try again");
            }
            
        }
        static void WriteMatrix(int[,] array)
        {
            WriteLine();
            int height = array.GetLength(0);
            int lenth = array.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < lenth; j++)
                {
                    Write(array[i,j] + "  ");
                }
                WriteLine();
                WriteLine();
            }
            WriteLine();
        }
        static void WriteArray(int[] array)
        {
            int lenth = array.Length;
            for (int i = 0; i < lenth; i++)
            {
                if (i < lenth - 1)
                {
                    Write(array[i] + ", ");
                }
                else
                {
                    Write(array[i] + ".");
                }
            }
            WriteLine();
        }
        static int[,] GenerateRandomMatrix(int N, int M)
        {
            Random rand = new Random();
            int[,] matrix = new int[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    matrix[i,j] = rand.Next(10, 100);
                }
            }
            return matrix;
        }
        static int[,] GenerateControlMatrix(int N, int M)
        {
            int[,] matrix = new int[N, M];
            int counter = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    matrix[i,j] = counter;
                    counter++;
                }
            }
            return matrix;
        }
    }
}
