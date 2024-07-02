using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class RectGrid2DCoordinatesFactory : Grid2DCoordinateFactory
    {
        public List<Coordinate2D> Create(int width, int height)
        {
            List<Coordinate2D> result = new List<Coordinate2D>(width * height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result.Add(new Coordinate2D { X = x, Y = y});
                }
            }

            return result;
        }

        public Coordinate2D ToGridCoordinate(Coordinate2D coordinate)
        {
            return coordinate;
        }
    }
}