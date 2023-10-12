namespace PCGToolkit.Sampling
{
    public abstract class ConstraintDoubleOperator<T> : SetConstraint<T>
    {
        protected Constraint<T> _first;
        protected Constraint<T> _second;
        private SetConstraint<T> _multiSampleFirst;
        private SetConstraint<T> _multiSampleSecond;
        private bool _firstIsMultiConstraint;
        private bool _secondIsMultiConstraint;
        
        public ConstraintDoubleOperator(Constraint<T> first, Constraint<T> second)
        {
            _first = first;
            _second = second;
        }
        
        public ConstraintDoubleOperator(SetConstraint<T> first, Constraint<T> second)
        {
            _first = first;
            _second = second;
            _multiSampleFirst = first;
            _firstIsMultiConstraint = true;
        }
        
        public ConstraintDoubleOperator(Constraint<T> first, SetConstraint<T> second)
        {
            _first = first;
            _second = second;
            _multiSampleSecond = second;
            _secondIsMultiConstraint = true;
        }
        
        public ConstraintDoubleOperator(SetConstraint<T> first, SetConstraint<T> second)
        {
            _first = first;
            _second = second;
            _multiSampleFirst = first;
            _multiSampleSecond = second;
            _firstIsMultiConstraint = true;
            _secondIsMultiConstraint = true;
        }

        public abstract bool IsValid(T item);

        public void AddResultSample(T sample)
        {
            if (_firstIsMultiConstraint)
            {
                _multiSampleFirst?.AddResultSample(sample);
            }

            if (_secondIsMultiConstraint)
            {
                _multiSampleSecond?.AddResultSample(sample);
            }
        }

        public void ClearResultSample()
        {
            if (_firstIsMultiConstraint)
            {
                _multiSampleFirst?.ClearResultSample();
            }

            if (_secondIsMultiConstraint)
            {
                _multiSampleSecond?.ClearResultSample();
            }
        }
    }
}
