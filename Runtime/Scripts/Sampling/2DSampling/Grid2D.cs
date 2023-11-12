using System;

namespace PCGToolkit.Sampling
{
    public class Grid2D<T>
    {
        private T[,] _tiles;
        
        public Grid2D(int height, int width)
        {
            if (width < 1 || height < 1)
            {
                throw new ArgumentException($"The {nameof(width)} and {nameof(height)} need to at least 1.");
            }
            
            _tiles = new T[height, width];
        }

        public Grid2D(int size) : this(size, size)
        {
            
        }

        public T this[int row, int column]
        {
            get => _tiles[row, column];
            set => _tiles[row, column] = value;
        }
    }
}
