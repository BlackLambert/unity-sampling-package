namespace PCGToolkit.Sampling
{
    public class ConstraintNotOperator<T> : ConstraintSingleOperator<T>
    {
        public ConstraintNotOperator(Constraint<T> constraint) : base(constraint) {}
        public ConstraintNotOperator(SetConstraint<T> constraint) : base(constraint) {}

        public override bool IsValid(T item)
        {
            return !_constraint.IsValid(item);
        }
    }
}
