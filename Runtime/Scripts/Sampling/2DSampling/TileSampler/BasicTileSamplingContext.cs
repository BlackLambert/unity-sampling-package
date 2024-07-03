namespace SBaier.Sampling
{
    public class BasicTileSamplingContext<T> : 
        TileSamplingContext<T> where T : Tile
    {
        public Grid2D<T> Grid { get; set; }
        public Coordinate2D CurrentSampleCoordinate { get; set; }
        public T CurrentDomainElementToValidate { get; set; }
        public TileInfo TileInfo { get; set; }
    }
}