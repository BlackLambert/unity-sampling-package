using System;

namespace SBaier.Sampling
{
    public enum HexGridIndentation
    {
        Odd = 0,
        Even = 1
    }
    
    public static class HexGridIndentationExtensions
    {
        public static int GetCoordinateAddition(this HexGridIndentation indentation, int index)
        {
            return indentation switch
            {
                HexGridIndentation.Odd => (index + (index&1)) / 2,
                HexGridIndentation.Even => (index - (index&1)) / 2,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public static bool IsIndented(this HexGridIndentation indentation, int index)
        {
            return indentation switch
            {
                HexGridIndentation.Odd => (index&1) == 0,
                HexGridIndentation.Even => (index&1) == 1,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}