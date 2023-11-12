namespace PCGToolkit.Sampling
{
    public interface Tile : Weighted
    {
        public int TopSocketId { get; }
        public int RightSocketId { get; }
        public int BottomSocketId { get; }
        public int LeftSocketId { get; }
    }
}