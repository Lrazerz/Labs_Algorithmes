using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp53
{
    class Program
    {
        static double Original(double x) //повертаємо значення самої ф-ції
        {
            return Math.Sin(Math.Cos(x));
        }
        static int NearestPts(double x,double[,] Data)//шукаємо найближчі точки
        {
            int num = 1;
            for (int i = 2; i < Data.GetLength(0) - 1; i++)
            {
                if (Data[i, 0] > x)
                {
                    if (Math.Abs(x - Data[i - 2, 0]) < (Math.Abs(x - Data[i + 1, 0])))
                    {
                        num = i - 1;
                    }
                    else
                    {
                        num = i;
                    }
                    break;
                }
            }
            return num;
        }
        static double LagrangePol(double[,] Data, double x)//повертаємо значення ф-ції використовуючи квадратичну апроксимацію
        {
            int number = NearestPts(x, Data);//шукаємо найближчих сусідів
            double[] Data_x_picked = { Data[number -1, 0], Data[number, 0], Data[number + 1, 0] };//створюємо массив значень аргументів сусідів
            double[] Data_y_picked = { Data[number - 1, 1], Data[number, 1], Data[number + 1, 1] };//створюємо массив значень функції сусідів
            double LagrangePol = 0;
            for (int i = 0; i < 3; i++)
            {
                double Polynom = 1;
                for (int j = 0; j < 3; j++)
                {
                    if (j != i)
                    {
                        Polynom *= (x - Data_x_picked[j]) / (Data_x_picked[i] - Data_x_picked[j]);//шукаємо кожен одночлен за ф-лою
                    }
                }
                LagrangePol += Polynom * Data_y_picked[i];//домножуємо на значення ф-ї і додаємо до многочлену
            }

            return LagrangePol;
        }
        static double LagrangePolAll(double[,] Data, double x)//повертаємо значення ф-ції використовуючи квадратичну аппроксимацію за всіма точками
        {
            double LagrangePol = 0;

            for (int i = 0; i < 15; i++)
            {
                double Polynom = 1;
                for (int j = 0; j < 15; j++)
                {
                    if (j != i)
                    {
                        Polynom *= (x - Data[j, 0]) / (Data[i, 0] - Data[j, 0]);
                    }
                }
                LagrangePol += Polynom * Data[i, 1];
            }

            return LagrangePol;

        }
        static double OriginalFirstDerevative(double x)//повертаємо значення першої похідної
        {
            return (-(Math.Cos(Math.Cos(x)) * Math.Sin(x)));
        }
        static double OriginalSecondDerevative(double x)//повертаємо значення другої похідної
        {
            return -(Math.Sin(x) * Math.Sin(Math.Cos(x)) * Math.Sin(x) + Math.Cos(Math.Cos(x)) * Math.Cos(x));
        }
        static double FirstDerevative(double x, double[,] Data)//повертаємо першу похідну використовуючи формули квадратичної аппроксимації
        {
            return (LagrangePol(Data, (x + 0.1)) - LagrangePol(Data, x - 0.1)) / 0.2;

        }
        static double SecondDerevative(double x, double[,] Data)//повертаємо другу похідну використовуючи формули квадратичної аппроксимації
        {
            return (LagrangePol(Data, (x + 0.1)) + LagrangePol(Data, (x - 0.1)) - 2 * LagrangePol(Data, x)) / Math.Pow(0.1, 2);

        }
        static void Main(string[] args)
        {
            double[,] Data = { { 0.88, 0.59490 }, { 1.68, -0.10877 }, { 2.3, -0.61806 }, { 2.8, -0.80887 }, { 3.5, -0.80546 }, { 4.11, -0.53678 }, { 4.78, 0.06751 }, { 5, 0.27987 }, { 6.5, 0.82859 }, { 7.2, 0.57152 }, { 8.9, -0.76138 }, { 9.3, -0.83725 }, { 9.33, -0.83904 }, { 9.89, -0.77941 }, { 10.2, -0.65506 } };
            double result, result2, result3;
            Console.WriteLine("Таблиця Значень \n");
            Console.WriteLine(" \n           {0} {1} {2}", "Own value", "Quad Approx.", "Approx all points");
            for (double i = 1.0; i <= 10.01; i += 0.1)
            {
                result = Original(i);
                result2 = LagrangePol(Data, i);
                result3 = LagrangePolAll(Data, i);
                Console.WriteLine("\nf({0,4:F1}) = {1,-15:F6} {2,-15:F6} {3,9:F6}", i, result, result2, result3);
            }
            Console.WriteLine(" \n            {0} {1} ", "Own derevative", "Quad approx derevative");
            for (double i = 1.0; i < 10; i += 0.1)
            {
                result = OriginalFirstDerevative(i);
                result2 = FirstDerevative(i, Data);
                Console.WriteLine("\nf'({0,4:F1}) = {1,-15:F6} {2,-15:F6} ", i, result, result2);
            }
            Console.WriteLine(" \n             {0} {1,34} ", "Own second derevative", "Quad approx second derevative");
            for (double i = 1.0; i < 10; i += 0.1)
            {
                result = OriginalSecondDerevative(i);
                result2 = SecondDerevative(i, Data);
                Console.WriteLine("\nf''({0,4:F1}) = {1,-15:F6} {2,20:F6} ", i, result, result2);
            }
        }
    }
}
