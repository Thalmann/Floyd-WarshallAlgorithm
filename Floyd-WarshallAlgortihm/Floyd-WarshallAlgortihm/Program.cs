using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adjacency_matrix
{
    class Program
    {
        static int infinity = 1000;
        static int tableSize = 0;

        static void Main(string[] args)
        {
            string inputFile = null;
            if (File.Exists("input.txt"))
                inputFile = File.ReadAllText("input.txt");
            else
            {
                Console.WriteLine("File input.txt was not found!");
                Console.WriteLine("Creating input.txt and opening it with notepad:");
                Process.Start("notepad.exe", "input.txt");
                Console.WriteLine("Press any key to continue..");
                Console.ReadKey();
                inputFile = File.ReadAllText("input.txt");
            }

            string temp = inputFile.Replace("\r\n", "").Replace(",","");

            calculateTableSize(temp);

            int[,] adjacency_matrix = new int[tableSize, tableSize];
            int[,] distance_matrix = new int[tableSize, tableSize];
            bool[,] transitive_matrix = new bool[tableSize, tableSize];

            inputFile = inputFile.Replace("\r\n", "");
            string[] output = Regex.Split(inputFile, ",|(;)");

            int x = 0, y = 0;
            for (int j = 0; j < output.Length; j++)
            {
                if (!String.IsNullOrWhiteSpace(output[j]))
                {
                    if (output[j] != ";")
                    {
                        adjacency_matrix[x, y] = Convert.ToInt32(output[j]);
                        y++;
                    }
                    if (output[j] == ";")
                    {
                        x++;
                        y = 0;
                    }
                }
            }
            


            printMatrix(adjacency_matrix);

            distance_matrix = floyd_Warshall(adjacency_matrix);

            printMatrix(distance_matrix);

            transitive_matrix = transitive_Closure(adjacency_matrix);

            printMatrix(transitive_matrix);

            Console.ReadKey();
        }

        private static void calculateTableSize(string inputFile)
        {
            for (int i = 0; i < inputFile.Length; i++)
            {
                if (inputFile[i] == ';')
                {
                    tableSize = i;
                    break;
                }
            }
        }

        private static void printMatrix<T>(T[,] adjacency_matrix)
        {
            Console.WriteLine("Adjacency matrix:");
            for (int z = 0; z < tableSize; z++)
            {
                for (int c = 0; c < tableSize; c++)
                {
                    Console.Write(adjacency_matrix[z, c] + " ");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private static int[,] floyd_Warshall(int[,] adjacency_matrix)
        {
            int[,] distance_matrix = adjacency_matrix;

            for (int k = 0; k < tableSize; k++)
            {
                for (int i = 0; i < tableSize; i++)
                {
                    for (int j = 0; j < tableSize; j++)
                    {
                        if (distance_matrix[i,k] + distance_matrix[k,j] < adjacency_matrix[i,j])
                        {
                            distance_matrix[i,j] = distance_matrix[i,k] + distance_matrix[k,j];
                        }
                    }

                }
            }
            return distance_matrix;
        }

        private static bool[,] transitive_Closure(int[,] adjacency_matrix)
        {
            bool[,] transitive_matrix = new bool[tableSize, tableSize];
            for (int a = 0; a < tableSize; a++)
            {
                for (int b = 0; b < tableSize; b++)
                {
                    if (adjacency_matrix[a,b] > 0 && adjacency_matrix[a,b] < infinity)
                    {
                        transitive_matrix[a,b] = true;
                    }
                    else
                    {
                        transitive_matrix[a,b] = false;
                    }
                }
            }
            for (int k = 0; k < tableSize; k++)
            {
                for (int i = 0; i < tableSize; i++)
                {
                    for (int j = 0; j < tableSize; j++)
                    {
                        transitive_matrix[i,j] = transitive_matrix[i,j] || (transitive_matrix[i,k] && transitive_matrix[k,j]);
                    }

                }
            }
            return transitive_matrix;
        }
    }
}
