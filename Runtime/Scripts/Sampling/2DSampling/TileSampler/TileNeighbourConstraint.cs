namespace SBaier.Sampling
{
    public class TileNeighbourConstraint<T> : Constraint<TileSamplingContext<T>> where T : NeighborConstraintTile
    {
        public bool IsValid(TileSamplingContext<T> context)
        {
            T currentElement = context.CurrentDomainElementToValidate;
            return IsTileValid(context, currentElement, context.CurrentSampleCoordinate);
        }

        private bool IsTileValid(TileSamplingContext<T> context, T currentElement, Coordinate2D currentSampleCoordinate)
        {
            for (int sideIndex = 0; sideIndex < context.TileInfo.NeighboursAmount; sideIndex++)
            {
                if (!IsValidNeighbour(context, currentElement, sideIndex, currentSampleCoordinate))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidNeighbour(TileSamplingContext<T> context, T currentElement, int sideIndex, Coordinate2D currentSampleCoordinate)
        {
            Coordinate2D neighborCoordinate = context.TileInfo.GetNeighborCoordinate(sideIndex, currentSampleCoordinate);
            bool hasTile = context.Grid.TryGet(neighborCoordinate.X, neighborCoordinate.Y, out T tile);
            return !hasTile || IsValidSocket(tile, sideIndex, currentElement, context.TileInfo.NeighboursAmount);
        }

        private bool IsValidSocket(T tile, int sideIndex, T currentElement, int amount)
        {
            int otherSocketId = GetSocketIdForOtherSide(tile, sideIndex, amount);
            return otherSocketId < 0 || otherSocketId == currentElement.GetSocketIdFor(sideIndex);
        }

        private int GetSocketIdForOtherSide(T tile, int sideIndex, int amount)
        {
            int otherSide = (sideIndex + amount/2) % amount;
            return tile.GetSocketIdFor(otherSide);
        }
    }
}
