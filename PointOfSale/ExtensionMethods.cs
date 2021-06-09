using PointOfSale.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PointOfSale
{
    /// <summary>
    /// Extension methods class
    /// </summary>
    /// <remarks>
    /// Creation 7.06.21. Ariadna Rojas.
    /// </remarks>
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Saves the information about a given exception.
        /// </summary>
        /// <param name="ex">Exception object</param>
        internal async static void SaveExceptionAsync(this Exception ex)
        {
            try
            {
                //TODO: ideally this should save more information and do it in a more visible path/tool/dashboard
                StringBuilder messageLog = new StringBuilder();
                messageLog.AppendLine(ex.GetExceptionMessageLog());
                messageLog.AppendLine(ex.InnerException.GetExceptionMessageLog());
                string fileName = string.Concat(GlobalConstants.LOG_FILE_NAME, DateTime.Now.Millisecond.ToString());
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    await writer.WriteAsync(messageLog.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Concatenates the relevant information about an exception object.
        /// </summary>
        /// <param name="ex">Exception object</param>
        /// <returns>String message log</returns>
        internal static string GetExceptionMessageLog(this Exception ex)
        {
            return string.Concat($"Message: {ex?.Message}", Environment.NewLine,
                $"StackTrace: {ex?.StackTrace}", Environment.NewLine,
                $"Source: {ex?.Source} - HR Result: {ex?.HResult}", Environment.NewLine);
        }

        /// <summary>
        /// Validates if a string is Y (yes) or N (no) and return a boolean accordingly
        /// </summary>
        /// <param name="yesNoString"></param>
        /// <returns>
        /// - Null if the input string is neither Y nor N
        /// - True if the input string is Y
        /// - False if the input string is N
        /// </returns>
        internal static bool? ValidateYesNo(this string yesNoString)
        {
            bool isYesNo = yesNoString.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ||
                yesNoString.Equals("N", StringComparison.InvariantCultureIgnoreCase);

            if (!isYesNo) return null;

            return yesNoString.Equals("Y", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Validate if a given string is a valid decimal
        /// </summary>
        /// <param name="currency">Input string</param>
        /// <returns>True if valid, false if not</returns>
        internal static bool ValidateDecimal(this string currency)
        {
            Regex regex = new Regex(GlobalConstants.DECIMAL_REGEX);
            return regex.IsMatch(currency);
        }

        /// <summary>
        /// Clones an instance of IEnumerable.
        /// </summary>
        /// <typeparam name="T">Type of the elements of the collection</typeparam>
        /// <param name="collection">Collection to clone</param>
        /// <returns>A new Collection of type T with the same values</returns>
        internal static IEnumerable<T> Clone<T>(this IEnumerable<T> collection)
        {
            Contract.Requires(collection != null);
            return new Stack<T>(new Stack<T>(collection));
        }
    }
}