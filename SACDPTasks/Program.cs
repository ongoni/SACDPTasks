using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACDPTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //BinaryTreeTasks treeTasks = new BinaryTreeTasks();
            //treeTasks.ProductOfNegativeNodes();
            //treeTasks.NodesHeight();

            GraphTasks graphTasks = new GraphTasks();
            //graphTasks.FindPathway();
            graphTasks.FindEulerPathway();
        }
    }
}
