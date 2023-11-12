namespace PCGToolkit.Sampling
{
    public abstract class ConstraintDoubleOperator<T> : Constraint<T>
    {
        protected Constraint<T> _first;
        protected Constraint<T> _second;
        
        public ConstraintDoubleOperator(Constraint<T> first, Constraint<T> second)
        {
            _first = first;
            _second = second;
        }

        public abstract bool IsValid(T samplingStep);
    }
}
