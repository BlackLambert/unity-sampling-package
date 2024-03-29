

namespace PCGToolkit.Sampling.Examples
{
    public class PowerConstraint : Constraint<Enemy>
    {
        private readonly int _maxPower;
        
        public PowerConstraint(int maxPower)
        {
            _maxPower = maxPower;
        }

        public bool IsValid(Enemy samplingStep)
        {
            return samplingStep.Power <= _maxPower;
        }
    }
}
