using System.Diagnostics.CodeAnalysis;

namespace ShopPriceComparer.Core.Models
{
    /// <summary>
    /// Represents the settings for a browser session. This includes the type of browser, the path to the driver, whether the browser should run in headless mode, the directory for downloads, and the default timeout duration.
    /// </summary>
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
