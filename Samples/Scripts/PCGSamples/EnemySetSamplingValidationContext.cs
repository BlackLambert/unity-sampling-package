using System;
using System.Collections.Generic;

namespace PCGToolkit.Sampling.Examples
{
    public class EnemySetSamplingValidationContext : SetSamplingValidationContext<Enemy>
    {
        public IReadOnlyDictionary<Enemy.ESize, int> CurrentSizeToAmount => _currentSizeToAmount;
        public Enemy CurrentDomainElementToValidate { get; set; }
        public int CombinedPower { get; private set; }
        
        
        private readonly Dictionary<Enemy.ESize, int> _currentSizeToAmount = new Dictionary<Enemy.ESize, int>();

        public void AddSample(Enemy sample)
        {
            CombinedPower += sample.Power;
            _currentSizeToAmount[sample.Size] += 1;
        }

        public void Reset()
        {
            CombinedPower = 0;
            
            foreach (Enemy.ESize size in Enum.GetValues(typeof(Enemy.ESize)))
            {
                _currentSizeToAmount[size] = 0;
            }
        }
    }
}