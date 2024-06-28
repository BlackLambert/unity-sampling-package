using System;

namespace PCGToolkit.Sampling
{
    public enum RectTileSide
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
        public static int GetXDelta(this RectTileSide side)
        {
            switch (side)
            {
                case RectTileSide.Top:
                case RectTileSide.Bottom:
                    return 0;
                case RectTileSide.Left:
                case RectTileSide.BottomLeft:
                case RectTileSide.TopLeft:
                    return -1;
                case RectTileSide.Right:
                case RectTileSide.BottomRight:
                case RectTileSide.TopRight:
                    return 1;
                default:
                    throw new InvalidOperationException();
            }
        }
        
        public static int GetYDelta(this RectTileSide side)
        {
            switch (side)
            {
                case RectTileSide.Left:
                case RectTileSide.Right:
                    return 0;
                case RectTileSide.Top:
                case RectTileSide.TopLeft:
                case RectTileSide.TopRight:
                    return 1;
                case RectTileSide.Bottom:
                case RectTileSide.BottomLeft:
                case RectTileSide.BottomRight:
                    return -1;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}