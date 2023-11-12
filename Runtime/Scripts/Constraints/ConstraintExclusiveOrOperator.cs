namespace PCGToolkit.Sampling
{
    public class ConstraintExclusiveOrOperator<T> : ConstraintDoubleOperator<T>
    {
        public ConstraintExclusiveOrOperator(Constraint<T> first, Constraint<T> second) : base(first, second) {}
        
        public override bool IsValid(T samplingStep)
        {
            return _first.IsValid(samplingStep) != _second.IsValid(samplingStep);
        }
    }
}
