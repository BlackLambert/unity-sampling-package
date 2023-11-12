using System;

namespace PCGToolkit.Sampling
{
    public class Grid2D<T>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private T[,] _tiles;
        
        public Grid2D(int width, int height)
        {
            if (width < 1 || height < 1)
            {
                throw new ArgumentException($"The {nameof(width)} and {nameof(height)} need to at least 1.");
            }

            Width = width;
            Height = height;
            _tiles = new T[height, width];
        }

        public Grid2D(int size) : this(size, size)
        {
            
        }

        public T this[int column, int row]
        {
            get => _tiles[row, column];
            set => _tiles[row, column] = value;
        }
    }
}
