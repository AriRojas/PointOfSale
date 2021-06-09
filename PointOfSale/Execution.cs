using PointOfSale.DTO.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSale
{
    /// <summary>
    /// Class to execute all the methods
    /// </summary>
    /// <remarks>
    /// Creation. 07.6.21. Ariadna Rojas
    /// </remarks>
    public class Execution
    {
        #region Properties

        public Dictionary<decimal, int> Bills { get; set; }
        public CurrencyCulture Culture { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Constructor by default
        /// </summary>
        public Execution()
        {
            Bills = new Dictionary<decimal, int>();
            Culture = new CurrencyCulture();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Choose the currency based on current culture or by user preference
        /// </summary>
        /// <returns>Object CurrencyCulture with the currency configuraion</returns>
        public void ChooseCurrency()
        {
            try
            {
                Culture.LoadCurrencyConfiguration();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Choose the currency based on the coin name
        /// </summary>
        /// <param name="coinName"></param>
        public void ChooseCurrency(string coinName)
        {
            try
            {
                Culture.FindCurrencyByCoinName(coinName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// First method to execute initializes the bills
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public void InitializeBills()
        {
            try
            {
                Bills = Culture?.Current.Currencies?.ToDictionary(c => c, c => 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes the CashMaster method to calculate change.
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public IReturnCode<Dictionary<decimal, int>> ExecuteProgram(decimal price)
        {
            CashMaster cashMaster;
            try
            {
                cashMaster = new CashMaster(Culture.Current?.Currencies.Clone() as Stack<decimal>);
                return cashMaster.CalculateChange(price, Bills);
            }
            catch (Exception ex)
            {
                ex.SaveExceptionAsync();
                throw;
            }
        }

        #endregion Methods
    }
}