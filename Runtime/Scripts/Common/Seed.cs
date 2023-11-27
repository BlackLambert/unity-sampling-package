using System;

namespace PCGToolkit
{
    public class Seed
    {
        public Random Random { get; private set; }
        public int SeedNumber { get; }

        public Seed(int seed)
        {
            Random = new Random(seed);
            SeedNumber = seed;
        }

        public void Reset()
        {
            Random = new Random(SeedNumber);
        }
    }
}