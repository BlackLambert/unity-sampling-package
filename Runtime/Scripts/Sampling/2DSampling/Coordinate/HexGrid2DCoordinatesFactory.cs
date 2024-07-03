using System;
using System.Collections.Generic;

namespace SBaier.Sampling
{
    public class HexGrid2DCoordinatesFactory : Grid2DCoordinateFactory
    {
        private readonly HexTileRotation _rotation;
        private readonly HexGridIndentation _indentation;

        public HexGrid2DCoordinatesFactory(HexTileRotation rotation, HexGridIndentation indentation)
        {
            _rotation = rotation;
            _indentation = indentation;
        }
        
        public List<Coordinate2D> Create(int width, int height)
        {
            List<Coordinate2D> result = new List<Coordinate2D>(width * height);
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    result.Add(_rotation.ConvertToHexCoordinates(_indentation, new Coordinate2D(column, row)));
                }
            }

            return result;
        }

        public Coordinate2D ToGridCoordinate(Coordinate2D hexCoordinate)
        {
            return _rotation.ConvertToGridCoordinates(_indentation, hexCoordinate);
        }
    }
}