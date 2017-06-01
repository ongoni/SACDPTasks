using System;
using System.Collections;
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

        private Graph(GraphNode root)
        {
            graph = root;
        }

        public Graph ReadFromFile(string path)
        {
            GraphNode root;

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

                root = new GraphNode(a);
            }

            return new Graph(root);
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
            graph.Reset();

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

        public void FindPathway(int a, int b)
        {
            if (a > graph.Size - 1 || b > graph.Size - 1)
            {
                Console.WriteLine("Wrong vertex.");
                return;
            }

            ArrayList path = new ArrayList();
            graph.Reset();
            graph.Dfs(a - 1, path);

            if (path.Contains(b))
            {
                foreach (int item in path)
                {
                    Console.Write(item.ToString() + " ");
                    if (item == b) break;
                }
            }
            else
            {
                Console.WriteLine("There is no pathway to vertex " + (b + 1).ToString());
            }
        }

        public void FindEulerPathway(int v)
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
            graph.SearchEuler(v - 1, ref a, ref c);
            graph.Reset();

            ArrayList path = new ArrayList();
            while (c.Count > 0)
            {
                path.Add(c.Pop());
            }

            bool correct = true;
            for (int i = 0; i + 1< path.Count; i++)
            {
                if (!graph.EdgeExist((int)path[i], (int)path[i + 1]))
                {
                    correct = false;
                }
            }

            if (correct)
            {
                foreach (int vertex in path)
                {
                    Console.Write((vertex + 1).ToString() + " ");
                }
            }
            else
            {
                Console.WriteLine("There is no Euler pathway for vertex " + v.ToString());
            }
        }

        public void FindHamiltonPathway(int v)
        {
            graph.Reset();
            int[] St = new int[graph.Size + 1];
            St[0] = v - 1;
            graph[v - 1] = true;
            graph.SearchHamilton(1, ref St);

            if (!graph.HaveHamiltonCycle)
            {
                Console.WriteLine("Graph don't have Hamilton cycle that starts " +
                    "in vertex " + v.ToString());
            }
        }
    }
}
