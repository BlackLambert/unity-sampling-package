namespace PCGToolkit.Sampling
{
    public interface TileSamplingValidationContext<T> where T : Tile
    {
        Grid2D<T> Grid { get; set; }
        Coordinate2D CurrentSampleCoordinate { get; set; }
        T CurrentDomainElementToValidate { get; set; }
    }
}