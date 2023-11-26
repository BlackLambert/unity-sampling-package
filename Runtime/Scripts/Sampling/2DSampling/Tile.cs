namespace PCGToolkit.Sampling
{
    public interface Tile : Weighted
    {
        public int GetSocketIdFor(TileSide tileSide);
    }
}