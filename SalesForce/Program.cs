using System;

namespace SalesForce
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SalesPerson kalle = new SalesPerson("Kalle Anka", "0101010001", "Ankeborg", 123);
            Console.WriteLine(kalle);
            Console.ReadLine();
        }
    }

    public class SalesPerson
    {
        // Medlemsvariabler
        readonly string name;
        readonly string personalCodeNumber;
        readonly string district;
        readonly int soldArticles;

        // Konstruktor
        public SalesPerson ( string name, string personalCodeNumber, string district, int soldArticles)
        {
            this.name = name;
            this.personalCodeNumber = personalCodeNumber;
            this.district = district;
            this.soldArticles = soldArticles;
        }

        // Metoder
        public override string ToString()
        {
            return name + "\t" + personalCodeNumber + "\t" + district + "\t" + soldArticles;
        }
    }
}
