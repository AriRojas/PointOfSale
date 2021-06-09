using Newtonsoft.Json;
using PointOfSale.DTO;
using System;
using System.Globalization;
using System.IO;

namespace PointOfSale
{
    /// <summary>
    /// Class that contains the logic to decide which currency to use based on the current culture.
    /// </summary>
    /// <remarks>
    /// Creation. 6/06/21. Ariadna Rojas.
    /// </remarks>
    // TODO: create methods to set up a new configuration, always store bills in ascending order
    public class CurrencyCulture
    {
        #region Constants

        /// <summary>
        /// Name of the file that contains all the currency configuration per culture
        /// </summary>
        private const string CONFIG_FILE_NAME = "CurrencyConfig.json";

        #endregion Constants

        #region Properties

        /// <summary>
        /// Private property to set internally the culture retrieved from the file
        /// </summary>
        private Culture currentCulture { get; set; }

        /// <summary>
        /// Public property to get the value of the culture retrieved from the file
        /// </summary>
        public Culture Current { get { return currentCulture; } }

        /// <summary>
        /// Private property to set the file configuration
        /// </summary>
        private CurrencyConfig currencyConfig { get; set; }

        /// <summary>
        /// Public property to get the file configuration
        /// </summary>
        public CurrencyConfig CurrencyConfig { get { return currencyConfig; } }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CurrencyCulture()
        {
            currencyConfig = new CurrencyConfig();
            currentCulture = new Culture();
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// - Reads the configuration file.
        /// - Locates the corresponding currency based on the culture
        /// </summary>
        internal void LoadCurrencyConfiguration()
        {
            try
            {
                ReadConfigFile();
                if (CurrencyConfig != null)
                {
                    currentCulture = CurrencyConfig.Cultures.Find(c => c.CultureName.Equals(CultureInfo.CurrentCulture.Name));
                }
            }
            catch (Exception ex)
            {
                ex.SaveExceptionAsync();
                throw;
            }
        }

        /// <summary>
        /// Find currency by coin name
        /// </summary>
        /// <param name="coinName">Coin name as it's in the JSON file</param>
        internal void FindCurrencyByCoinName(string coinName)
        {
            try
            {
                currentCulture = CurrencyConfig.Cultures.Find(c => c.CoinName.Equals(coinName));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Reads the JSON file from the path into a CurrencyConfig object.
        /// </summary>
        private void ReadConfigFile()
        {
            try
            {
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                string pathToConfigFile = Path.Combine(appPath, CONFIG_FILE_NAME);
                using (StreamReader reader = new StreamReader(pathToConfigFile))
                {
                    currencyConfig = JsonConvert.DeserializeObject<CurrencyConfig>(reader.ReadToEnd());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Private Methods
    }
}