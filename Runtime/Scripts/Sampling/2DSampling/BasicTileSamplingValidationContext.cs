namespace PCGToolkit.Sampling
{
    public class BasicTileSamplingValidationContext<T> : TileSamplingValidationContext<T> where T : Tile
    {
        public Grid2D<T> Grid { get; set; }
        public int CurrentSampleXCoordinate { get; set; }
        public int CurrentSampleYCoordinate { get; set; }
        public T CurrentDomainElementToValidate { get; set; }
    }
}