using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adjacency_matrix
{
    class Program
    {
        static int infinity = 1000000;
        static void Main(string[] args)
        {
            int[][] adjacency_matrix = { new int[] { 0, 8, 3, 1, infinity }, new int[] { 8, 0, 4, infinity, 2 }, new int[] { 3, 4, 0, 1, 1 }, new int[] { 1, infinity, 1, 0, 8 }, new int[] { infinity, 3, 1, 8, 0 } };
            // string inputFile = File.ReadAllText("input.txt");

            int[][] distance_matrix;
            bool[][] transitive_matrix;

            Console.WriteLine("Adjacency matrix:");
            for (int x = 0; x < adjacency_matrix.Length; x++)
            {
                for (int y = 0; y < adjacency_matrix.Length; y++)
                {
                    Console.Write(adjacency_matrix[x][y] + " ");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }

            distance_matrix = floyd_Warshall(adjacency_matrix);

            Console.WriteLine("Distance matrix:");
            for (int x = 0; x < distance_matrix.Length; x++)
            {
                for (int y = 0; y < distance_matrix.Length; y++)
                {
                    Console.Write(distance_matrix[x][y] + " ");
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

        private static int[][] floyd_Warshall(int[][] adjacency_matrix)
        {
            int[][] distance_matrix = adjacency_matrix;

            for (int k = 0; k < adjacency_matrix.Length; k++)
            {
                for (int i = 0; i < adjacency_matrix.Length; i++)
                {
                    for (int j = 0; j < adjacency_matrix.Length; j++)
                    {
                        if (distance_matrix[i][k] + distance_matrix[k][j] < adjacency_matrix[i][j])
                        {
                            distance_matrix[i][j] = distance_matrix[i][k] + distance_matrix[k][j];
                        }
                    }

                }
            }
            return distance_matrix;
        }

        private static bool[][] transitive_Closure(int[][] adjacency_matrix)
        {
            bool[][] transitive_matrix = { new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false } };
            for (int a = 0; a < adjacency_matrix.Length; a++)
            {
                for (int b = 0; b < adjacency_matrix.Length; b++)
                {
                    if (adjacency_matrix[a][b] > 0 && adjacency_matrix[a][b] < infinity)
                    {
                        transitive_matrix[a][b] = true;
                    }
                    else
                    {
                        transitive_matrix[a][b] = false;
                    }
                }
            }
            for (int k = 0; k < adjacency_matrix.Length; k++)
            {
                for (int i = 0; i < adjacency_matrix.Length; i++)
                {
                    for (int j = 0; j < adjacency_matrix.Length; j++)
                    {
                        transitive_matrix[i][j] = transitive_matrix[i][j] || (transitive_matrix[i][k] && transitive_matrix[k][j]);
                    }

                }
            }
            return transitive_matrix;
        }
    }
}
