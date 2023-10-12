

namespace PCGToolkit.Sampling.Examples
{
    public class PowerMultiConstraint : SetConstraint<Enemy>
    {
        private readonly int _maxPower;
        private int _powerLeft;
        
        public PowerMultiConstraint(int maxPower)
        {
            _maxPower = maxPower;
        }
        
        public bool IsValid(Enemy item)
        {
            return item.Power <= _powerLeft;
        }

        public void AddResultSample(Enemy sample)
        {
            _powerLeft -= sample.Power;
        }

        public void ClearResultSample()
        {
            _powerLeft = _maxPower;
        }
    }
}
