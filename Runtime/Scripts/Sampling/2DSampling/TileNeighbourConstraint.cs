using System;
using System.Collections.Generic;
using System.Linq;

namespace PCGToolkit.Sampling
{
    public class TileNeighbourConstraint<T> : Constraint<TileSamplingValidationContext<T>> where T : Tile
    {
        private readonly Dictionary<TileSide, Coordinate2D> _sideToCoordinateDelta;
        private readonly TileSide[] _tileSides = Enum.GetValues(typeof(TileSide)).Cast<TileSide>().ToArray();
        private readonly int _tileSidesCount;
        private readonly int _tileSidesHalfCount;

        public TileNeighbourConstraint()
        {
            _tileSidesCount = _tileSides.Length;
            _tileSidesHalfCount = _tileSidesCount / 2;
        }

        public bool IsValid(TileSamplingValidationContext<T> context)
        {
            Grid2D<T> grid = context.Grid;
            T currentElement = context.CurrentDomainElementToValidate;
            int x = context.CurrentSampleXCoordinate;
            int y = context.CurrentSampleYCoordinate;
            return IsTileValid(grid, currentElement, x, y);
        }

        private bool IsTileValid(Grid2D<T> grid, T currentElement, int x, int y)
        {
            foreach (TileSide side in _tileSides)
            {
                if (!IsValidNeighbour(grid, currentElement, side, x, y))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidNeighbour(Grid2D<T> grid, T currentElement, TileSide side, int x, int y)
        {
            bool hasTile = grid.TryGet(x + side.GetXDelta(), y + side.GetYDelta(), out T tile);
            return !hasTile || IsValidSocket(tile, side, currentElement);
        }

        private bool IsValidSocket(T tile, TileSide side, T currentElement)
        {
            int otherSocketId = GetSocketIdForOtherSide(tile, side);
            return otherSocketId < 0 || otherSocketId == currentElement.GetSocketIdFor(side);
        }

        private int GetSocketIdForOtherSide(T tile, TileSide thisSide)
        {
            int otherSide = ((int)thisSide + _tileSidesHalfCount) % _tileSidesCount;
            return tile.GetSocketIdFor((TileSide)otherSide);
        }
    }
}
