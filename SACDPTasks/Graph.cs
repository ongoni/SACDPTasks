using System;
using System.Collections.Generic;
using System.IO;

namespace SACDPTasks
{
    class Graph
    {
        private GraphNode graph;

        public Graph()
        {
            graph = null;
        }

        public void ReadFromFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                int n = int.Parse(sr.ReadLine());
                int[,] a = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    string line = sr.ReadLine();
                    string[] splitedLine = line.Split(' ');

                    for (int j = 0; j < n; j++)
                    {
                        a[i, j] = int.Parse(splitedLine[j]);
                    }
                }

                graph = new GraphNode(a);
            }
        }

        public void Show()
        {
            for (int i = 0; i < graph.Size; i++, Console.WriteLine())
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    Console.Write(graph[i, j] + " ");
                }
            }
        }

        public void Bfs(int vertex)
        {
            graph.Reset();
            graph.Bfs(vertex);
            Console.WriteLine();
        }

        public void Dfs(int vertex)
        {
            graph.Reset();
            graph.Dfs(vertex);
            Console.WriteLine();
        }

        public void Dijkstra(int vertex)
        {
            graph.Reset();

            int[] p;
            long[] d = graph.Dijkstra(vertex, out p);

            Console.WriteLine("Длина кратчайшего пути от вершины {0} до вершины", vertex + 1);
            for (int i = 0; i < graph.Size; i++, Console.WriteLine())
            {
                if (i != vertex)
                {
                    Console.Write("{0} равна {1}, путь ", i + 1, d[i]);

                    if (d[i] != int.MaxValue)
                    {
                        Stack<int> items = new Stack<int>();
                        graph.DijkstraWayBack(vertex, i, p, ref items);

                        while (items.Count != 0)
                        {
                            Console.Write((items.Pop() + 1).ToString() + " ");
                        }
                    }
                }
            }
        }

        public void Floyd()
        {
            int[,] p;
            long[,] a = graph.Floyd(out p);

            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    if (i != j)
                    {
                        if (a[i, j] == int.MaxValue)
                        {
                            Console.WriteLine("Пути из вершины {0} в вершину {1} не существует", i, j);
                        }
                        else
                        {
                            Console.Write("Кратчайший путь от вершины {0} до вершины {1} " +
                                "равен {2}, путь ", i, j, a[i, j]);
                            Queue<int> items = new Queue<int>();
                            items.Enqueue(i);
                            graph.FloydWayBack(i, j, p, ref items);
                            items.Enqueue(j);

                            while (items.Count != 0)
                            {
                                Console.WriteLine(items.Dequeue().ToString() + " ");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        public void SearchEuler()
        {
            int[,] a = new int[graph.Size, graph.Size];

            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    a[i, j] = graph[i, j];
                }
            }
            
            Stack<int> c = new Stack<int>();
            graph.SearchEuler(0, ref a, ref c);

            while (c.Count != 0)
            {
                Console.Write("{0} ", c.Pop() + 1);
            }
            Console.WriteLine();
        }

        public void SearchHamilton()
        {
            graph.Reset();
            int[] St = new int[graph.Size + 1];
            St[0] = 0;
            graph[0] = true;
            graph.SearchHamilton(1, ref St);
        }
    }
}
