namespace PointOfSale.DTO.ErrorHandling
{
    /// <summary>
    /// Base return code interface.
    /// </summary>
    /// <remarks>
    /// Creation. 6.06.21. Ariadna Rojas
    /// </remarks>
    public interface IReturnCodeBase
    {
        string Message { get; set; }
        bool Success { get; set; }
    }
}