

namespace PCGToolkit.Sampling.Examples
{
    public class SizeConstraint : Constraint<Enemy>
    {
        private readonly Enemy.ESize _size;
        
        public SizeConstraint(Enemy.ESize size)
        {
            _size = size;
        }

        public bool IsValid(Enemy samplingStep)
        {
            return _size.HasFlag(samplingStep.Size);
        }
    }
}
