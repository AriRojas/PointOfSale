using PointOfSale.DTO.ErrorHandling;
using System;
using System.Collections.Generic;

namespace PointOfSale
{
    internal class Program
    {
        /// <summary>
        /// Input a string, validate it and return it as decimal
        /// </summary>
        /// <returns>Decimal value of the string</returns>
        private static decimal InputDecimal()
        {
            try
            {
                bool isError = false;
                string inputValue;
                do
                {
                    Console.WriteLine("Enter price of purchase: ");
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

        /// <summary>
        /// Main execution
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Execution executionObject = new Execution();

            Console.WriteLine("*************************************************");
            Console.WriteLine("WELCOME TO POS CASH MASTER");
            Console.WriteLine("*************************************************");

            #region Choosing currency

            executionObject.ChooseCurrency();

            Console.WriteLine("Currency found: " + (executionObject.Culture.Current.CultureName.Equals(string.Empty) ? "Not found" : executionObject.Culture.Current.CoinName));
            Console.WriteLine("Change currency? Y (yes)/N (no)");
            string input;
            bool? answer;
            do
            {
                input = Console.ReadLine();
                answer = input.ValidateYesNo();
                if (answer == null) Console.WriteLine("Invalid input");
            }
            while (answer == null);

            // TODO: option to set up a new currency
            // for now choose among the existing ones
            if (executionObject.Culture.Current.CultureName.Equals(string.Empty) || (bool)answer)
            {
                foreach (var item in executionObject.Culture.CurrencyConfig.Cultures)
                {
                    Console.WriteLine($"Coin: {item.CoinName}");
                }
                Console.WriteLine("Choose coin:");
                string coinName = Console.ReadLine();
                executionObject.ChooseCurrency(coinName);
            }

            #endregion Choosing currency

            IReturnCode<Dictionary<decimal, int>> returnCodeOperation;
            string inputValue;
            decimal price;
            bool? continueExecution;
            do
            {
                price = InputDecimal();

                // initializes the object Bills so the user can input data
                executionObject.InitializeBills();

                Console.WriteLine("Enter payment amount: ");
                IReadOnlyDictionary<decimal, int> test = executionObject.Bills;
                foreach (var item in executionObject.Bills.Clone())
                {
                    Console.Write($"Number of {item} bills: ");
                    inputValue = Console.ReadLine();
                    executionObject.Bills[item.Key] = Convert.ToInt32(inputValue);
                }

                // TODO: run tasks asynchronously
                Console.WriteLine("*************************************************");
                Console.WriteLine("Calculating...");
                Console.WriteLine("*************************************************");

                returnCodeOperation = executionObject.ExecuteProgram(price);

                //returnCodeOperation = cashMaster.CalculateChange(price, paymentBills);
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
                    inputValue = Console.ReadLine();
                    continueExecution = inputValue.ValidateYesNo();

                    if (continueExecution == null) Console.WriteLine("Invalid input");
                } while (continueExecution == null);
            } while ((bool)continueExecution);
        }
    }
}