using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    { 
        static void Main(string[] args)
        {
            (int[] A, int[] B) = EnterArray();
            Display(A, "'A'");
            Display(B, "'B'");
            LinFinder(A, B);
            //Array.Sort(A);
            //Array.Sort(B);
            QuickSort(A, 0, A.Length - 1);
            QuickSort(B, 0, B.Length - 1);
            Display(A, "'A', отсортированный");
            Display(B, "'B', отсортированный");
            int[] D = A.Distinct().ToArray();
            Display(D, "'A' без повторений");
            int index = 0;
            List<int> indexs = new List<int>();
            for (int j = 0; j < D.Length; j++)
            {
                index = BinSearch(B, D[j]);
                if (index != -1 && index != B.Length - 1)
                {
                    indexs = Method(B, index);
                    Console.WriteLine("\nИндексы {0} :", D[j]);
                    foreach (int l in indexs)
                    {
                        Console.Write(l + " ");
                    }
                }
            }

            Console.WriteLine();
        }
        static void LinFinder(int[] A, int[] B)
        {
            bool trent = true;
            int c = 0;
            int[] C = new int[B.Length / 2];
            List<int> Indexxx = new List<int>();
            for (int i = 0; i < A.Length; i++)// ел-ти масиву A
            {
                for (int j = 0; j < B.Length; j++)// ел-ти масиву B
                {
                    if (A[i] == B[j])// якщо і-тий ел. першого масиву = j-му другого
                    {
                        for (int m = j + 1; m < B.Length; m++)//найшли один перевіряємо чи немає другого такого ел-ту
                        {
                            if (A[i] == B[m] && Checker(A[i], C, ref trent))//от і 2-й елемент (в чекері пер. чи не було вже такого ел-ту
                            {
                                Indexxx.Add(j);
                                Indexxx.Add(m);
                                C[c] = A[i];
                                c++; //к-кість елементів в масиві спільних ел-тів
                                for (int nm = m + 1; nm < B.Length; nm++)
                                {
                                    if (A[i] == B[nm])
                                    {
                                        Indexxx.Add(nm);
                                    }
                                }
                                Indexxx.Add(-1);
                            }
                        }
                    }
                }
            }
            int[] Indexx = Indexxx.ToArray();
            Display(C, "с эл-ми массива A, которые встр. 2+ раз в B");
            Console.WriteLine("\n\n");
            Console.WriteLine("\n*************************Индексы элементов*************************");
            int inde = 0;
            for (int p = 0; p < c; p++)
            {
                Console.WriteLine($"\nИндексы элемента {C[p]}: ");
                do
                {
                    Console.Write(Indexx[inde] + "  ");
                    inde++;
                } while (Indexx[inde] != -1);
                inde++;
            }
        }
        static bool Checker(int a, int[] C, ref bool trent)
        {
            for (int i = 0; i < C.Length; i++)
            {
                if (a == 0)
                {
                    if (trent)
                    {
                        trent = false;
                        return true;
                    }

                }
                if (a == C[i])
                {
                    return false;
                }
            }
            return true;
        }
        static int BinSearch(int[] a, int key)// Запускаем бинарный поиск
        {
            int m = 0;
            int l = -1;                  // l, r — левая и правая границы
            int r = a.Length;
            while (l < r - 1)
            {
                m = (l + r) / 2;           // m — середина области поиска
                if (a[m] < key)
                    l = m;
                else
                    r = m;
            }                      // 
            if (a[r] == key && r < a.Length - 1)
            {
                if (a[r] == a[r + 1])
                    return r;
                else
                    return -1;
            }
            else
                return -1;
        }
        static List<int> Method(int[] B, int index)
        {
            List<int> numbs = new List<int>();
            numbs.Add(index);
            numbs.Add(index + 1);
            for (int i = index + 2; i < B.Length; i++)
            {
                if (B[index] == B[i])
                    numbs.Add(i);
                else
                    break;
            }
            return numbs;
        }
        static void Display(int[] A, string unique)
        {
            Console.WriteLine("\n\n*******************************************************Массив {0}*******************************************************", unique);
            for (int i = 0; i < A.Length; i++)
            {
                Console.Write(A[i] + " ");
            }
        }
        static (int[], int[]) EnterArray()
        {
            int n;
            int m;
            Console.WriteLine("Введите кол-во эл-тов первого массива");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите кол-во эл-тов второго массива");
            m = Convert.ToInt32(Console.ReadLine());
            int[] A = new int[n];
            int[] B = new int[m];
            //Console.WriteLine("Введіть елементи массиву А");
            //for (int i = 0; i < A.Length; i++)
            //{
            //    A[i] = Convert.ToInt32(Console.ReadLine());
            //}
            //Console.WriteLine("Введіть елементи массиву B");
            //for (int i = 0; i < B.Length; i++)
            //{
            //    B[i] = Convert.ToInt32(Console.ReadLine());
            //}
            Random rand = new Random();
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = rand.Next(0, 50);
            }
            for (int j = 0; j < B.Length; j++)
            {
                B[j] = rand.Next(0, 50);
            }
            return (A, B);
        }
        static int[] QuickSort(int[] a, int start, int end)
        {
            if (start < end)
            {
                int q = Partition(a, start, end);// q - по якому ділимо
                a = QuickSort(a, start, q);// лівий массив
                a = QuickSort(a, q + 1, end);// правий массив
            }
            return a;
        }
        static int Partition(int[] a, int start, int end)
        {
            int x = a[start];  //фіксуємо перший елемент
            int i = start - 1;
            int j = end + 1;
            while (true)
            {
                do
                {
                    j--;//ел-ти від кінця 
                }
                while (a[j] > x);// беремо ел. від кінця не більший за фікс
                do
                {
                    i++;// ел-ти від початку не менше за фіксований
                }
                while (a[i] < x);
                if (i < j)//якщо зліва не дошли до ел-тів справа
                {
                    int tmp = a[i];
                    a[i] = a[j];
                    a[j] = tmp;
                }
                else
                {
                    return j;
                }
            }
        }
        //*****************************************************
        //static void LinFinderforCtrl(int A, int[] B)
        //{
        //    List<int> numbs = new List<int>();
        //    for (int i = 0; i < B.Length; i++)
        //    {
        //        if (A == B[i])
        //            numbs.Add(i);
        //    }
        //    if (numbs.Count != 0)
        //    {
        //        Console.WriteLine("\nИндексы элемента {0}: ", A);
        //        foreach (int k in numbs)
        //        {
        //            Console.Write(k + " ");
        //        }
        //    }
        //}
        //static int BinSearchforCtrl(int[] a, int key)
        //{
        //    int m = 0;
        //    int l = -1;                  // l, r — левая и правая границы
        //    int r = a.Length;
        //    while (l < r - 1)
        //    {            // Запускаем цикл
        //        m = (l + r) / 2;           // m — середина области поиска
        //        if (a[m] < key)
        //            l = m;
        //        else
        //            r = m;
        //    }                      // Сужение границ
        //    if (a[r] == key)
        //    {
        //        return r;
        //    }
        //    else
        //        return -1;
        //}
    }
}
