using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SalesForce
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder LogString = new StringBuilder();
            List<SalesPerson> salesPersons = new List<SalesPerson> { };

            // Testdata
            salesPersons.Add(new SalesPerson("Kalle Anka", "000", "Ankeborg", 39));
            salesPersons.Add(new SalesPerson("Joakim von Anka", "000", "Ankeborg", 198));
            salesPersons.Add(new SalesPerson("Alexander Lukas", "000", "Ankeborg", 201));
            salesPersons.Add(new SalesPerson("Knatte Anka", "000", "Ankeborg", 52));
            salesPersons.Add(new SalesPerson("Fnatte Anka", "000", "Ankeborg", 75));
            salesPersons.Add(new SalesPerson("Tjatte Anka", "000", "Ankeborg", 100));
            salesPersons.Add(new SalesPerson("Knase Anka", "000", "Ankeborg", -3));
            salesPersons.Add(new SalesPerson("Kvacke Anka", "000", "Ankeborg", 0));

            // Lägg till säljare
            Console.WriteLine("Hur många säljare vill du lägga till?");
            int salesGuysToAdd = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < salesGuysToAdd; i++)
            {
                salesPersons.Add(SalesPerson.GetSalesPerson());
            }

            // Beräkna bonus
            salesPersons.Sort();
            int currentBonusGroup = BonusProgram.Levels.Length;  // Börja med högsta gruppen
            int salesPersonsInCurrentGroup = 0;
            foreach (SalesPerson salesPerson in salesPersons)
            {
                int bonusGroup = BonusProgram.BonusGroup(salesPerson);

                if (bonusGroup < currentBonusGroup)
                {
                    PrintBonusLevelSummary(salesPersonsInCurrentGroup, currentBonusGroup);
                    currentBonusGroup -= 1;
                    salesPersonsInCurrentGroup = 0;
                }
                Logger.Out(salesPerson.ToString());
                salesPersonsInCurrentGroup += 1;
            }
            PrintBonusLevelSummary(salesPersonsInCurrentGroup, currentBonusGroup);

            string logFile = Environment.CurrentDirectory + @"\log.txt";
            File.WriteAllText(logFile, Logger.LogString.ToString());
            Console.WriteLine($"Resultatet har sparats i {logFile}");
            Console.ReadLine();
        }

        private static void PrintBonusLevelSummary(int salesPersonsInCurrentGroup, int currentBonusGroup)
        {
            if (currentBonusGroup == 0) // Skriv inte ut säljare i bonusgrupp 0
            {
                return;
            }
            if (currentBonusGroup == BonusProgram.Levels.Length)
            {
                Logger.Out($"{salesPersonsInCurrentGroup} säljare har nått nivå {currentBonusGroup}: {BonusProgram.Levels[BonusProgram.Levels.Length - 1]}+ artiklar");
                Logger.Out("");
            }
            else
            {
                Logger.Out($"{salesPersonsInCurrentGroup} säljare har nått nivå {currentBonusGroup}: {BonusProgram.Levels[currentBonusGroup - 1]}-{BonusProgram.Levels[currentBonusGroup]} artiklar");
                Logger.Out("");
            }
        }
    }

    public class SalesPerson : IComparable<SalesPerson>
    {
        // Medlemsvariabler
        readonly string name;
        readonly string personalCodeNumber;
        readonly string district;
        public readonly int soldArticles;

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
            Logger.Out("Namn:");
            string name = Console.ReadLine();

            Logger.Out("Personnummer:");
            string personalCodeNumber = Console.ReadLine();

            Logger.Out("Distrikt:");
            string district = Console.ReadLine();

            Logger.Out("Antal sålda artiklar:");
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

    public class BonusProgram
    {
        public static readonly int[] Levels = { 1, 50, 100, 200 };  // Ändra bonusnivåerna här

        public static int BonusGroup(SalesPerson salesPerson)
        {
            int soldArticles = salesPerson.soldArticles;

            if (soldArticles < 1) // Returnera 0 om säljaren inte sålt något
            {
                return 0;
            }

            int bonusLevel = 1;
            for (int i = 0; i < Levels.Length; i++)
            {
                if (bonusLevel == BonusProgram.Levels.Length) // Returnera högsta möjliga bonusnivå
                {
                    return bonusLevel;
                }

                if (BonusProgram.Levels[i] <= soldArticles && soldArticles < BonusProgram.Levels[i + 1]) // Se om antal sålda artiklar ligger inom bonusintervallet
                {
                    return bonusLevel;
                }
                bonusLevel += 1;
            }

            return 0; // Returnera 0 som en sista utväg
        }
    }

    public static class Logger
    {
        public static StringBuilder LogString = new StringBuilder();

        public static void Out(string str)
        {
            Console.WriteLine(str);
            LogString.Append(str).Append(Environment.NewLine);
        }
    }
}
