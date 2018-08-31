using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Node
    {
        public int item;
        public Node left;
        public Node right;
        public void display()
        {
            Console.Write("[");
            Console.Write(item);
            Console.Write("]");
        }




        
2
3
4
5
6
7
8
9
10
11
12
13
14
15
16
17
18
19
20
21
22
23
24
25
26

    class Tree
    {
        public Node root;
        public Tree()
        {
            root = null;
        }
        public Node ReturnRoot()
        {
            return root;
        }
        public void Insert(int id)
        {
            Node newNode = new Node();
            newNode.item = id;
            if (root == null)
                root = newNode;
            else
            {
                Node current = root;
                Node parent;
                while (true)
                {
                    parent = current;
                    if (id < current.item)
                    {
                        current = current.left;
                        if (current == null)
                        {
                            parent.left = newNode;
                            return;
                        }
                    }
                    else
                    {
                        current = current.right;
                        if (current == null)
                        {
                            parent.right = newNode;
                            return;
                        }
                    }
                }
            }
        }
        public void Preorder(Node Root)
        {
            if (Root != null)
            {
                Console.Write(Root.item + " ");
                Preorder(Root.left);
                Preorder(Root.right);
            }
        }
        public void Inorder(Node Root)
        {
            if (Root != null)
            {
                Inorder(Root.left);
                Console.Write(Root.item + " ");
                Inorder(Root.right);
            }
        }
        public void Postorder(Node Root)
        {
            if (Root != null)
            {
                Postorder(Root.left);
                Postorder(Root.right);
                Console.Write(Root.item + " ");
            }
        }
        public object SearchElement_Rec(int element, Node root)

        {
            Node current = root;
            if (root == null)

                return "Not found";

            if (Convert.ToInt32(element) == Convert.ToInt32(current.item))

                return element;

            if (Convert.ToInt32(element) < Convert.ToInt32(current.item))

                return this.SearchElement_Rec(element, current.left);

            else

                return this.SearchElement_Rec(element, current.right);

        }
            public void Delete(int key)
            {
                Node current = root;
                Node parent = root;
                bool isLeftChild = true;
                while (current.value != key)
                {
                    parent = current;
                    if (key < current.Data)
                    {
                        isLeftChild = true;
                        current = current.Right;
else {
                            isLeftChild = false;
                            current = current.Right;
                        }
                        if (current == null)
                            return false;
                    }
                    if ((current.Left == null) & (current.Right == null))
                        if (current == root)
                            root == null;
                        else if (isLeftChild)
                            parent.Left = null;
                        else
                            parent.Right = null;
                }
                // the rest of the class goes here
            }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Tree BST = new Tree();
            BST.Insert(30);
            BST.Insert(35);
            BST.Insert(57);
            BST.Insert(15);
            BST.Insert(63);
            BST.Insert(49);
            BST.Insert(89);
            BST.Insert(77);
            BST.Insert(67);
            BST.Insert(98);
            BST.Insert(91);
            Console.WriteLine("Inorder Traversal : ");
            BST.Inorder(BST.ReturnRoot());
            Console.WriteLine(" ");
            Console.WriteLine();
            Console.WriteLine("Preorder Traversal : ");
            BST.Preorder(BST.ReturnRoot());
            Console.WriteLine(" ");
            Console.WriteLine();
            Console.WriteLine("Postorder Traversal : ");
            BST.Postorder(BST.ReturnRoot());
            Console.WriteLine(" ");

            Console.WriteLine("HERE WE GO");
            Object Dsa = BST.SearchElement_Rec(15, BST.root);
            Console.WriteLine(Convert.ToInt32(Dsa));

        }
    }
}
