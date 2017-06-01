using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACDPTasks
{
    class BinaryTreeTasks
    {
        public void ProductOfNegativeNodes()
        {
            BinaryTree tree = new BinaryTree().ReadFromFile("../../BinaryTreeInput.txt");
            Console.WriteLine(tree.ProductOfNegativeNodes());
        }

        public void NodesHeight()
        {
            BinaryTree tree = new BinaryTree().ReadFromFile("../../BinaryTreeInput.txt");
            tree.FindHeightForEach();
        }
    }
}
