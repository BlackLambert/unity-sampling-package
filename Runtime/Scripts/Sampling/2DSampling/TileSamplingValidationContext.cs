namespace PCGToolkit.Sampling
{
    public interface TileSamplingValidationContext<T> where T : Tile
    {
        Grid2D<T> Grid { get; set; }
        int CurrentSampleXCoordinate { get; set; }
        int CurrentSampleYCoordinate { get; set; }
        T CurrentDomainElementToValidate { get; set; }
    }
}