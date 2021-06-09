using System;
using System.Collections.Generic;

namespace PointOfSale
{
    /// <summary>
    /// Class created with the only purpose of testing the extension methods.
    /// </summary>
    /// <remarks>
    /// Creation 08.06.21. Ariadna Rojas
    /// </remarks>
    public class TextExtensionMethods
    {
        public void SaveExceptionAsync(Exception ex)
        {
            try
            {
                ex.SaveExceptionAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GetExceptionMessageLog(Exception ex)
        {
            try
            {
                ex.GetExceptionMessageLog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void ValidateYesNo(string yesNoString)
        {
            try
            {
                yesNoString.ValidateYesNo();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ValidateDecimal(string currency)
        {
            try
            {
                currency.ValidateDecimal();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Clone<T>(IEnumerable<T> collection)
        {
            try
            {
                collection.Clone();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}