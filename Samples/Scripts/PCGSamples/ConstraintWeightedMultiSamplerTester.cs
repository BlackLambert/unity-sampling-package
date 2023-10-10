using System;
using System.Collections.Generic;
using System.Linq;
using PCG.Toolkit;
using UnityEngine;
using Random = System.Random;

namespace PCG.Tests
{
    public class ConstraintWeightedMultiSamplerTester : MonoBehaviour
    {
        [SerializeField] private Enemies _enemies;
        [SerializeField] private List<SizeToAmount> _validSizes = new List<SizeToAmount>();
        [SerializeField] private int _maxPower = -1;
        [SerializeField] private int _sampleMaxAmount = 10;
        
        private WeightedSingleSampler<Enemy> _weightedSingleSampler;
        private ConstraintMultiSampler<Enemy> _constraintMultiSampler;

        public void Sample()
        {
            _weightedSingleSampler = new WeightedSingleSampler<Enemy>(new Random());
            SetConstraint<Enemy> constraint = new PowerMultiConstraint(_maxPower).And(new SizeMultiConstraint(CreateSizeToAmount()));
            _constraintMultiSampler = new ConstraintMultiSampler<Enemy>(_weightedSingleSampler, constraint, _sampleMaxAmount);
            _constraintMultiSampler.UpdateSamples(_enemies.List);

            List<Enemy> enemies = _constraintMultiSampler.Sample();
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
