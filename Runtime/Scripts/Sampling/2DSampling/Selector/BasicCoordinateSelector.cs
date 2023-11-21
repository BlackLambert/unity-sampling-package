using System;
using System.Collections.Generic;
using System.Linq;

namespace PCGToolkit.Sampling
{
    public class BasicCoordinateSelector : Selector<Coordinate2D>
    {
        private List<Coordinate2D> _coordinates = new List<Coordinate2D>();
        private int _index = 0;
        private int _count = 0;

        public void Init(ICollection<Coordinate2D> items)
        {
            _index = 0;
            _coordinates = items.ToList();
            _count = _coordinates.Count;
        }

        public bool HasNext()
        {
            return _index < _count;
        }

        public Coordinate2D GetNext()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("There is not item left to get.");
            }

            Coordinate2D result = _coordinates[_index];
            _index++;
            return result;
        }
    }
}