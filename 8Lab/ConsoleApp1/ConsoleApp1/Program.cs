using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Node// вузол
    {
        public int value;
        public Node left;//лівий вузол
        public Node right;
    }
    class Tree
    {
        public Node root;
        public Tree()
        {
            root = null;
        }
        public void Insert(int numb)
        {
            Node newNode = new Node();
            newNode.value = numb;//ств вузол і присв. йому знач
            if (root == null)//при добавленні першого числа записуємо корінь дерева
                root = newNode;
            else
            {
                Node current = root;
                Node parent;
                while (true)
                {
                    parent = current;//присвоюємо значенню базовому знач поточного(спочатку корінь)
                    if (numb < current.value)
                    {
                        current = current.left;//якщо значення менше за вибраний то беремо лівий за базовий
                        if (current == null)//якщо не заповнене - запонюємо
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
        public void DeleteNode(int key)
        {
            if (key == root.value)
            {
                root = null;
            }
            if (root != null)
            {
                Node current = root;
                Node parent = root;
                bool isLeftChild = true;
                while (current.value != key)
                {
                    parent = current;
                    if (key < current.value)
                    {
                        isLeftChild = true;
                        current = current.left;
                    }
                    else
                    {
                        isLeftChild = false;
                        current = current.right;
                    }
                    if (current == null)
                        Console.WriteLine("No numb");
                }
                if (isLeftChild)//parent.left - шуканий
                {
                    if (parent.left.right == null)
                    {
                        parent.left = parent.left.left;
                    }
                    else
                    {
                        if (parent.left.right.left == null)
                        {
                            Node tmp = parent.left.left;
                            parent.left = parent.left.right;
                            parent.left.left = tmp;
                        }
                        else
                        {
                            current = parent.left.right;
                            while (current.left.left != null)//мін значення правого піддерева
                            {
                                current = current.left;
                            }
                                Node tmp = parent.left;//сейвимо
                                parent.left = current.left;//=найменшому ел-ту

                                current.left = current.left.right;//підтягуємо праве піддерево
                                parent.left.right = tmp.right;
                                parent.left.left = tmp.left;
                        }
                    }
                }
                else//parent.right = шуканий;
                {
                    if (parent.right.right == null)
                    {
                        parent.right = parent.right.left;
                    }
                    else
                    {
                        if (parent.right.right.left == null)
                        {
                            Node tmp = parent.right.left;
                            parent.right = parent.right.right;
                            parent.right.left = tmp;
                        }
                        else
                        {
                            current = parent.right.right;//ост
                            while (current.left.left != null)
                            {
                                current = current.left;
                            }
                                Node tmp = parent.right;
                                parent.right = current.left;
                                current.left = current.left.right;
                                parent.right.right = tmp.right;
                                parent.right.left = tmp.left;
                        }
                    }
                }
            }
        }
        public void Preorder(Node Root)
        {
            if (Root != null)
            {
                Console.Write(Root.value + " ");
                Preorder(Root.left);
                Preorder(Root.right);
            }
        }
        public void Inorder(Node Root)
        {
            if (Root != null)
            {
                Inorder(Root.left);
                Console.Write(Root.value + " ");
                Inorder(Root.right);
            }
        }
        public void Postorder(Node Root)
        {
            if (Root != null)
            {
                Postorder(Root.left);
                Postorder(Root.right);
                Console.Write(Root.value + " ");
            }
        }
        public int  SearchElement_Rec(int element, Node root, int counter)//ел, вузол, каунтэр
        {
            Node current = root;
            if (root == null)
                throw new Exception("node not found!");//немає вузла
            if (Convert.ToInt32(element) == Convert.ToInt32(current.value))
            {
                if (counter > 0)
                {
                    return counter;
                }
                else
                    return -1;
            }
            if (Convert.ToInt32(element) < Convert.ToInt32(current.value))
            {
                counter++;
                return SearchElement_Rec(element, current.left,counter);
            }
            else
            {
                counter++;
                return SearchElement_Rec(element, current.right,counter);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tree Mytree = new Tree();
                int[] array = new int[] {10,5,9,7,8,4,20,25,26,29,27,28,24,22,23};
                for(int i= 0; i < array.Length; i++)
                {
                    Mytree.Insert(array[i]);
                }

                Console.WriteLine("Preorder: ");
                Mytree.Preorder(Mytree.root);
                Console.WriteLine(" ");
                Console.WriteLine();
                Console.WriteLine("Inorder: ");
                Mytree.Inorder(Mytree.root);
                Console.WriteLine(" ");
                Console.WriteLine();
                Console.WriteLine("Postorder: ");
                Mytree.Postorder(Mytree.root);
                Console.WriteLine(" ");
                Console.WriteLine("HERE WE GO");
                Console.WriteLine("Enter the number");
                int numb = Convert.ToInt32(Console.ReadLine());
                int counter = 0;
                counter = Mytree.SearchElement_Rec(numb, Mytree.root,counter);
                if (counter > 0)
                {
                    Console.WriteLine($"****************************Depth is {counter}********************************");
                    Console.WriteLine();
                    Console.WriteLine("Delete???");
                    int del = Convert.ToInt32(Console.ReadLine());
                    Mytree.DeleteNode(del);
                    Console.WriteLine("Preorder: ");
                    Mytree.Preorder(Mytree.root);
                    Console.WriteLine(" ");
                    Console.WriteLine();
                    Console.WriteLine("Inorder: ");
                    Mytree.Inorder(Mytree.root);
                    Console.WriteLine(" ");
                    Console.WriteLine();
                    Console.WriteLine("Postorder: ");
                    Mytree.Postorder(Mytree.root);
                    Console.WriteLine(" ");
                    Console.WriteLine("Delete???");
                    int del2 = Convert.ToInt32(Console.ReadLine());
                    Mytree.DeleteNode(del2);
                    Console.WriteLine("Preorder: ");
                    Mytree.Preorder(Mytree.root);
                    Console.WriteLine(" ");
                    Console.WriteLine();
                    Console.WriteLine("Inorder: ");
                    Mytree.Inorder(Mytree.root);
                    Console.WriteLine(" ");
                    Console.WriteLine();
                    Console.WriteLine("Postorder: ");
                    Mytree.Postorder(Mytree.root);
                    Console.WriteLine(" ");
                }
                else if (counter == -1)
                {
                    throw new Exception("Do you want to find the root? Are you kidding me?");
                }
                else
                {
                    Console.WriteLine($"WTF,{counter}");
                }           
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }
}
