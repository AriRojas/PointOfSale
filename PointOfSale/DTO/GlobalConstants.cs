namespace PointOfSale.DTO
{
    /// <summary>
    /// Class to store global constants for the system.
    /// </summary>
    /// <remarks>
    /// Creation. 7.06.21. Ariadna Rojas.
    /// </remarks>
    internal static class GlobalConstants
    {
        /// <summary>
        /// Regular expression to validate decimal values.
        /// </summary>
        internal const string DECIMAL_REGEX = @"^[1-9]\d*(\.\d+)?$";

        /// <summary>
        /// Name of the file that will contain the error logs of the application
        /// </summary>
        internal const string LOG_FILE_NAME = "POS_CashMasterLog_";
    }
}