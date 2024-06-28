namespace PCGToolkit.Sampling
{
    public interface Tile : Weighted
    {
        public int GetSocketIdFor(int tileSideId);
    }
}