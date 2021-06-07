using PointOfSale.DTO.ErrorHandling;
using System;
using System.Collections.Generic;

namespace PointOfSale
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                CurrencyCulture culture = new CurrencyCulture();
                Console.WriteLine("*************************************************");
                Console.WriteLine("WELCOME TO POS CASH MASTER");
                Console.WriteLine("*************************************************");
                culture.LoadCurrencyConfiguration();

                Console.WriteLine("Currency found: " + culture.CurrentCulture.CultureName);
                Console.WriteLine("*************************************************");
                Console.WriteLine("Enter price of purchase: ");
                string price = Console.ReadLine();
                Console.WriteLine("Enter payment amount: ");
                string pay = Console.ReadLine();
                CashMaster cashMaster = new CashMaster(culture.CurrentCulture.Currencies);
                // TODO: run tasks asynchronously and allow more than one run
                IReturnCode<Dictionary<decimal, int>> returnCodeOperation = cashMaster.CalculateChange(Convert.ToDecimal(price), Convert.ToDecimal(pay));
                Console.WriteLine("*************************************************");
                Console.WriteLine("Calculating...");
                Console.WriteLine("*************************************************");
                Console.WriteLine(returnCodeOperation.Message);
                if (returnCodeOperation.Success)
                {
                    Console.WriteLine("The amount of bills to give back is:");
                    foreach (var item in returnCodeOperation.Result)
                    {
                        Console.WriteLine($"{item.Value} bills/coins of {item.Key}");
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: add logic to handle exceptions in the last layer
            }
        }
    }
}