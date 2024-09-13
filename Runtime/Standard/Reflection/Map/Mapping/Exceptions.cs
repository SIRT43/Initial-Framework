using System;
using System.Runtime.Serialization;

namespace InitialFramework.Reflection
{
    public class InvalidContainerException : ArgumentException
    {
        public InvalidContainerException() { }
        public InvalidContainerException(string message) : base(message) { }
        public InvalidContainerException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidContainerException(string message, string paramName) : base(message, paramName) { }
        public InvalidContainerException(string message, string paramName, Exception innerException) : base(message, paramName, innerException) { }
        protected InvalidContainerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
