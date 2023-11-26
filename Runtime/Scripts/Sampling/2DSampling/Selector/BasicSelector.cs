using System;
using System.Collections.Generic;
using System.Linq;

namespace PCGToolkit.Sampling
{
    public class BasicSelector<TItem> : Selector<TItem>
    {
        private List<TItem> _coordinates = new List<TItem>();
        private int _index = 0;
        private int _count = 0;

        public void Init(ICollection<TItem> items)
        {
            _index = 0;
            _coordinates = items.ToList();
            _count = _coordinates.Count;
        }

        public bool HasNext()
        {
            return _index < _count;
        }

        public TItem GetNext()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("There is not item left to get.");
            }

            TItem result = _coordinates[_index];
            _index++;
            return result;
        }
    }
}