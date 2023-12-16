using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPriceComparer.Core.Interfaces
{
    /// <summary>
    /// Defines a generic interface for a factory pattern that creates instances of type T.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object that this factory creates.
    /// </typeparam>
    /// <returns>
    /// An instance of type T.
    /// </returns>
    public interface IFactory<out T>
    {
        T Create();
    }
}
