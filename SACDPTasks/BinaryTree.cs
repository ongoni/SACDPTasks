using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private ArrayList GetNodesList()
        {
            ArrayList nodes = new ArrayList();
            BinaryTreeNode.GetNodesList(tree, nodes);
            return nodes;
        }
    }
}
