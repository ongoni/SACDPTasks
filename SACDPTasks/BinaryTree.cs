using System;
using System.Collections;
using System.IO;

namespace SACDPTasks
{
    class BinaryTree
    {
        BinaryTreeNode tree;

        public object Inf
        {
            set { tree.inf = value; }
            get { return tree.inf; }
        }

        public int Counter
        {
            get { return tree.counter; }

        }

        public BinaryTree()
        {
            tree = null;
        }

        private BinaryTree(BinaryTreeNode r)
        {
            tree = r;
        }

        public BinaryTree ReadFromFile(string path)
        {
            BinaryTree tree = new BinaryTree();

            using (StreamReader fileIn = new StreamReader(path))
            {
                string line = fileIn.ReadToEnd();
                string[] data = line.Split(' ');

                foreach (string item in data)
                {
                    tree.Add(int.Parse(item));
                }
            }

            return tree; 
        }

        public void Add(object nodeInf)
        {
            BinaryTreeNode.Add(ref tree, nodeInf);
        }

        public void Preorder()
        {
            BinaryTreeNode.Preorder(tree);
        }

        public void Inorder()
        {
            BinaryTreeNode.Inorder(tree);
        }

        public void Postorder()
        {
            BinaryTreeNode.Postorder(tree);
        }

        public BinaryTree Search(object key)
        {
            BinaryTreeNode r;
            BinaryTreeNode.Search(tree, key, out r);
            BinaryTree t = new BinaryTree(r);
            return t;
        }

        public void SearchToRoot(object key)
        {
            BinaryTreeNode.SearchToRoot(ref tree, key);
        }

        public void InsertToRoot(object item)
        {
            BinaryTreeNode.InsertToRoot(ref tree, item);
        }

        public void Balancer()
        {
            BinaryTreeNode.Balancer(ref tree);
        }

        public void Delete(object key)
        {
            BinaryTreeNode.Delete(ref tree, key);
        }

        public int ProductOfNegativeNodes()
        {
            int res = 1;
            BinaryTreeNode.FindProductOfNegative(tree, ref res);
            return res;
        }

        private ArrayList GetNodeList()
        {
            ArrayList nodes = new ArrayList();
            BinaryTreeNode.GetNodeList(tree, nodes);
            return nodes;
        }

        public void FindHeightForEach()
        {
            foreach (BinaryTreeNode node in GetNodeList())
            {
                Console.WriteLine("Height of " + node.inf + " is " + 
                    BinaryTreeNode.FindHeight(node));
            }
        }
    }
}
