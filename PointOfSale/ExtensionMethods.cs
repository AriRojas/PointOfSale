using PointOfSale.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace PointOfSale
{
    /// <summary>
    ///
    /// </summary>
    internal static class ExtensionMethods
    {
        internal static void SaveException(this Exception ex)
        {
            //TODO: add logic to save information about the exception
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

        internal static Stack<T> Clone<T>(this Stack<T> stack)
        {
            Contract.Requires(stack != null);
            return new Stack<T>(new Stack<T>(stack));
        }
    }
}