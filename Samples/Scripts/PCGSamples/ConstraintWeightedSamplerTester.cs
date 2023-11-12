using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace PCGToolkit.Sampling.Examples
{
    public class ConstraintWeightedSamplerTester : MonoBehaviour
    {
        [SerializeField] private Enemies _enemies;
        [SerializeField] private Enemy.ESize _validSizes = Enemy.ESize.None;
        [SerializeField] private int _maxPower = -1;
        [SerializeField] private int _sampleAmount = 10;
        
        private WeightedList<Enemy> _weightedEnemies;
        private WeightedSingleSampler<Enemy> _weightedSingleSampler;
        private ConstraintSingleSampler<Enemy> _constraintSingleSampler;

        public void Sample()
        {
            PowerConstraint powerConstraint = new PowerConstraint(_maxPower);
            SizeConstraint sizeConstraint = new SizeConstraint(_validSizes);
            Constraint<Enemy> constraint = powerConstraint.And(sizeConstraint);

            _weightedSingleSampler = new WeightedSingleSampler<Enemy>(new Random());
            _constraintSingleSampler = new ConstraintSingleSampler<Enemy>(_weightedSingleSampler, constraint);
            _constraintSingleSampler.UpdateDomain(_enemies.List);

            List<Enemy> enemies = _constraintSingleSampler.Sample(_sampleAmount);
            foreach (Enemy enemy in enemies)
            {
                Debug.Log($"ChosenEnemy {enemy}");
            }
        }
    }
}
