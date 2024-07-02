using System;

namespace SBaier.Sampling
{
    public enum HexGridIndentation
    {
        Even = 0,
        Odd = 1
    }
    
    public static class HexGridIndentationExtensions
    {
        public static int GetCoordinateAddition(this HexGridIndentation indentation, int index)
        {
            return indentation switch
            {
                HexGridIndentation.Even => index / 2,
                HexGridIndentation.Odd => (index + 1) / 2,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public static bool IsIndented(this HexGridIndentation indentation, int index)
        {
            return indentation switch
            {
                HexGridIndentation.Even => index % 2 == 0,
                HexGridIndentation.Odd => index % 2 == 1,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}