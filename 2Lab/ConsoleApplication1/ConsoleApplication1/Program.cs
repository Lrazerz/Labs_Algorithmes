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
            try//тут ми піймаємо виключення коли для пошуку результату заданої точності треба більше 500 ітерацій
            {

                double[] massiv = new double[500]; //ініціалізуємо массив, де будуть зберігатися результати кожної ітерації
                massiv[0] = -99999999999; //щоб похибка для першої ітерації не була менше епсілона ( 1/15 * ( massiv[i]-massiv[i-1] ) )
                int i = 0;
                double x, a, b, h, s;
                int n;
                a = 0.0; //ліва межа інтегрування
                b = 1.0; //права межа інтегрування
                n = 10; //розбиваємо на 10 відрізків, бо заданий крок 0.1
                double eps = 0.0001;//точність
                do
                {

                    i++; // ставимо позицію в масиві +1, щоб записати результат в наступний елемент массиву
                    h = (b - a) / n; // шукаємо крок
                    x = a + h;
                    s = 4 * Function(x); //Присвоуємо s = 4 f(a+h) і заодно позбавляємося від результатів минулої ітерації циклу
                    while (x < b - h) // за ф-лою будемо шукати суму доданків
                    {
                        s = s +  Function(x); // шукаємо всі доданки до f(b-2h) і множ на 2
                        x = x + h;
                        s = s +  Function(x); // шукаємо всі доданки до f(b-h) і множ на 4(перший доданок вже є в початковому s)
                    }
                    massiv[i] = h / 3 * (s + Function(a) + Function(b));
                    Console.WriteLine("Розбиваємо на {1} вiдрiзкiв, розвязок = {0}, похибка = {2:F20}", massiv[i], n, (massiv[i] - massiv[i - 1]) / 15);
                    n *= 2;
                } while ((1.0 / 15) * (massiv[i] - massiv[i - 1]) >= eps);// перевіряємо умову закінчення ітерацій за принципом рунге
                Console.WriteLine("Iнтеграл = {0}", massiv[i]);
            } catch (IndexOutOfRangeException)//якщо за 500 ітерацій не знайшли результат заданої точності
            {
                Console.WriteLine("Треба більше 500 ітерацій для знаходження результату заданої точності");
            }
            }
        static double Function(double x)// повертаємо функцію
        {
            return Math.Pow(x - Math.Pow(x, 3), 1.0 / 3);
        }
    }
}
