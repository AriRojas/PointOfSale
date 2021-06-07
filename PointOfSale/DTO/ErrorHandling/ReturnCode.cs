namespace PointOfSale.DTO.ErrorHandling
{
    /// <summary>
    /// Error handling class with result object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Creation. 6.06.21. Ariadna Rojas.
    /// </remarks>
    internal class ReturnCode<T> : IReturnCode<T>
    {
        #region Properties

        /// <summary>
        /// Default error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Wether the operation was successfull or not
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Result object
        /// </summary>
        public T Result { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor with message and success flag
        /// </summary>
        /// <param name="message">String message</param>
        /// <param name="success">Operation Successfull?</param>
        public ReturnCode(string message, bool success = false)
        {
            Message = message;
            Success = success;
            Result = default(T);
        }

        /// <summary>
        /// Constructor using a base class
        /// </summary>
        /// <param name="returnCode">Base class return code object</param>
        public ReturnCode(IReturnCodeBase returnCode)
            : this(returnCode.Message, returnCode.Success)
        {
        }

        /// <summary>
        /// Constructor with message, success flag and result object
        /// </summary>
        /// <param name="message">String message</param>
        /// <param name="success">Operation Successfull?</param>
        /// <param name="result">Result Object</param>
        public ReturnCode(string message, bool success, T result)
            : this(message, success)
        {
            Result = result;
        }

        #endregion Constructors
    }
}