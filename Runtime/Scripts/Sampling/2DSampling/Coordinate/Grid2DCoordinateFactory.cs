using System.Collections.Generic;

namespace SBaier.Sampling
{
    public interface Grid2DCoordinateFactory
    {
        List<Coordinate2D> Create(int width, int height);
        Coordinate2D ToGridCoordinate(Coordinate2D coordinate);
    }
}