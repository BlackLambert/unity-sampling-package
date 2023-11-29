using System;
using System.Collections.Generic;
using System.Linq;

namespace PCGToolkit.Sampling
{
    public class RandomSelector<TItem> : Selector<TItem>
    {
        private List<TItem> _coordinates = new List<TItem>();
        private List<int> _randomIndices = new List<int>();
        private Random _random;
        private int _index = 0;

        public RandomSelector(Random random)
        {
            _random = random;
        }

        public void Init(ICollection<TItem> items)
        {
            _coordinates = items.ToList();
            _randomIndices = CreateRandomIndices(items.Count);
            _index = 0;
        }

        public bool HasNext()
        {
            return _index < _coordinates.Count;
        }

        public TItem GetNext()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("There is not item left to get.");
            }

            TItem result = _coordinates[_randomIndices[_index]];
            _index++;
            return result;
        }

        private List<int> CreateRandomIndices(int itemsCount)
        {
            List<int> result = new List<int>(itemsCount);
            for (int i = 0; i < itemsCount; i++)
            {
                result.Add(i);
            }
            
            //Source: https://stackoverflow.com/questions/273313/randomize-a-listt
            int n = itemsCount;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                (result[k], result[n]) = (result[n], result[k]);
            }

            return result;
        }
    }
}