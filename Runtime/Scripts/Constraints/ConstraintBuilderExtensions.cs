namespace PCGToolkit.Sampling
{
    public static class ConstraintBuilderExtensions
    {
        public static Constraint<T> Not<T>(this Constraint<T> constraint)
        {
            return new ConstraintNotOperator<T>(constraint);
        }

        public static Constraint<T> And<T>(this Constraint<T> constraint, Constraint<T> other)
        {
            return new ConstraintAndOperator<T>(constraint, other);
        }

        public static Constraint<T> Or<T>(this Constraint<T> constraint, Constraint<T> other)
        {
            return new ConstraintOrOperator<T>(constraint, other);
        }

        public static Constraint<T> XOr<T>(this Constraint<T> constraint, Constraint<T> other)
        {
            return new ConstraintExclusiveOrOperator<T>(constraint, other);
        }
    }
}
