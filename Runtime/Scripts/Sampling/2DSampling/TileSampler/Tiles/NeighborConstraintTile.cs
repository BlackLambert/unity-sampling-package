namespace SBaier.Sampling
{
    public interface NeighborConstraintTile : Tile
    {
        int GetSocketIdFor(int tileSideId);
    }
}