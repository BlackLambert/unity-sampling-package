namespace PCGToolkit.Sampling
{
    public abstract class ConstraintSingleOperator<T> : SetConstraint<T>
    {
        protected Constraint<T> _constraint;
        private SetConstraint<T> _setConstraint;
        private bool _isMultiConstraint;
        
        public ConstraintSingleOperator(Constraint<T> constraint)
        {
            _constraint = constraint;
        }
        
        public ConstraintSingleOperator(SetConstraint<T> constraint)
        {
            _constraint = constraint;
            _setConstraint = constraint;
            _isMultiConstraint = true;
        }

        public abstract bool IsValid(T item);

        public void AddResultSample(T sample)
        {
            if (_isMultiConstraint)
            {
                _setConstraint?.AddResultSample(sample);
            }
        }

        public void ClearResultSample()
        {
            if (_isMultiConstraint)
            {
                _setConstraint?.ClearResultSample();
            }
        }
    }
}
