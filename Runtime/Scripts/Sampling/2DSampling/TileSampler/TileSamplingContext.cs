namespace SBaier.Sampling
{
    public interface TileSamplingContext<T> where T : Tile
    {
        Grid2D<T> Grid { get; }
        Coordinate2D CurrentSampleCoordinate { get; }
        T CurrentDomainElementToValidate { get; }
        TileInfo TileInfo { get; }
    }
}