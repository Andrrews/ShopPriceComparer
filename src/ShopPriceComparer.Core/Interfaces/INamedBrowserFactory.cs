using OpenQA.Selenium;
using ShopPriceComparer.Core.Models;

namespace ShopPriceComparer.Core.Interfaces
{
    /// <summary>
    /// Defines an interface for a named browser factory that inherits from the IFactory interface. 
    /// This interface includes a property for the name of the browser.
    /// </summary>
    public interface INamedBrowserFactory : IFactory<IWebDriver>
    {
        /// <summary>
        /// Gets the name of the browser.
        /// </summary>
        Browsers Name { get; }
    }
}
