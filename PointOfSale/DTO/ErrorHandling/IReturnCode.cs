namespace PointOfSale.DTO.ErrorHandling
{
    /// <summary>
    /// Interface to add a result object to a return code.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Creation. 6/06/21. Ariadna Rojas.
    /// </remarks>
    internal interface IReturnCode<T> : IReturnCodeBase
    {
        T Result { get; set; }
    }
}