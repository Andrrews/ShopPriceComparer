using System.Diagnostics.CodeAnalysis;

namespace ShopPriceComparer.Core.Models
{
    /// <summary>
    /// Represents the settings for a browser session. This includes the type of browser, the path to the driver, whether the browser should run in headless mode, the directory for downloads, and the default timeout duration.
    /// </summary>
    /// <param name="Browser">The type of browser to be used in the session.</param>
    /// <param name="DriverPath">The path to the driver for the browser.</param>
    /// <param name="Headless">Determines whether the browser should run in headless mode.</param>
    /// <param name="DownloadDirectory">The directory where downloads will be stored.</param>
    /// <param name="DefaultTimeoutSeconds">The default timeout duration in seconds.</param>
    /// <returns>
    /// An instance of SessionSettings class.
    /// </returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public class SessionSettings
    {
        public Browsers Browser { get; set; }
        public string DriverPath { get; set; } = string.Empty;
        public bool Headless { get; set; }
        public string DownloadDirectory { get; set; } = string.Empty;
        public uint DefaultTimeoutSeconds { get; set; }

    }
}
