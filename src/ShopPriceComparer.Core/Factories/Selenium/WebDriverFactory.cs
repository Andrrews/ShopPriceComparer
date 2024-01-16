using ShopPriceComparer.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using ShopPriceComparer.Core.Interfaces;
using ShopPriceComparer.Core.Exceptions;
using Microsoft.Extensions.Options;
namespace ShopPriceComparer.Core.Factories.Selenium
{
    /// <summary>  
    /// A factory class to create WebDriver instances.  
    /// </summary>  
    public class WebDriverFactory
    {
        private readonly SessionSettings _driverOptions;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the WebDriverFactory class. This class is responsible for creating WebDriver instances.
        /// </summary>
        /// <param name="serviceProvider">An instance of IServiceProvider which will be used for creating the WebDriver instances.</param>
        /// <param name="driverOptions">An instance of IOptions of type SessionSettings which will be used to configure the WebDriver instances.</param>
        /// <returns>
        /// Does not return a value.
        /// </returns>        
        public WebDriverFactory(IServiceProvider serviceProvider, IOptions<SessionSettings> driverOptions)
        {
            _serviceProvider = serviceProvider;
            _driverOptions = driverOptions.Value;
        }
        /// <summary>  
        /// Creates a new instance of the WebDriver.  
        /// </summary>  
        /// <returns>A new instance of the <see cref="IWebDriver"/>.</returns>  
        public IWebDriver Create()
        {
            var factory = _serviceProvider.GetServices<INamedBrowserFactory>()
                                          .FirstOrDefault(f => f.Name == _driverOptions.Browser);

            if (factory == null)
            {
                throw new ServiceNotRegisteredException($"No factory registered for {_driverOptions.Browser} browser.");
            }

            return factory.Create();

        }
    }
}
