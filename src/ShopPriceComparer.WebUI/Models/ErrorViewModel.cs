namespace ShopPriceComparer.WebUI.Models
{
    /// <summary>
/// Represents a model for handling errors in the application.
/// </summary>
/// <remarks>
/// This model contains the request ID associated with the error and a flag indicating whether the request ID should be displayed.
/// </remarks>
/// <param name="RequestId">The unique identifier for the request that caused the error.</param>
/// <returns>
/// A boolean value indicating whether the request ID should be displayed.
/// </returns>
public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
