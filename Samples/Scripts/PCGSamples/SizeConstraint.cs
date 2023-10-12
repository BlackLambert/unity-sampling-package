

namespace PCGToolkit.Sampling.Examples
{
    public class SizeConstraint : Constraint<Enemy>
    {
        private readonly Enemy.ESize _size;
        
        public SizeConstraint(Enemy.ESize size)
        {
            _size = size;
        }

        public bool IsValid(Enemy item)
        {
            return _size.HasFlag(item.Size);
        }
    }
}
