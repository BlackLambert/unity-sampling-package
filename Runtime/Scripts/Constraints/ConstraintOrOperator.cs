namespace PCGToolkit.Sampling
{
    public class ConstraintOrOperator<T> : ConstraintDoubleOperator<T>
    {
        public ConstraintOrOperator(Constraint<T> first, Constraint<T> second) : base(first, second) {}
        public ConstraintOrOperator(SetConstraint<T> first, Constraint<T> second) : base(first, second) {}
        public ConstraintOrOperator(Constraint<T> first, SetConstraint<T> second) : base(first, second) {}
        public ConstraintOrOperator(SetConstraint<T> first, SetConstraint<T> second) : base(first, second) {}
        
        public override bool IsValid(T item)
        {
            return _first.IsValid(item) || _second.IsValid(item);
        }
    }
}
