using System;
using System.Collections.Generic;

namespace SalesForce
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SalesPerson Kalle = new SalesPerson("Kalle Anka", "0101010001", "Ankeborg", 123);
            Console.WriteLine(Kalle);

            SalesPerson Joakim = SalesPerson.GetSalesPerson();
            Console.WriteLine(Joakim);

            List<SalesPerson> myList = new List<SalesPerson> { };
            myList.Add(Kalle);
            myList.Add(Joakim);

            myList.Sort();
            foreach (var salesPerson in myList)
            {
                Console.WriteLine(salesPerson);
            }

            Console.ReadLine();
        }        
    }

    public class SalesPerson : IComparable<SalesPerson>
    {
        // Medlemsvariabler
        readonly string name;
        readonly string personalCodeNumber;
        readonly string district;
        readonly int soldArticles;

        // Konstruktor
        public SalesPerson(string name, string personalCodeNumber, string district, int soldArticles)
        {
            this.name = name;
            this.personalCodeNumber = personalCodeNumber;
            this.district = district;
            this.soldArticles = soldArticles;
        }

        // Metoder
        public static SalesPerson GetSalesPerson()
        {
            Console.WriteLine("Namn:");
            string name = Console.ReadLine();

            Console.WriteLine("Personnummer:");
            string personalCodeNumber = Console.ReadLine();

            Console.WriteLine("Distrikt:");
            string district = Console.ReadLine();

            Console.WriteLine("Antal sålda artiklar:");
            int soldArticles = Convert.ToInt32(Console.ReadLine());

            return new SalesPerson(name, personalCodeNumber, district, soldArticles);
        }

        public int CompareTo(SalesPerson salesPerson)
        {
            return soldArticles.CompareTo(salesPerson.soldArticles);
        }

        public override string ToString()
        {
            return name + "\t" + personalCodeNumber + "\t" + district + "\t" + soldArticles;
        }
    }
}
