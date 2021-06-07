namespace PointOfSale.DTO.ErrorHandling
{
    /// <summary>
    /// Base return code
    /// </summary>
    /// <remarks>
    /// Creation 6.06.21. Ariadna Rojas
    /// </remarks>
    internal class ReturnCodeBase : IReturnCodeBase
    {
        #region Properties

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Operation successfull flag
        /// </summary>
        public bool Success { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor with message and success flag
        /// </summary>
        /// <param name="message">String message</param>
        /// <param name="success">Operation Successfull?</param>
        public ReturnCodeBase(string message, bool success = false)
        {
            Message = message;
            Success = success;
        }

        /// <summary>
        /// Constructor with success flag
        /// </summary>
        /// <param name="success">Operation Successfull?</param>
        public ReturnCodeBase(bool success)
            : this(success ? "Operation Successfull" : "Oh no! Something went wrong.", success)
        {
        }

        #endregion Constructors
    }
}