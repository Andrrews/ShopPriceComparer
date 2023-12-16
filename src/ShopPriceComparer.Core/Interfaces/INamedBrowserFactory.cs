using OpenQA.Selenium;
using ShopPriceComparer.Core.Models;

namespace ShopPriceComparer.Core.Interfaces
{
    public interface INamedBrowserFactory : IFactory<IWebDriver>
    {
        Browsers Name { get; }
    }


}
