
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ExpenseTracker
{
    public class Expense
    {
        // Add variables here.
        public string Name;
        public decimal Price;
        public string Categorie;

        
    }

    public class Program
    {
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            // Write the main program code here.

            Console.WriteLine("Välkommen!");
            bool runing = true;
            List<Expense> expenseList = new List<Expense>();

            
            

            while (runing)
            {


                int selectedOption = ShowMenu("Vad vill du göra?", new[]
                {
                    "-Lägg till utgift",
                    "-Visa alla utgifter",
                    "-Visa summa per kategori",
                    "-Ta bord samtliga utgifter",
                    "-Avsluta"

                });
                Console.Clear();


                if (selectedOption == 0)
                {
                    
                    Console.WriteLine();
                    Console.WriteLine("Lägg till utgift:");

                    Console.Write("Namn: ");
                    string name = Console.ReadLine();

                    Console.Write("Pris: ");
                    decimal price = decimal.Parse(Console.ReadLine());
                    
                    
                    string categorie;

                    Console.WriteLine();
                    int selectedCategorie = ShowMenu("Välj kategori:", new[]
                    {
                        "-Utbildning",
                        "-Livsmedel",
                        "-Underhållning",
                        "-Övrigt"
                    });
                    Console.Clear();


                    if (selectedCategorie == 0)
                    {
                        
                        categorie = "Utbildning";
                    }
                    else if (selectedCategorie == 1)
                    {
                        categorie = "Livsmedel";
                    }
                    else if (selectedCategorie == 2)
                    {
                        categorie = "Underhållning";
                    }
                    else
                    {
                        categorie = "Övrigt";
                    }
                    expenseList.Add(new Expense
                    {
                        Name = name,
                        Price = price,
                        Categorie = categorie
                    });
                    
                }

                // We need to use "ToString()" in order to round a decimal-variable, if we use "Math.Round" we will get bugg when "Price" input contains less than 2 numbers efter the decimal.
                // We need also creat new variables in order to round our numbers with "ToString()" method.
                else if (selectedOption == 1)
                {
                    
                    foreach  (Expense expense in expenseList)
                    { 
                        Console.WriteLine($"{expense.Name}: {expense.Price} kr ({expense.Categorie})");
                    }
                    
                    decimal sumVAT =  SumExpenses(expenseList, true);
                    decimal sumNonVAT = SumExpenses(expenseList, false);
                    Console.WriteLine();
                    Console.WriteLine($"Antal utgifter: {expenseList.Count}");
                    Console.WriteLine($"Summa: {sumVAT.ToString("0,00")} kr inkl.moms ({sumNonVAT.ToString("0,00")}) kr exkl.moms)");
                       
                }

                else if (selectedOption == 2)
                {
                    
                    decimal educationVAT = SumEducation(expenseList, true);
                    decimal educationNonVAT = SumEducation(expenseList, false);

                    decimal foodVAT = SumFood(expenseList, true);
                    decimal foodNonVAT = SumFood(expenseList, false);

                    decimal intertainmentVAT = SumIntertainment(expenseList, true);
                    decimal intertainmentNonVAT = SumIntertainment(expenseList, false);

                    decimal otherVAT = SumOther(expenseList, true);
                    decimal otherNonVAT = SumOther(expenseList, false);

                    
                    Console.WriteLine($"Utbildning: {educationVAT.ToString("0,00")} ({educationNonVAT.ToString("0,00")} kr exkl.moms)");
                    Console.WriteLine($"Livsmedel: {foodVAT.ToString("0,00")} kr inkl.moms ({foodNonVAT.ToString("0,00")} kr exkl.moms)");
                    Console.WriteLine($"Underhållning: {intertainmentVAT.ToString("0,00")} kr inkl.moms ({intertainmentNonVAT.ToString("0,00")} kr exkl.moms)");
                    Console.WriteLine($"Övrigt: {otherVAT.ToString("0,00")} kr inkl.moms ({otherNonVAT.ToString("0,00")} kr exkl.moms)");
                }

                else if (selectedOption == 3)
                {
                    Console.WriteLine("Är du säkert?");
                    int yesOrNo = ShowMenu("Är du säkert?", new[]
                    {
                        "-Ja",
                        "-Nej"
                    });
                    
                    if (yesOrNo == 0)
                    {
                        expenseList.Clear();
                        Console.WriteLine();
                        Console.WriteLine("Samtliga utgifter har tagits bort!");
                    }
                    else
                    {
                        Console.WriteLine("Inga utgifter har tagits bort!");
                        Console.Clear();
                    }
                }
                



            }
        }

        // Return the sum of all expenses in the specified list, with or without VAT based on the second parameter.
        // This method *must* be in the program and *must* be used in both the main program and in the tests.
        public static decimal SumExpenses(List<Expense> expenses, bool includeVAT)
        {
            decimal sum = 0;
            // Implement the rest of this method here.
            if (includeVAT)
            {
                foreach (Expense expense in expenses)
                {
                    sum += expense.Price;
                }
            }
            else
            {
                foreach (Expense expense in expenses)
                {
                    if (expense.Categorie.Contains("Utbildning"))
                    {
                        sum += expense.Price;
                    }
                    else if (expense.Categorie.Contains("Livsmedel"))
                    {
                        sum += expense.Price / 1.6m;
                    }
                    else if (expense.Categorie.Contains("Underhållning"))
                    {
                        sum += expense.Price / 1.12m;
                    }
                    else
                    {
                        sum += expense.Price / 1.25m;
                    }
                }            

                
            }
            return sum;
        }

        public static decimal SumEducation(List<Expense> expenses, bool includeVAT)
        {
            decimal sum = 0;
            // Implement the rest of this method here.
            if (includeVAT)
            {
                foreach (Expense exp in expenses)
                {
                    if (exp.Categorie.Contains("Utbildning"))
                    {
                        sum += exp.Price;

                    }
                    
                }
            }
            else
            {
                foreach (Expense exp in expenses)
                {
                    if (exp.Categorie == "Utbildning")
                    {
                        sum += exp.Price;
                    }
                    
                }


            }
            return sum;
        }

        public static decimal SumFood(List<Expense> expenses, bool includeVAT)
        {
            Decimal sum = 0;

            if (includeVAT)
            {


                foreach (Expense exp in expenses)
                {

                    if (exp.Categorie.Contains("Livsmedel"))
                    {
                        sum += exp.Price;
                    }
                }
            }
            else
            {
                foreach (Expense exp in expenses)
                {
                    if (exp.Categorie.Contains("Livsmedel"))
                    {
                        sum += exp.Price / 1.06m; 
                    }
                }
            }
            return sum;
        }

        public static decimal SumIntertainment(List<Expense> expenses, bool includeVAT)
        {
            Decimal sum = 0;

            if (includeVAT)
            {


                foreach (Expense exp in expenses)
                {

                    if (exp.Categorie.Contains("Underhållning"))
                    {
                        sum += exp.Price;
                    }
                }
            }
            else
            {
                foreach (Expense exp in expenses)
                {
                    if (exp.Categorie.Contains("Underhållning"))
                    {
                        sum += exp.Price / 1.12m;
                    }
                }
            }
            return sum;
        }

        public static decimal SumOther(List<Expense> expenses, bool includeVAT)
        {
            Decimal sum = 0;

            if (includeVAT)
            {


                foreach (Expense exp in expenses)
                {

                    if (exp.Categorie.Contains("Övrigt"))
                    {
                        sum += exp.Price;
                    }
                }
            }
            else
            {
                foreach (Expense exp in expenses)
                {
                    if (exp.Categorie.Contains("Övrigt"))
                    {
                        sum += exp.Price / 1.25m;
                    }
                }
            }
            return sum;
        }


        // Do not change this method.
        // For more information about ShowMenu: https://csharp.jakobkallin.com/large-exercises/
        public static int ShowMenu(string prompt, IEnumerable<string> options)
        {
            if (options == null || options.Count() == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty list of options.");
            }

            Console.WriteLine(prompt);

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            // Calculate the width of the widest option so we can make them all the same width later.
            int width = options.Max(option => option.Length);

            int selected = 0;
            int top = Console.CursorTop;
            for (int i = 0; i < options.Count(); i++)
            {
                // Start by highlighting the first option.
                if (i == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var option = options.ElementAt(i);
                // Pad every option to make them the same width, so the highlight is equally wide everywhere.
                Console.WriteLine("- " + option.PadRight(width));

                Console.ResetColor();
            }
            Console.CursorLeft = 0;
            Console.CursorTop = top - 1;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(intercept: true).Key;

                // First restore the previously selected option so it's not highlighted anymore.
                Console.CursorTop = top + selected;
                string oldOption = options.ElementAt(selected);
                Console.Write("- " + oldOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.ResetColor();

                // Then find the new selected option.
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count() - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }

                // Finally highlight the new selected option.
                Console.CursorTop = top + selected;
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                string newOption = options.ElementAt(selected);
                Console.Write("- " + newOption.PadRight(width));
                Console.CursorLeft = 0;
                // Place the cursor one step above the new selected option so that we can scroll and also see the option above.
                Console.CursorTop = top + selected - 1;
                Console.ResetColor();
            }

            // Afterwards, place the cursor below the menu so we can see whatever comes next.
            Console.CursorTop = top + options.Count();

            // Show the cursor again and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }
    }

    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void SumAllExpensesInklMomsTest()
        {
            List<Expense> expensList = new List<Expense>();

            Expense expens1 = new Expense
            {
                Name = "Appel",
                Price = 200,
                Categorie = "Livsmedel"
            };
            Expense expens2 = new Expense
            {
                Name = "orange",
                Price = 150,
                Categorie = "Livsmedel"
            };
            expensList.Add(expens1);
            expensList.Add(expens2);

            

            decimal sumOfExpenses = Program.SumExpenses(expensList, true);
            sumOfExpenses = Math.Round(sumOfExpenses, 2);

            Assert.AreEqual(sumOfExpenses, 350);


        }

        [TestMethod]
        public void SumAllExpensesExklMoms()
        {
            List<Expense> expenses = new List<Expense>();
            
            

            Expense expense1 = new Expense
            {
                Name = "äpple",
                Price = 100,
                Categorie = "Livsmedel"
            };
            Expense expense2 = new Expense
            {
                Name = "Appelsin",
                Price = 100,
                Categorie = "Livsmedel"
            };
           
            expenses.Add(expense1);
            expenses.Add(expense2);
            

            decimal sumExklMoms = Program.SumExpenses(expenses, false);
            sumExklMoms = Math.Round(sumExklMoms, 2);
            Assert.AreEqual(sumExklMoms, 200);
        }

        [TestMethod]
        public void SumOfFoodCategorie()
        {
            List<Expense> expenses = new List<Expense>();
            
            decimal categorieInklMoms;

            Expense expense1 = new Expense
            {
                Name = "Äpple",
                Price = 200,
                Categorie = "Livsmedel"
            };
            Expense expense2 = new Expense
            {
                Name = "Appelsin",
                Price = 150,
                Categorie = "Livsmedel"
            };
            expenses.Add(expense1);
            expenses.Add(expense2);

            categorieInklMoms = Program.SumFood(expenses, true);
            categorieInklMoms = Math.Round(categorieInklMoms, 2);
            Assert.AreEqual(categorieInklMoms, 350);

        }
    }
}

