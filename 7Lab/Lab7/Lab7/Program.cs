using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp150
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array = new int[5, 5];//матриця суміжності
            int[] vertex_status = new int[array.GetLength(0)];// статус вершин
            Stack<int> staack = new Stack<int>();//стек 
            List<int> cycles = new List<int>();//цикли запишемо в список
            Input(array);
            Output(array);
            HereWeGo(array,vertex_status,cycles,staack);
        }
        static int[,] Input(int[,] array)
        {
            array[0,2] = 1;
            array[0,1] = 1;
            array[3,4] = 1;
            array[4,2] = 1;
            array[3,2] = 1;
            array[3,0] = 1;
            array[1,3] = 1;
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for(int j = 0; j < array.GetLength(1); j++)
                {
                    if(array[i,j] == 1)
                    array[j, i] = array[i, j];
                }
            }
            return array;
        }
        static void Output(int[,] array)
        {
            Console.WriteLine("      1     2     3     4     5     ");
            Console.WriteLine();
            for (int i = 0; i < array.GetLength(1); i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{i+1}"+ "");
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("{0,6}", array[i, j]);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void HereWeGo(int[,] array,int[] vertex_status,List<int> cycles,Stack<int> staack)
        {
            int vertex = 0;//вершина
            while (checker(vertex_status,vertex))//тут перевіряємо чи є ще вершини зі станом "0", якщо є находимо її і повертаємо
            {
                staack.Push(vertex);//Закидуємо в стек
                while (staack.Count != 0)//поки в стеці є вершини
                {
                    vertex = staack.Pop();//забираємо зі стеку вершину
                    cycles.Add(vertex);//+вершина в цикли
                    vertex_status[vertex] = 2;// ми проходимо цю вершину, присвоюємо їй стан "2"
                    bool checker = true;// чекер для сусідів вершини
                    for (int i = 0; i < array.GetLength(0); i++)
                    {
                        if (array[vertex, i] == 1)//якщо і-та вершина - сусід
                        {
                            if (vertex_status[i] == 0)//і стан 0 - тобто ми перший раз її зустрічаємо
                            {
                                checker = false;
                                vertex_status[i] = 1;
                                staack.Push(i);
                            }
                            else
                            {
                                if (vertex_status[i] == 1)
                                {
                                    checker = false;
                                }
                                else
                                {
                                    if (vertex_status[i] == 2)
                                    {
                                        cycleCreater(cycles, i);
                                    }
                                }
                            }
                        }
                    }
                    if (checker)//сюди доходимо якщо цикл знайшли
                    {
                        bool check = true;
                        while (check && cycles.Count > 1)//видаляємо із цикла списку циклів вершини
                        {
                            vertex_status[cycles[cycles.Count - 1]] = 3;
                            cycles.RemoveAt(cycles.Count - 1);
                            for (int i = array.GetLength(0) - 1; i > -1; i--)//пер від останнього елементу до першого
                            {
                                if (array[cycles[cycles.Count - 1], i] == 1)//перевіряємо чи в матриці суміжності ще є звязки від ост  вершини цикла
                                {
                                    if (vertex_status[i] == 1)//якщо вершина яка є сусідом має стан "1" то ми ще не видаляємо з циклу
                                    {
                                        check = false;
                                    }
                                }
                            }
                        }
                        if (check && cycles.Count == 1)
                        {
                            vertex_status[cycles[cycles.Count - 1]] = 3;
                            cycles.RemoveAt(cycles.Count - 1);
                        }
                    }
                }
            }
        }
        static bool checker(int[] array,int vertex)
        {
            vertex = 0;
            bool check = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 0)
                {
                    vertex = i;
                    check = true;
                    break;
                }
            }
            return check;
        }//перевіряємо чи є ще непровірені вершини, якщо находимо хоч одну, зразу її вертаємо
        static void cycleCreater(List<int> array, int search)
        {
            if (array.Count > 2 && array[array.Count - 2] != search)
            {
                string cycle = (search + 1) + "";
                for (int i = array.Count - 1; i > -1; i--)
                {
                    if (array[i] == search)
                    {
                        cycle = (array[i] + 1) + " , " + cycle;
                        break;
                    }
                    else
                    {
                        cycle = (array[i] + 1) + " , " + cycle;
                    }
                }
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine($"Знайшли цикл:  {cycle}");
            }
        }
    }
}
