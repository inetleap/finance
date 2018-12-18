using System;

namespace Finance.Exceptions
{
    [Serializable]
    public class InsufficientPaymentAmountException : Exception
    {
        public InsufficientPaymentAmountException() { }
        public InsufficientPaymentAmountException(string message) : base(message) { }
        public InsufficientPaymentAmountException(string message, Exception inner) : base(message, inner) { }
        protected InsufficientPaymentAmountException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
