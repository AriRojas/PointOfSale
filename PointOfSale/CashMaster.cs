using PointOfSale.DTO.ErrorHandling;
using System;
using System.Collections.Generic;

namespace PointOfSale
{
    /// <summary>
    /// Contains the logic to calculate the change to give back.
    /// Creation. 6/06/21. Ariadna Rojas.
    /// </summary>
    internal class CashMaster
    {
        #region Properties

        /// <summary>
        /// The bills/coins values to use
        /// </summary>
        private Stack<decimal> currencyStack { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="currencyStack">Stack with the values of the bills to use</param>
        internal CashMaster(Stack<decimal> currencyStack)
        {
            this.currencyStack = currencyStack;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Calculates the amount of bills/coins to give back as change.
        /// </summary>
        /// <param name="price">Price of the product purchased</param>
        /// <param name="pay">Amount of the payment</param>
        /// <returns>
        ///     IReturnCode<Dictionary<decimal, int>> : Key to the dictionary is the bill/coin and the value is the number of bills calculated
        /// </returns>
        internal IReturnCode<Dictionary<decimal, int>> CalculateChange(decimal price, decimal pay)
        {
            try
            {
                // First validate the amounts are valid according to the rules
                IReturnCodeBase evaluation = ValidateAmounts(price, pay);
                if (!evaluation.Success)
                {
                    return new ReturnCode<Dictionary<decimal, int>>(evaluation);
                }
                // structure to save the number of bills
                Dictionary<decimal, int> currentChange = new Dictionary<decimal, int>();
                decimal change = pay - price; // quantity to give back
                int numberOfBills = 0; // number of bills of each denomination

                // take the first (bigger) bill available
                decimal currentElement = currencyStack.Pop();
                do
                {
                    // number of bills that fit into the remaining change
                    numberOfBills = (int)(change / currentElement);
                    if (numberOfBills > 0)
                    {
                        change = change % currentElement;
                        if (currentChange.ContainsKey(currentElement))
                            currentChange[currentElement] = currentChange[currentElement] + numberOfBills;
                        else
                            currentChange.Add(currentElement, numberOfBills);
                    }

                    // break the loop if there are no more bills/coins
                    if (currencyStack.Count == 0) break;
                    currentElement = currencyStack.Pop();
                } while (change > 0);

                if (change == 0)
                    return new ReturnCode<Dictionary<decimal, int>>("Operation Successfull", true, currentChange);
                else
                    return new ReturnCode<Dictionary<decimal, int>>("The configured bills cannot give an exact change for the purchased product.", true, currentChange);
            }
            catch (Exception ex)
            {
                ex.SaveException();
                throw;
            }
        }

        #endregion Public Methods

        #region Private methods

        /// <summary>
        /// Validates the following:
        /// - The payment is enough to cover the full price of the product
        /// - The payment is not equals to the price of the product
        /// </summary>
        /// <param name="price">Price of the item purchased</param>
        /// <param name="pay">Amount payed for the product</param>
        /// <returns></returns>
        private IReturnCodeBase ValidateAmounts(decimal price, decimal pay)
        {
            try
            {
                if (price > pay)
                    return new ReturnCodeBase("The payment is not enough to cover the price of the product");

                if (price == pay)
                    return new ReturnCodeBase("The payment provided cannot be equals to the price of the product");

                return new ReturnCodeBase(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Private methods
    }
}