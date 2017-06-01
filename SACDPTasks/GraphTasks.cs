using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACDPTasks
{
    class GraphTasks
    {
        public void FindPathway()
        {
            Graph graph = new Graph().ReadFromFile("../../GraphInput.txt");

            Console.Write("Enter a: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Enter b: ");
            int b = int.Parse(Console.ReadLine());

            graph.FindPathway(a, b);
            Console.WriteLine();
        }

        public void FindEulerPathway()
        {
            Graph graph = new Graph().ReadFromFile("../../EulerGraphInput.txt");

            Console.Write("Enter a: ");
            int a = int.Parse(Console.ReadLine());
            
            graph.FindEulerPathway(a);
        }

        public void FindHamiltonPathway()
        {
            Graph graph = new Graph().ReadFromFile("../../HamiltonGraphInput.txt");

            Console.Write("Enter a: ");
            int a = int.Parse(Console.ReadLine());
            
            graph.FindHamiltonPathway(a);
        }

        public void FindPairs()
        {
            Graph graph = new Graph().ReadFromFile("../../WeightedGraphInput.txt");

            Console.Write("Enter L: ");
            int l = int.Parse(Console.ReadLine());

            graph.FindPairs(l);
        }
    }
}