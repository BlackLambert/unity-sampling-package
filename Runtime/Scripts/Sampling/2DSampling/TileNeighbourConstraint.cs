using System;
using System.Collections.Generic;
using System.Linq;

namespace PCGToolkit.Sampling
{
    public class TileNeighbourConstraint<T> : Constraint<TileSamplingValidationContext<T>> where T : Tile
    {
        public delegate Coordinate2D GetNeighborCoordinate(int sideIndex, Coordinate2D coordinate);
        
        private readonly Dictionary<int, Coordinate2D> _sideToCoordinateDelta;
        private readonly int _tileSidesCount;
        private readonly int _tileSidesHalfCount;
        private readonly GetNeighborCoordinate _getNeighborCoordinate;

        public TileNeighbourConstraint(int tileSidesCount, GetNeighborCoordinate getNeighborCoordinate)
        {
            _tileSidesCount = tileSidesCount;
            _tileSidesHalfCount = _tileSidesCount / 2;
            _getNeighborCoordinate = getNeighborCoordinate;
        }

        public bool IsValid(TileSamplingValidationContext<T> context)
        {
            Grid2D<T> grid = context.Grid;
            T currentElement = context.CurrentDomainElementToValidate;
            return IsTileValid(grid, currentElement, context.CurrentSampleCoordinate);
        }

        private bool IsTileValid(Grid2D<T> grid, T currentElement, Coordinate2D currentSampleCoordinate)
        {
            for (int sideIndex = 0; sideIndex < _tileSidesCount; sideIndex++)
            {
                if (!IsValidNeighbour(grid, currentElement, sideIndex, currentSampleCoordinate))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidNeighbour(Grid2D<T> grid, T currentElement, int sideIndex, Coordinate2D currentSampleCoordinate)
        {
            Coordinate2D neighborCoordinate = _getNeighborCoordinate(sideIndex, currentSampleCoordinate);
            bool hasTile = grid.TryGet(neighborCoordinate.X, neighborCoordinate.Y, out T tile);
            return !hasTile || IsValidSocket(tile, sideIndex, currentElement);
        }

        private bool IsValidSocket(T tile, int sideIndex, T currentElement)
        {
            int otherSocketId = GetSocketIdForOtherSide(tile, sideIndex);
            return otherSocketId < 0 || otherSocketId == currentElement.GetSocketIdFor(sideIndex);
        }

        private int GetSocketIdForOtherSide(T tile, int sideIndex)
        {
            int otherSide = (sideIndex + _tileSidesHalfCount) % _tileSidesCount;
            return tile.GetSocketIdFor(otherSide);
        }
    }
}
