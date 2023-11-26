using System;

namespace PCGToolkit.Sampling
{
    public enum TileSide
    {
        Top = 0,
        TopRight = 1,
        Right = 2,
        BottomRight = 3,
        Bottom = 4,
        BottomLeft = 5,
        Left = 6,
        TopLeft = 7,
    }
    
    public static class TileSideExtension
    {
        public static int GetXDelta(this TileSide side)
        {
            switch (side)
            {
                case TileSide.Top:
                case TileSide.Bottom:
                    return 0;
                case TileSide.Left:
                case TileSide.BottomLeft:
                case TileSide.TopLeft:
                    return -1;
                case TileSide.Right:
                case TileSide.BottomRight:
                case TileSide.TopRight:
                    return 1;
                default:
                    throw new InvalidOperationException();
            }
        }
        
        public static int GetYDelta(this TileSide side)
        {
            switch (side)
            {
                case TileSide.Left:
                case TileSide.Right:
                    return 0;
                case TileSide.Top:
                case TileSide.TopLeft:
                case TileSide.TopRight:
                    return 1;
                case TileSide.Bottom:
                case TileSide.BottomLeft:
                case TileSide.BottomRight:
                    return -1;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}