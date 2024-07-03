namespace SBaier.Sampling
{
    public interface TileInfo
    {
        int NeighboursAmount { get; }
        Coordinate2D GetNeighborCoordinate(int tileSideId, Coordinate2D coordinate);

        public Coordinate2D[] GetNeighborCoordinates(Coordinate2D coordinate)
        {
            Coordinate2D[] neighbors = new Coordinate2D[NeighboursAmount];
            
            for (int i = 0; i < NeighboursAmount; i++)
            {
                neighbors[i] = GetNeighborCoordinate(i, coordinate);
            }

            return neighbors;
        }
    }
}