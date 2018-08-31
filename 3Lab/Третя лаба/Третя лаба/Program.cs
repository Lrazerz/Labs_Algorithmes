using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Третя_лаба
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 0;
            double b = 0;
            double eps = 0.0001;
            double result = 0;
            double Value_minus = FindMinPoint();
            double Value_plus  = FindMaxPoint();
            if(Value_minus - Value_plus > 0)
            {
                b = Value_minus;
                a = Value_plus;
            }
            else
            {
                b = Value_plus;
                a = Value_minus;
            }
            result = Bisection(a, b,eps);
            Console.WriteLine("{0:F4}",result);
        }
        static double FindMinPoint()
        {
            double z = 0;
            for (double i = -0.5; i < 0.5; i += 0.1)
            {
                if (f(i) < 0)
                {
                    z = i;
                    break;
                }
            }
            if(z != 0)
            {
                return z;
            }
            else
            {
                throw new Exception("Немає значень");
            }

        }
        static double FindMaxPoint()
        {
            double z = 0;
            for (double i = -0.5; i < 0.5; i += 0.1)
            {
                if (f(i) > 0)
                {
                    z = i;
                    break;
                }
            }
            if (z != 0)
            {
                return z;
            }
            else
            {
                throw new Exception("Немає значень");
            }
        }
        static double f(double x)
        {
            return Math.Pow(x, 2) - Math.Sin(5 * x);
        }
        static double Bisection(double a,double b,double eps)
        {
            double x;
            do
            {
                x = (b + a) / 2;
                if (f(a) * f(x) < 0)
                {
                    b = x;
                }
                else
                {
                    a = x;
                }
            } while (!((b-a)<eps)|f(x)==0);
            return x;
        }
    }
}
