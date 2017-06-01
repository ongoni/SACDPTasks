using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACDPTasks
{
    class BinaryTreeNode
    {
        public object inf;
        public object Inf { get { return inf; } }
        public int counter;
        public BinaryTreeNode left;
        public BinaryTreeNode right;

        public BinaryTreeNode(object nodeInf)
        {
            inf = nodeInf;
            counter = 1;
            left = null;
            right = null;
        }

        public static void Add(ref BinaryTreeNode r, object nodeInf)
        {
            if (r == null)
            {
                r = new BinaryTreeNode(nodeInf);
            }
            else
            {
                r.counter++;
                if (((IComparable)(r.inf)).CompareTo(nodeInf) > 0)
                {
                    Add(ref r.left, nodeInf);
                }
                else
                {
                    Add(ref r.right, nodeInf);
                }
            }
        }

        public static void Preorder(BinaryTreeNode r)
        {
            if (r != null)
            {
                Console.Write("({0}, {1}) ", r.inf, r.counter);
                Preorder(r.left);
                Preorder(r.right);
            }
        }

        public static void Inorder(BinaryTreeNode r)
        {
            if (r != null)
            {
                Inorder(r.left);
                Console.Write("({0}, {1}) ", r.inf, r.counter);
                Inorder(r.right);
            }
        }

        public static void Postorder(BinaryTreeNode r)
        {
            if (r != null)
            {
                Postorder(r.left);
                Postorder(r.right);
                Console.Write("({0}, {1}) ", r.inf, r.counter);
            }
        }

        public static void Part(ref BinaryTreeNode t, int k)
        {
            int x = (t.left == null) ? 0 : t.left.counter;
            if (x > k)
            {
                Part(ref t.left, k);
                RotationRight(ref t);
            }
            if (x < k)
            {
                Part(ref t.right, k - x - 1);
                RotationLeft(ref t);
            }
        }

        public static void Balancer(ref BinaryTreeNode t)
        {
            if (t == null || t.counter == 1) return;
            Part(ref t, t.counter / 2);
            Balancer(ref t.left);
            Balancer(ref t.right);
        }

        public static void Search(BinaryTreeNode r, object key, out BinaryTreeNode item)
        {
            if (r == null)
            {
                item = null;
            }
            else
            {
                if (((IComparable)(r.inf)).CompareTo(key) == 0)
                {
                    item = r;
                }
                else
                {
                    if (((IComparable)(r.inf)).CompareTo(key) > 0)
                    {
                        Search(r.left, key, out item);
                    }
                    else
                    {
                        Search(r.right, key, out item);
                    }
                }
            }
        }

        public static void SearchToRoot(ref BinaryTreeNode r, object key)
        {
            if (r != null)
            {
                if (((IComparable)(r.inf)).CompareTo(key) == 0)
                {
                    return;
                }
                else
                {
                    if (((IComparable)(r.inf)).CompareTo(key) > 0)
                    {
                        SearchToRoot(ref r.left, key);
                        RotationRight(ref r);
                    }
                    else
                    {
                        SearchToRoot(ref r.right, key);
                        RotationLeft(ref r);
                    }
                }
            }
        }

        public static void Count(ref BinaryTreeNode r)
        {
            r.counter = 1;
            if (r.left != null) r.counter += r.left.counter;
            if (r.right != null) r.counter += r.right.counter;
        }

        public static void RotationRight(ref BinaryTreeNode t)
        {
            BinaryTreeNode x = t.left;
            t.left = x.right;
            x.right = t;

            Count(ref t);
            Count(ref x);

            t = x;
        }

        public static void RotationLeft(ref BinaryTreeNode t)
        {
            BinaryTreeNode x = t.right;
            t.right = x.left;
            x.left = t;

            Count(ref t);
            Count(ref x);

            t = x;
        }

        public static void InsertToRoot(ref BinaryTreeNode t, object item)
        {
            if (t == null)
            {
                t = new BinaryTreeNode(item);
            }
            else
            {
                t.counter++;
                if (((IComparable)(t.inf)).CompareTo(item) > 0)
                {
                    InsertToRoot(ref t.left, item);
                    RotationRight(ref t);
                }
                else
                {
                    InsertToRoot(ref t.right, item);
                    RotationLeft(ref t);
                }
            }
        }

        private static void Del(BinaryTreeNode t, ref BinaryTreeNode tr)
        {
            if (tr.right != null)
            {
                Del(t, ref tr.right);
            }
            else
            {
                t.inf = tr.inf;
                tr = tr.left;
            }
        }

        public static void Delete(ref BinaryTreeNode t, object key)
        {
            if (t == null)
            {
                throw new Exception("Данное значение в дереве отсутствует");
            }
            else
            {
                t.counter--;
                if (((IComparable)(t.inf)).CompareTo(key) > 0)
                {
                    Delete(ref t.left, key);
                }
                else
                {
                    if (((IComparable)(t.inf)).CompareTo(key) < 0)
                    {
                        Delete(ref t.right, key);
                    }
                    else
                    {
                        if (t.left == null)
                        {
                            t = t.right;
                        }
                        else
                        {
                            if (t.right == null)
                            {
                                t = t.left;
                            }
                            else
                            {
                                Del(t, ref t.left);
                            }
                        }
                    }
                }
            }
        }

        public static void GetNodesList(BinaryTreeNode r, ArrayList nodes)
        {
            if (r != null)
            {
                nodes.Add(r);
                GetNodesList(r.left, nodes);
                GetNodesList(r.right, nodes);
            }
        }
    }
}
