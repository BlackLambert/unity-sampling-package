namespace PCGToolkit.Sampling
{
    public class BasicTileSamplingValidationContext<T> : TileSamplingValidationContext<T> where T : Tile
    {
        public Grid2D<T> Grid { get; set; }
        public Coordinate2D CurrentSampleCoordinate { get; set; }
        public T CurrentDomainElementToValidate { get; set; }
    }
}