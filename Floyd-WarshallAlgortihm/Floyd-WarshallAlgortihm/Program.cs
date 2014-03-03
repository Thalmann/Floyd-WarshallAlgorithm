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
        static int infinity = 1000000;
        static int tableSize = 3;

        static void Main(string[] args)
        {
            int[,] adjacency_matrix = new int[tableSize, tableSize];

            string inputFile = null;
            if (File.Exists("input.txt"))
                inputFile = File.ReadAllText("input.txt");
            else
            {
                Console.WriteLine("File input.txt was not found!");
                Console.WriteLine("Creating input.txt and opening it with notepad:");
                Process.Start("notepad.exe", "input.txt");
            }

            inputFile = inputFile.Replace("\r\n", "").Replace(",","");

            int x = 0, y = 0;
            for (int j = 0; j < inputFile.Length; j++)
            {
                if (inputFile[j] != ';')
                {
                    adjacency_matrix[x, y] = Convert.ToInt32(inputFile[j].ToString());
                    y++;
                }
                if (inputFile[j] == ';')
                {
                    x++;
                    y = 0;
                }
            }
            


            int[,] distance_matrix = new int[tableSize,tableSize];
            bool[,] transitive_matrix = new bool[tableSize, tableSize];

            Console.WriteLine("Adjacency matrix:");
            for (int z = 0; z < tableSize; z++)
            {
                for (int c = 0; c < tableSize; c++)
                {
                    Console.Write(adjacency_matrix[z,c] + " ");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }

            distance_matrix = floyd_Warshall(adjacency_matrix);

            Console.WriteLine("Distance matrix:");
            for (int z = 0; z < tableSize; z++)
            {
                for (int c = 0; c < tableSize; c++)
                {
                    Console.Write(distance_matrix[z,c] + " ");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }

            transitive_matrix = transitive_Closure(adjacency_matrix);

            //Console.WriteLine("Transitive Matrix:");
            //for (int x = 0; x < transitive_matrix.Length; x++)
            //{
            //    for (int y = 0; y < transitive_matrix.Length; y++)
            //    {
            //        Console.Write(transitive_matrix[x][y] + " ");
            //    }
            //    Console.WriteLine("");
            //    Console.WriteLine("");
            //}

            Console.ReadKey();
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
