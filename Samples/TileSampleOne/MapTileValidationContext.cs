namespace PCGToolkit.Sampling.Examples.TileSampleOne
{
    public class MapTileValidationContext : TileSamplingValidationContext<MapTileSettings>
    {
        public Grid2D<MapTileSettings> Grid { get; set; }
        public int CurrentSampleXCoordinate { get; set; }
        public int CurrentSampleYCoordinate { get; set; }
        public MapTileSettings CurrentDomainElementToValidate { get; set; }
    }
}