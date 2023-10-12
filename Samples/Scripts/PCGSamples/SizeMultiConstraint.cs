using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling.Examples
{
    public class SizeMultiConstraint : SetConstraint<Enemy>
    {
        private Dictionary<Enemy.ESize, int> _sizeToMaxAmount;
        private Dictionary<Enemy.ESize, int> _currentSizeToAmount = new Dictionary<Enemy.ESize, int>();

        public SizeMultiConstraint(Dictionary<Enemy.ESize, int> sizeToMaxAmount)
        {
            _sizeToMaxAmount = sizeToMaxAmount;
        }

        public bool IsValid(Enemy item)
        {
            return !_sizeToMaxAmount.ContainsKey(item.Size) ||
                   _currentSizeToAmount[item.Size] < _sizeToMaxAmount[item.Size];
        }

        public void AddResultSample(Enemy sample)
        {
            _currentSizeToAmount[sample.Size] += 1;
        }

        public void ClearResultSample()
        {
            foreach (Enemy.ESize size in Enum.GetValues(typeof(Enemy.ESize)))
            {
                _currentSizeToAmount[size] = 0;
            }
        }
    }
}
