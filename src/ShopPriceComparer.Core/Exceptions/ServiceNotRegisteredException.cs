

namespace ShopPriceComparer.Core.Exceptions
{
    /// <summary>
    /// Exception class that is thrown when a service is not registered.
    /// </summary>
    [Serializable]
    public class ServiceNotRegisteredException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotRegisteredException"/> class.
        /// </summary>
        public ServiceNotRegisteredException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotRegisteredException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServiceNotRegisteredException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotRegisteredException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ServiceNotRegisteredException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotRegisteredException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected ServiceNotRegisteredException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
