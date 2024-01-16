using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using ShopPriceComparer.Core.Interfaces;
using ShopPriceComparer.Core.Models;

namespace ShopPriceComparer.Core.Factories.Selenium
{
    /// <summary>
    /// This class is responsible for creating a new instance of the Firefox browser with the provided session settings.
    /// </summary>
    /// <returns>
    /// It returns a new instance of the IWebDriver representing the Firefox browser.
    /// </returns>
    public class FirefoxFactory : INamedBrowserFactory
    {
        private readonly SessionSettings _options;
        private const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36";


        /// <summary>  
        /// Initializes a new instance of the <see cref="ChromeFactory"/> class with the provided session settings.  
        /// </summary>  
        /// <param name="options">The session settings to be used for creating the browser instances.</param>  
        public FirefoxFactory(IOptions<SessionSettings> options)
        {
            _options = options.Value;
        }
        /// <summary>
        /// Gets the name of the browser, in this case, Firefox.
        /// </summary>
        public Browsers Name => Browsers.Firefox;

        /// <summary>  
        /// Creates a new instance of the Chrome browser.  
        /// </summary>  
        /// <returns>A new instance of the <see cref="IWebDriver"/> representing the Chrome browser.</returns>  
        public IWebDriver Create()
        {

            var driverService = FirefoxDriverService.CreateDefaultService();
            var options = new ChromeOptions();
            if (_options.Headless)
            {
                options.AddArgument("--headless=new");
            }
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-notifications");
            //options.AddArgument("--start-maximized");
            options.AddUserProfilePreference("download.default_directory", _options.DownloadDirectory);
            options.AddUserProfilePreference("profile.cookie_controls_mode", 0);
            options.AddArgument($"--user-agent={userAgent}");
            options.SetLoggingPreference(LogType.Browser, LogLevel.Debug);

            return new FirefoxDriver(driverService);

        }


    }
}
