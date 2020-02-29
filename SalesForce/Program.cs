using System;
using System.Collections.Generic;

namespace SalesForce
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SalesPerson> salesPersons = new List<SalesPerson> { };

            // Testdata
            salesPersons.Add(new SalesPerson("Kalle Anka", "000", "Ankeborg", 39));
            salesPersons.Add(new SalesPerson("Joakim von Anka", "000", "Ankeborg", 198));
            salesPersons.Add(new SalesPerson("Alexander Lukas", "000", "Ankeborg", 201));
            salesPersons.Add(new SalesPerson("Knatte Anka", "000", "Ankeborg", 52));
            salesPersons.Add(new SalesPerson("Fnatte Anka", "000", "Ankeborg", 75));
            salesPersons.Add(new SalesPerson("Tjatte Anka", "000", "Ankeborg", 100));

            // Lägg till säljare
            Console.WriteLine("Hur många säljare vill du lägga till?");
            int salesGuysToAdd = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < salesGuysToAdd; i++)
            {
                salesPersons.Add(SalesPerson.GetSalesPerson());
            }

            salesPersons.Sort();
            foreach (var salesPerson in salesPersons)
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
            // Ett minustecken innan sorterar från högsta till lägsta
            return -soldArticles.CompareTo(salesPerson.soldArticles);
        }

        public override string ToString()
        {
            return name + "\t" + personalCodeNumber + "\t" + district + "\t" + soldArticles;
        }
    }
}
