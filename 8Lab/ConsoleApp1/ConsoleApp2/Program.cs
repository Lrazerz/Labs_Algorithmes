using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Tree
    {
        public Tree(int number)
        {
            root = new Node(number);
        }
        private Node root;
        private class Node
        {
            public Node(int number)
            {
                knot = number;
                left = null;
                right = null;
            }
            public int knot;
            public Node left, right;
            public void Add(int number)//додає число
            {
                if (number >= this.knot)
                {
                    if (right == null)
                    {
                        right = new Node(number);
                    }
                    else
                    {
                        right.Add(number);
                    }
                }
                else
                {
                    if (left == null)
                    {
                        left = new Node(number);
                    }
                    else
                    {
                        left.Add(number);
                    }
                }
            }
            public void Printf()//прямий вивід дерева
            {
                if (this.left != null)
                {
                    this.left.Printf();
                }
                Console.Write(this.knot + " ");
                if (this.right != null)
                {
                    this.right.Printf();
                }

            }
            public Node Min()//пошук мінімума
            {
                if (this.left == null)
                {
                    return this;
                }
                else
                {
                    return left.Min();
                }
            }
            public void SearchLogic(ref int counter, int key)//пошук
            {
                if (this.knot == key)
                {
                    counter++;
                    if (this.right != null)
                    {
                        this.right.SearchLogic(ref counter, key);
                    }
                }
                else
                {
                    if (this.knot <= key)
                    {
                        if (this.right != null)
                        {
                            this.right.SearchLogic(ref counter, key);
                        }
                    }
                    else
                    {
                        if (this.left != null)
                        {
                            this.left.SearchLogic(ref counter, key);
                        }
                    }
                }

            }
            static public void Beckdown(int n)
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write(" ");
                }
            }

            public void PrintSupport(int counter, ref bool chacker, int tabe)
            {
                if (counter != 0)
                {
                    if (this.left == null)
                    {
                        Beckdown(tabe * (int)Math.Pow(2, counter));
                    }
                    else
                    {
                        left.PrintSupport(counter - 1, ref chacker, tabe);
                    }
                    if (this.right == null)
                    {
                        Beckdown(tabe * (int)Math.Pow(2, counter));
                    }
                    else
                    {
                        right.PrintSupport(counter - 1, ref chacker, tabe);
                    }
                }
                else
                {
                    Beckdown(tabe);
                    Console.Write($"{knot}");
                    Beckdown(tabe);
                    chacker = true;
                }
            }
            public void MegaPrintf()
            {
                int count = 0, tabe = 64;
                bool chacker = true;
                while (chacker)
                {
                    chacker = false;
                    this.PrintSupport(count, ref chacker, tabe);
                    Console.WriteLine("\n");
                    count++;
                    tabe /= 2;
                }
            }
            public Node Delete(int key)
            
        }
        public void SearchLogic(ref int counter, int key)
        {
            root.SearchLogic(ref counter, key);
        }
        public void Add(int number)
        {
            root.Add(number);
        }
        public void Printf()
        {
            root.Printf();
            Console.WriteLine();
            root.MegaPrintf();
            Console.WriteLine("\n\n");
        }
        public void Delete(int key)
        {
            Node tree = root.Delete(key);
            if (tree != null)
            {
                if (tree.previous == null)
                {
                    root = tree.right;
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            Tree tree = new Tree(4);
            for (int i = 0; i < 8; i += 1)
            {
                tree.Add(i);
            }
            tree.Add(0);
            tree.Add(0);
            tree.Add(0);
            tree.SearchLogic(ref counter, 0);
            tree.Printf();
            Console.WriteLine($"key is 0 number of search {counter} ");
            counter = 0;
            tree.SearchLogic(ref counter, 5);
            Console.WriteLine($"key is 5 number of search {counter} ");
            counter = 0;
            tree.SearchLogic(ref counter, 20);
            Console.WriteLine($"key is 20 number of search {counter} ");
            tree.Delete(1);
            tree.Delete(0);
            tree.Delete(0);
            Console.WriteLine("remuwe 1, 0, 0");
            tree.Printf();
            counter = 0;
            tree.SearchLogic(ref counter, 0);
            Console.WriteLine($"key is 0 number of search {counter} ");
            counter = 0;
            tree.SearchLogic(ref counter, 5);
            Console.WriteLine($"key is 5 number of search {counter} ");
            counter = 0;
            tree.SearchLogic(ref counter, 20);
            Console.WriteLine($"key is 20 number of search {counter} ");
        }
    }
}
