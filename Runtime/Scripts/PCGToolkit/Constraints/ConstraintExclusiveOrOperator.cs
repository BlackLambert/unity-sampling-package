namespace PCG.Toolkit
{
    public class ConstraintExclusiveOrOperator<T> : ConstraintDoubleOperator<T>
    {
        public ConstraintExclusiveOrOperator(Constraint<T> first, Constraint<T> second) : base(first, second) {}
        public ConstraintExclusiveOrOperator(SetConstraint<T> first, Constraint<T> second) : base(first, second) {}
        public ConstraintExclusiveOrOperator(Constraint<T> first, SetConstraint<T> second) : base(first, second) {}
        public ConstraintExclusiveOrOperator(SetConstraint<T> first, SetConstraint<T> second) : base(first, second) {}
        
        public override bool IsValid(T item)
        {
            return _first.IsValid(item) != _second.IsValid(item);
        }
    }
}
