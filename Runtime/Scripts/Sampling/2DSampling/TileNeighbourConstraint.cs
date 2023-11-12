namespace PCGToolkit.Sampling
{
    public class TileNeighbourConstraint<T> : Constraint<TileSamplingValidationContext<T>> where T : Tile
    {
        public bool IsValid(TileSamplingValidationContext<T> context)
        {
            T currentElement = context.CurrentDomainElementToValidate;
            int x = context.CurrentSampleXCoordinate;
            int y = context.CurrentSampleYCoordinate;

            T leftTile = x > 0 ? context.Grid[x - 1, y] : default;
            T rightTile = x < context.Grid.Width - 1 ? context.Grid[x + 1, y] : default;
            T bottomTile = y > 0 ? context.Grid[x, y - 1] : default;
            T topTile = y < context.Grid.Height - 1 ? context.Grid[x, y + 1] : default;

            bool hasLeftTile = leftTile != null && !leftTile.Equals(default);
            bool hasRightTile = rightTile != null && !rightTile.Equals(default);
            bool hasTopTile = topTile != null && !topTile.Equals(default);
            bool hasBottomTile = bottomTile != null && !bottomTile.Equals(default);

            return (!hasLeftTile || leftTile.RightSocketId == currentElement.LeftSocketId) && 
                   (!hasRightTile || rightTile.LeftSocketId == currentElement.RightSocketId) && 
                   (!hasTopTile || topTile.BottomSocketId == currentElement.TopSocketId) && 
                   (!hasBottomTile || bottomTile.TopSocketId == currentElement.BottomSocketId);
        }
    }
}
