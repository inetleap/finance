using System;

namespace Finance.Exceptions
{
    [Serializable]
    public class InvalidAccountTypeException : Exception
    {
        public InvalidAccountTypeException() { }
        public InvalidAccountTypeException(string message) : base(message) { }
        public InvalidAccountTypeException(string message, Exception inner) : base(message, inner) { }
        protected InvalidAccountTypeException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
