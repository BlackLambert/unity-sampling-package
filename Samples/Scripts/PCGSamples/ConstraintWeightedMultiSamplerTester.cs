using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace PCGToolkit.Sampling.Examples
{
    public class ConstraintWeightedMultiSamplerTester : MonoBehaviour
    {
        [SerializeField] private Enemies _enemies;
        [SerializeField] private List<SizeToAmount> _validSizes = new List<SizeToAmount>();
        [SerializeField] private int _maxPower = -1;
        [SerializeField] private int _sampleMaxAmount = 10;
        
        private WeightedSingleSampler<Enemy> _weightedSingleSampler;
        private ConstraintSetSampler<Enemy, EnemySetSamplingValidationContext> _constraintSetSampler;

        public void Sample()
        {
            _weightedSingleSampler = new WeightedSingleSampler<Enemy>(new Random());
            Constraint<EnemySetSamplingValidationContext> constraint = new EnemyPowerSetConstraint(_maxPower).
                And(new EnemySizeSetConstraint(CreateSizeToAmount()));
            _constraintSetSampler = new ConstraintSetSampler<Enemy, EnemySetSamplingValidationContext>(
                _weightedSingleSampler, constraint, _sampleMaxAmount);
            _constraintSetSampler.UpdateDomain(_enemies.List);

            List<Enemy> enemies = _constraintSetSampler.Sample();
            foreach (Enemy enemy in enemies)
            {
                Debug.Log($"ChosenEnemy {enemy}");
            }
            
            Debug.Log($"Power Sum: {enemies.Sum(e => e.Power)}");
        }

        private Dictionary<Enemy.ESize, int> CreateSizeToAmount()
        {
            Dictionary<Enemy.ESize, int> sizeToAmount = new Dictionary<Enemy.ESize, int>();
            
            foreach (SizeToAmount pair in _validSizes)
            {
                sizeToAmount.Add(pair.Size, pair.Amount);
            }

            return sizeToAmount;
        }

        [Serializable]
        public class SizeToAmount
        {
            [field: SerializeField] public Enemy.ESize Size;
            [field: SerializeField] public int Amount;
        }
    }
}
