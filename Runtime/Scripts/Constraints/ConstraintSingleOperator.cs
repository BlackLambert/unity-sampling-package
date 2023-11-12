namespace PCGToolkit.Sampling
{
    public abstract class ConstraintSingleOperator<T> : Constraint<T>
    {
        protected Constraint<T> _constraint;
        
        public ConstraintSingleOperator(Constraint<T> constraint)
        {
            _constraint = constraint;
        }

        public abstract bool IsValid(T samplingStep);
    }
}
