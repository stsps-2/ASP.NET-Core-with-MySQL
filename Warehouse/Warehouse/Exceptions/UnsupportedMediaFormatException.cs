using System;

namespace Warehouse.Exceptions
{
    public class UnsupportedMediaFormatException : Exception
    {
        public UnsupportedMediaFormatException(string message)
            : base(message)
        {
            
        }
    }
}
