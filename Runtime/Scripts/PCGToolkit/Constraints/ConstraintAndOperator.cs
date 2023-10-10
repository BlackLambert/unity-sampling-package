namespace PCG.Toolkit
{
    public class ConstraintAndOperator<T> : ConstraintDoubleOperator<T>
    {
        public ConstraintAndOperator(Constraint<T> first, Constraint<T> second) : base(first, second) {}
        public ConstraintAndOperator(SetConstraint<T> first, Constraint<T> second) : base(first, second) {}
        public ConstraintAndOperator(Constraint<T> first, SetConstraint<T> second) : base(first, second) {}
        public ConstraintAndOperator(SetConstraint<T> first, SetConstraint<T> second) : base(first, second) {}

        public override bool IsValid(T item)
        {
            return _first.IsValid(item) && _second.IsValid(item);
        }
    }
}
