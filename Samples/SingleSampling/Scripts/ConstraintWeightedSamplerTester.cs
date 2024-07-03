using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace SBaier.Sampling.Examples
{
    public class ConstraintWeightedSamplerTester : MonoBehaviour
    {
        [SerializeField] private Enemies _enemies;
        [SerializeField] private Enemy.ESize _validSizes = Enemy.ESize.None;
        [SerializeField] private int _maxPower = -1;
        [SerializeField] private int _sampleAmount = 10;
        
        private WeightedList<Enemy> _weightedEnemies;
        private WeightedSampler<Enemy> _weightedSampler;
        private ConstraintSampler<Enemy> _constraintSampler;

        public void Sample()
        {
            PowerConstraint powerConstraint = new PowerConstraint(_maxPower);
            SizeConstraint sizeConstraint = new SizeConstraint(_validSizes);
            Constraint<Enemy> constraint = powerConstraint.And(sizeConstraint);

            _weightedSampler = new WeightedSampler<Enemy>(new Random());
            _constraintSampler = new ConstraintSampler<Enemy>(_weightedSampler, constraint);
            _constraintSampler.UpdateDomain(_enemies.List);

            List<Enemy> enemies = _constraintSampler.Sample(_sampleAmount);
            foreach (Enemy enemy in enemies)
            {
                Debug.Log($"ChosenEnemy {enemy}");
            }
        }
    }
}
