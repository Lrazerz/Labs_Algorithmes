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
            int n = 20;
            int[] c;
            int[] b;
            int[] a = new int[n];
            //Console.WriteLine("Введiть к-кiсть елементiв массиву");
            //n = Convert.ToInt32(Console.ReadLine());
            //int[] a = new int[n];
            //Console.WriteLine("Введiть елементи");
            //for (int i = 0; i < a.Length; i++)
            //{
            //    a[i] = Convert.ToInt32(Console.ReadLine());
            //}
            Random rand = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = rand.Next(1, 17);
            }
            

            Console.WriteLine("\nВведений массив ");
            for (int k = 0; k < a.Length; k++)
            {
                Console.Write(a[k] + " ");
            }
            b = a.Distinct().ToArray();
            Console.WriteLine("\nМасив без дублiкатiв ");
            for (int k = 0; k < b.Length; k++)
            {
                Console.Write(b[k] + " ");
            }
            c = b.ToArray();
            Console.WriteLine("\nКвиксорт ");
            Quicksort(c,0,c.Length-1);

            Console.WriteLine("\nШелла ");
            c = shellSort(b);
            for (int k = 0; k < b.Length; k++)
            {
                Console.Write(b[k] + " ");
            }
            b = Zlittya(b);
            Console.WriteLine("\nСортування злиттям");
            for (int k = 0; k < b.Length; k++)
            {
                Console.Write(b[k] + " ");
            }
            Console.WriteLine();
        }

        public static void Quicksort(int[] numbers, int left, int right)
        {
            int pivot; // разрешающий элемент
            int l_hold = left; //левая граница
            int r_hold = right; // правая граница
            pivot = numbers[left];
            while (left < right) // пока границы не сомкнутся
            {
                while ((numbers[right] >= pivot) && (left < right))
                    right--; // сдвигаем правую границу пока элемент [right] больше [pivot]
                if (left != right) // если границы не сомкнулись
                {
                    numbers[left] = numbers[right]; // перемещаем элемент [right] на место разрешающего
                    left++; // сдвигаем левую границу вправо
                }
                while ((numbers[left] <= pivot) && (left < right))
                    left++; // сдвигаем левую границу пока элемент [left] меньше [pivot]
                if (left != right) // если границы не сомкнулись
                {
                    numbers[right] = numbers[left]; // перемещаем элемент [left] на место [right]
                    right--; // сдвигаем правую границу вправо
                }
            }
            numbers[left] = pivot; // ставим разрешающий элемент на место
            pivot = left;
            left = l_hold;
            right = r_hold;
            Output(numbers);
            if (left < pivot) // Рекурсивно вызываем сортировку для левой и правой части массива
                Quicksort(numbers, left, pivot - 1);
            if (right > pivot)
                Quicksort(numbers, pivot + 1, right);
        }
        public static int[] shellSort(int[] arr)
        {
            int j;

            int step = arr.Length / 2;
            while (step > 0)
            {
                for (int i = 0; i < (arr.Length - step); i++)
                {
                    j = i;
                    while ((j >= 0) && (arr[j] > arr[j + step]))
                    {
                        int tmp = arr[j];
                        arr[j] = arr[j + step];
                        arr[j + step] = tmp;
                        Output(arr);
                        j -= step;
                    }
                }
                step = step / 2;
            }
            return arr;
        }
        public static int[] Zlittya(int[] a)
        {
            if (a.Length == 1)
                return a;
            int point = a.Length / 2;
            return merge(Zlittya(a.Take(point).ToArray()), Zlittya(a.Skip(point).ToArray()));
        }
        public static int[] merge(int[] a1, int[] a2)
        {
            int a = 0, b = 0;
            int[] merged = new int[a1.Length + a2.Length];
            for (int i = 0; i < a1.Length + a2.Length; i++)
            {
                if (b < a2.Length && a < a1.Length)
                    if (a1[a] > a2[b] && b < a2.Length)
                    {
                        merged[i] = a2[b++];
                    }
                    else {
                        merged[i] = a1[a++];
                    }
                else
                    if (b < a2.Length)
                    merged[i] = a2[b++];
                else
                    merged[i] = a1[a++];
            }
            return merged;
        }
        static void Output(int[] a)
        {
            for (int i = 0; i < a.Length; i++)

                Console.Write("{0,3} ", a[i]);
            Console.WriteLine();
        }
    }
}