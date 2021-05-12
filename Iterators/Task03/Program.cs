using System;
using System.Collections;

/* На вход подается число N.
 * На каждой из следующих N строках записаны ФИО человека, 
 * разделенные одним пробелом. Отчество может отсутствовать.
 * Используя собственноручно написанный итератор, выведите имена людей,
 * отсортированные в лексико-графическом порядке в формате 
 *      <Фамилия_с_большой_буквы> <Заглавная_первая_буква_имени>.
 * Затем выведите имена людей в исходном порядке.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield.
 * 
 * Пример входных данных:
 * 3
 * Banana Bill Bananovich
 * Apple Alex Applovich
 * Carrot Clark Carrotovich
 * 
 * Пример выходных данных:
 * Apple A.
 * Banana B.
 * Carrot C.
 * 
 * Banana B.
 * Apple A.
 * Carrot C.
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
*/

namespace Task03
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!int.TryParse(Console.ReadLine(), out int N) || N <= 0)
                {
                    throw new ArgumentException();
                }

                Person[] people = new Person[N];

                for (int i = 0; i < N; i++)
                {
                    var words = Console.ReadLine().Split();
                    if (words.Length < 2)
                    {
                        throw new ArgumentException();
                    }
                    people[i] = new Person(words[0], words[1]);
                }

                People peopleList = new People(people);

                foreach (Person p in peopleList)
                    Console.WriteLine(p);

                foreach (Person p in peopleList.GetPeople)
                    Console.WriteLine(p);
            }
            catch (ArgumentException)
            {
                Console.Write("error");
            }
        }
    }



    public class Person
    {
        public string firstName;
        public string lastName;

        public Person(string lastName, string firstName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public override string ToString() => $"{char.ToUpper(lastName[0])}{lastName.Substring(1, lastName.Length - 1)} {char.ToUpper(firstName[0])}.";
    }



    public class People : IEnumerable
    {
        private Person[] people;
        public Person[] GetPeople { get => people; }


        public People(Person[] people)
        {
            this.people = people;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum((Person[])people.Clone());
        }
    }



    public class PeopleEnum : IEnumerator
    {
        private Person[] people;
        private int position = -1;


        public PeopleEnum(Person[] people)
        {
            Array.Sort(people, (x, y) => x.lastName.CompareTo(y.lastName));
            this.people = people;
        }


        public bool MoveNext()
        {
            if (position < people.Length - 1)
            {
                position++;
                return true;
            }
            return false;
        }


        public void Reset()
        {
            position = -1;
        }
       

        public object Current { get => people[position]; }
    }
}
