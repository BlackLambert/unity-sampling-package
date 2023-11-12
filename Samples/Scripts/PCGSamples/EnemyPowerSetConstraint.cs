namespace PCGToolkit.Sampling.Examples
{
    public class EnemyPowerSetConstraint : Constraint<EnemySetSamplingValidationContext>
    {
        private readonly int _maxPower;
        
        public EnemyPowerSetConstraint(int maxPower)
        {
            _maxPower = maxPower;
        }
        
        public bool IsValid(EnemySetSamplingValidationContext samplingValidationContext)
        {
            return samplingValidationContext.CurrentDomainElementToValidate.Power <= _maxPower - samplingValidationContext.CombinedPower;
        }
    }
}
