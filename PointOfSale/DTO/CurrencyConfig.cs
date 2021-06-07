using System.Collections.Generic;

namespace PointOfSale.DTO
{
    /// <summary>
    /// Class to store the currency configuration according to the current culture.
    /// </summary>
    /// <remarks>
    /// Creation 6/06/21. Ariadna Rojas.
    /// </remarks>
    internal class CurrencyConfig
    {
        #region Properties

        /// <summary>
        /// List of existing cultures
        /// </summary>
        public List<Culture> Cultures { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CurrencyConfig()
        {
            Cultures = new List<Culture>();
        }

        #endregion Constructors
    }

    /// <summary>
    /// Key - Value structure of the config file
    /// </summary>
    public class Culture
    {
        #region Properties

        /// <summary>
        /// Unique identifier of the culture
        /// </summary>
        public string CultureName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string CoinName { get; set; }

        /// <summary>
        /// Array with the configured currencies
        /// </summary>
        public Stack<decimal> Currencies { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Public default constructor
        /// </summary>
        public Culture()
        {
            CultureName = string.Empty;
            Currencies = new Stack<decimal>();
        }

        #endregion Constructors
    }
}