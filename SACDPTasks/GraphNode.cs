using System;
using System.Collections;
using System.Collections.Generic;

namespace SACDPTasks
{
    class GraphNode
    {
        private int[,] adjacencyMatrix;
        private bool[] used;

        public int this[int i, int j]
        {
            get { return adjacencyMatrix[i, j]; }
            set { adjacencyMatrix[i, j] = value; }
        }

        public bool this[int i]
        {
            get { return used[i]; }
            set { used[i] = value; }
        }

        public GraphNode() { }

        public GraphNode(int[,] matrix)
        {
            adjacencyMatrix = matrix;
            used = new bool[matrix.GetLength(0)];
        }

        public int Size
        {
            get { return adjacencyMatrix.GetLength(0); }
        }

        public void Reset()
        {
            for (int i = 0; i < Size; i++)
            {
                used[i] = false;
            }
        }

        public void Add()
        {
            int[,] newAdjacencyMatrix = new int[Size + 1, Size + 1];
            bool[] newUsed = new bool[Size + 1];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    newAdjacencyMatrix[i, j] = adjacencyMatrix[i, j];
                }
            }

            adjacencyMatrix = newAdjacencyMatrix;
            used = newUsed;
        }

        public void AddArc(int a, int b)
        {
            adjacencyMatrix[a - 1, b - 1] = 1;
        }

        public void AddArc(int a, int b, int weight)
        {
            adjacencyMatrix[a - 1, b - 1] = weight;
        }

        public void AddEdge(int a, int b)
        {
            adjacencyMatrix[a - 1, b - 1] = 1;
            adjacencyMatrix[b - 1, a - 1] = 1;
        }

        public void AddEdge(int a, int b, int weight)
        {
            adjacencyMatrix[a - 1, b - 1] = weight;
            adjacencyMatrix[b - 1, a - 1] = weight;
        }

        public void Bfs(int vertex)
        {
            Queue<int> queue = new Queue<int>();

            used[vertex] = true;
            queue.Enqueue(vertex);

            while (queue.Count != 0)
            {
                vertex = queue.Peek();
                queue.Dequeue();
                Console.Write((vertex + 1).ToString() + " ");

                for (int i = 0; i < Size; i++)
                {
                    if (Convert.ToBoolean(adjacencyMatrix[vertex, i]))
                    {
                        if (!used[i])
                        {
                            used[i] = true;
                            queue.Enqueue(i);
                        }
                    }
                }
            }
        }

        public void Dfs(int vertex)
        {
            Console.Write((vertex + 1).ToString() + " ");
            used[vertex] = true;

            for (int i = 0; i < Size; i++)
            {
                if (!used[i] && adjacencyMatrix[vertex, i] != 0)
                {
                    Dfs(i);
                }
            }
        }

        public void Dfs(int a, ArrayList path)
        {
            used[a] = true;
            path.Add(a + 1);

            for (int i = 0; i < Size; i++)
            {
                if (!used[i] && adjacencyMatrix[a, i] != 0)
                {
                    Dfs(i, path);
                }
            }
        }

        public long[] Dijkstra(int vertex, out int[] p)
        {
            used[vertex] = true;
            int[,] c = new int[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (adjacencyMatrix[i, j] == 0 || i == j)
                    {
                        c[i, j] = int.MaxValue;
                    }
                    else
                    {
                        c[i, j] = adjacencyMatrix[i, j];
                    }
                }
            }

            long[] d = new long[Size];
            p = new int[Size];

            for (int i = 0; i < Size; i++)
            {
                if (vertex != i)
                {
                    d[i] = c[vertex, i];
                    p[i] = vertex;
                }
            }

            for (int i = 0; i < Size - 1; i++)
            {
                long min = int.MaxValue;
                int w = 0;

                for (int j = 0; j < Size; j++)
                {
                    if (!used[j] && min > d[j])
                    {
                        min = d[j];
                        w = j;
                    }
                }

                used[w] = true;

                for (int j = 0; j < Size; j++)
                {
                    long distance = d[w] + c[w, j];
                    if (!used[j] && d[j] > distance)
                    {
                        d[j] = distance;
                        p[j] = w;
                    }
                }

            }

            return d;
        }

        public void DijkstraWayBack(int a, int b, int[] p, ref Stack<int> items)
        {
            items.Push(b);

            if (a == p[b])
            {
                items.Push(a);
            }
            else
            {
                DijkstraWayBack(a, p[b], p, ref items);
            }
        }

        public long[,] Floyd(out int[,] p)
        {
            long[,] a = new long[Size, Size];
            p = new int[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i == j)
                    {
                        a[i, j] = 0;
                    }
                    else
                    {
                        if (adjacencyMatrix[i, j] == 0)
                        {
                            a[i, j] = int.MaxValue;
                        }
                        else
                        {
                            a[i, j] = adjacencyMatrix[i, j];
                        }
                    }

                    p[i, j] = -1;
                }
            }

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    for (int k = 0; k < Size; k++)
                    {
                        long distance = a[j, i] + a[i, k];

                        if (a[j, k] > distance)
                        {
                            a[j, k] = distance;
                            p[j, k] = i;
                        }
                    }
                }
            }

            return a;
        }

        public void FloydWayBack(int a, int b, int[,] p, ref Queue<int> items)
        {
            int k = p[a, b];

            if (k != -1)
            {
                FloydWayBack(a, k, p, ref items);
                items.Enqueue(k);
                FloydWayBack(k, b, p, ref items);
            }
        }

        public void SearchEuler(int v, ref int[,] a, ref Stack<int> c)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                if (a[v, i] != 0)
                {
                    a[v, i] = 0;
                    a[i, v] = 0;
                    SearchEuler(i, ref a, ref c);
                }
            }
            c.Push(v);
        }

        public void SearchHamilton(int k, ref int[] St)
        {
            int v = St[k - 1];
            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                if (adjacencyMatrix[v, i] != 0)
                {
                    if (k == adjacencyMatrix.GetLength(0) && i == 0)
                    {
                        St[k] = i;
                        foreach (int item in St)
                        {
                            Console.Write("{0} ", item + 1);
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        if (!used[i])
                        {
                            St[k] = i;
                            used[i] = true;
                            SearchHamilton(k + 1, ref St);
                            used[i] = false;
                        }
                    }
                }
            }
        }
    }
}
