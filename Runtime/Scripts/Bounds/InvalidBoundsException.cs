using System;

namespace PCGToolkit.Sampling
{
    public class InvalidBoundsException : ArgumentException 
    {
        public InvalidBoundsException() : base("Please provide bounds with valid values") { }
        public InvalidBoundsException(string message) : base(message) { }
    }
}
