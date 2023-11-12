namespace PCGToolkit.Sampling
{
    public class ConstraintNotOperator<T> : ConstraintSingleOperator<T>
    {
        public ConstraintNotOperator(Constraint<T> constraint) : base(constraint) {}

        public override bool IsValid(T samplingStep)
        {
            return !_constraint.IsValid(samplingStep);
        }
    }
}
