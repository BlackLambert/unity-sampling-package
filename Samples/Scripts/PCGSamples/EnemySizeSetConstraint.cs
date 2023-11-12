using System.Collections.Generic;

namespace PCGToolkit.Sampling.Examples
{
    public class EnemySizeSetConstraint : Constraint<EnemySetSamplingValidationContext>
    {
        private Dictionary<Enemy.ESize, int> _sizeToMaxAmount;

        public EnemySizeSetConstraint(Dictionary<Enemy.ESize, int> sizeToMaxAmount)
        {
            _sizeToMaxAmount = sizeToMaxAmount;
        }

        public bool IsValid(EnemySetSamplingValidationContext samplingValidationContext)
        {
            Enemy.ESize size = samplingValidationContext.CurrentDomainElementToValidate.Size;
            return !_sizeToMaxAmount.ContainsKey(size) ||
                   samplingValidationContext.CurrentSizeToAmount[size] < _sizeToMaxAmount[size];
        }
    }
}
