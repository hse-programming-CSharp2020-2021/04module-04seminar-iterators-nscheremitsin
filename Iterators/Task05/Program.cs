using System;
using System.Collections;

/* На вход подается число N.
 * Нужно создать коллекцию из N элементов последовательного ряда натуральных чисел, возведенных в 10 степень, 
 * и вывести ее на экран ТРИЖДЫ. Инвертировать порядок элементов при каждом последующем выводе.
 * Элементы коллекции разделять пробелом. 
 * Очередной вывод коллекции разделять переходом на новую строку.
 * Не хранить всю коллекцию в памяти.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield и foreach. Не вызывать метод Reset() в классе Program.
 * 
 * Пример входных данных:
 * 2
 * 
 * Пример выходных данных:
 * 1 1024
 * 1024 1
 * 1 1024
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
 * В других ситуациях выбрасывайте 
*/
namespace Task05
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!int.TryParse(Console.ReadLine(), out int value) || value <= 0)
                {
                    throw new ArgumentException();
                }
                MyDigits myDigits = new MyDigits();
                IEnumerator enumerator = myDigits.MyEnumerator(value);

                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
            catch (ArithmeticException)
            {
                Console.WriteLine("ooops");
            }
        }

        static void IterateThroughEnumeratorWithoutUsingForeach(IEnumerator enumerator)
        {
            while (true)
            {
                Console.Write(enumerator.Current);

                if (!enumerator.MoveNext())
                {
                    return;
                }
                Console.Write(" ");
            }
        }
    }


    class MyDigits : IEnumerator // НЕ МЕНЯТЬ ЭТУ СТРОКУ
    {
        private int value;
        private int current = 1;
        private bool isAscending = true;


        public IEnumerator MyEnumerator(int value)
        {
            this.value = value;
            return this;
        }


        public bool MoveNext()
        {
            if (isAscending && current < value)
            {
                current++;
                return true;
            }
            else if (!isAscending && current > 1)
            {
                current--;
                return true;
            }
            Reset();
            return false;
        }


        public object Current { get => (int)Math.Pow(current, 10); }


        public void Reset()
        {
            isAscending = !isAscending;

            if (isAscending)
            {
                current = 1;
            }
            else
            {
                current = value;
            }
        }
    }
}
