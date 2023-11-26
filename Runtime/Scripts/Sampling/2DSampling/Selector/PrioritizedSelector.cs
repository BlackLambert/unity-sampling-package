using System;
using System.Collections.Generic;
using System.Linq;

namespace PCGToolkit.Sampling
{
    public class PrioritizedSelector<TItem> : Selector<TItem>
    {
        private readonly int _startPriority;
        private List<KeyValuePair<TItem, int>> _priorityList = new List<KeyValuePair<TItem, int>>();
        private IComparer<KeyValuePair<TItem, int>> _comparer = new PriorityComparer();

        public PrioritizedSelector(int startPriority = 0)
        {
            _startPriority = startPriority;
        }
        
        public void Init(ICollection<TItem> items)
        {
            _priorityList = new List<KeyValuePair<TItem, int>>(items.Count);
            foreach (TItem item in items)
            {
                _priorityList.Add(new KeyValuePair<TItem, int>(item, _startPriority));
            }

            Reorder();
        }

        private void Reorder()
        {
            _priorityList.Sort(_comparer);
            _priorityList.Reverse();
        }

        public bool HasNext()
        {
            return _priorityList.Count > 0;
        }

        public TItem GetNext()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("There is not item left to get.");
            }
            
            KeyValuePair<TItem, int> firstPair = _priorityList.First();
            _priorityList.RemoveAt(0);
            return firstPair.Key;
        }

        private class PriorityComparer : IComparer<KeyValuePair<TItem, int>>
        {
            public int Compare(KeyValuePair<TItem, int> x, KeyValuePair<TItem, int> y)
            {
                return x.Value.CompareTo(y.Value);
            }
        }
    }
}