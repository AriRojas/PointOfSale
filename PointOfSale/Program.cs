using PointOfSale.DTO.ErrorHandling;
using System;
using System.Collections.Generic;

namespace PointOfSale
{
    internal class Program
    {
        /// <summary>
        /// Choose the currency based on current culture or by user preference
        /// </summary>
        /// <returns>Object CurrencyCulture with the currency configuraion</returns>
        private static CurrencyCulture ChooseCurrency()
        {
            try
            {
                CurrencyCulture culture = new CurrencyCulture();
                culture.LoadCurrencyConfiguration();

                Console.WriteLine("Currency found: " + (culture.CurrentCulture.CultureName.Equals(string.Empty) ? "Not found" : culture.CurrentCulture.CoinName));
                Console.WriteLine("Change currency? Y (yes)/N (no)");
                string input = string.Empty;
                bool? answer = false;
                do
                {
                    input = Console.ReadLine();
                    answer = input.ValidateYesNo();
                    if (answer == null) Console.WriteLine("Invalid input");
                }
                while (answer == null);

                // TODO: option to set up a new currency
                // for now choose among the existing ones
                if (culture.CurrentCulture.CultureName.Equals(string.Empty) || (bool)answer)
                {
                    foreach (var item in culture.CurrencyConfig.Cultures)
                    {
                        Console.WriteLine($"Coin: {item.CoinName}");
                    }
                    Console.WriteLine("Choose coin:");
                    string coinName = Console.ReadLine();
                    culture.FindCurrencyByCoinName(coinName);
                }

                return culture;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Input a string, validate it and return it as decimal
        /// </summary>
        /// <returns>Decimal value of the string</returns>
        private static decimal InputDecimal()
        {
            try
            {
                bool isError = false;
                string inputValue = string.Empty;

                do
                {
                    inputValue = Console.ReadLine();
                    isError = !inputValue.ValidateDecimal();
                    if (isError) Console.WriteLine("Invalid input");
                }
                while (isError);

                return Convert.ToDecimal(inputValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static Dictionary<decimal, int> InputBills(CurrencyCulture currency)
        {
            try
            {
                decimal[] elements = currency.CurrentCulture.Currencies.ToArray();
                decimal element;
                string inputValue;
                Dictionary<decimal, int> keyValuePairs = new Dictionary<decimal, int>();
                for (int i = 0; i < elements.Length; i++)
                {
                    element = elements[i];
                    Console.Write($"Number of {element} bills: ");
                    inputValue = Console.ReadLine();
                    Console.WriteLine();

                    keyValuePairs.Add(element, Convert.ToInt32(inputValue));
                }
                return keyValuePairs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Main execution
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            decimal price;
            CurrencyCulture currency;
            bool? continueExecution;
            string answerContinue;
            CashMaster cashMaster;
            try
            {
                Console.WriteLine("*************************************************");
                Console.WriteLine("WELCOME TO POS CASH MASTER");
                Console.WriteLine("*************************************************");
                currency = ChooseCurrency();
                Console.WriteLine("*************************************************");
                IReturnCode<Dictionary<decimal, int>> returnCodeOperation;
                Dictionary<decimal, int> paymentBills;
                do
                {
                    Console.WriteLine("Enter price of purchase: ");
                    price = InputDecimal();

                    Console.WriteLine("Enter payment amount: ");
                    //pay = InputDecimal();
                    paymentBills = InputBills(currency);

                    cashMaster = new CashMaster(currency.CurrentCulture.Currencies.Clone());
                    // TODO: run tasks asynchronously
                    Console.WriteLine("*************************************************");
                    Console.WriteLine("Calculating...");
                    Console.WriteLine("*************************************************");
                    returnCodeOperation = cashMaster.CalculateChange(price, paymentBills);
                    Console.WriteLine(returnCodeOperation.Message);
                    if (returnCodeOperation.Success)
                    {
                        Console.WriteLine("The amount of bills to give back is:");
                        foreach (var item in returnCodeOperation.Result)
                        {
                            Console.WriteLine($"{item.Value} bills/coins of {item.Key}");
                        }
                    }
                    Console.WriteLine("*************************************************");

                    do
                    {
                        Console.WriteLine("Run again?  Y (yes)/N (no)");
                        answerContinue = Console.ReadLine();
                        continueExecution = answerContinue.ValidateYesNo();

                        if (continueExecution == null) Console.WriteLine("Invalid input");
                    } while (continueExecution == null);
                } while ((bool)continueExecution);
            }
            catch (Exception ex)
            {
                ex.SaveExceptionAsync();
                Console.WriteLine("An unexpected error has ocurred in the system. Contact your system administrator.");
            }
        }
    }
}