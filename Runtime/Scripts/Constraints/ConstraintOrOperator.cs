namespace PCGToolkit.Sampling
{
    public class ConstraintOrOperator<T> : ConstraintDoubleOperator<T>
    {
        public ConstraintOrOperator(Constraint<T> first, Constraint<T> second) : base(first, second) {}
        
        public override bool IsValid(T samplingStep)
        {
            return _first.IsValid(samplingStep) || _second.IsValid(samplingStep);
        }
    }
}
