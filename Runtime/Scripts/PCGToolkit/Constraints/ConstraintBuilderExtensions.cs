namespace PCG.Toolkit
{
    public static class ConstraintBuilderExtensions
    {
        public static SetConstraint<T> Not<T>(this Constraint<T> constraint)
        {
            return new ConstraintNotOperator<T>(constraint);
        }
        
        public static SetConstraint<T> Not<T>(this SetConstraint<T> constraint)
        {
            return new ConstraintNotOperator<T>(constraint);
        }

        public static SetConstraint<T> And<T>(this Constraint<T> constraint, Constraint<T> other)
        {
            return new ConstraintAndOperator<T>(constraint, other);
        }

        public static SetConstraint<T> And<T>(this Constraint<T> constraint, SetConstraint<T> other)
        {
            return new ConstraintAndOperator<T>(constraint, other);
        }

        public static SetConstraint<T> And<T>(this SetConstraint<T> constraint, Constraint<T> other)
        {
            return new ConstraintAndOperator<T>(constraint, other);
        }

        public static SetConstraint<T> And<T>(this SetConstraint<T> constraint, SetConstraint<T> other)
        {
            return new ConstraintAndOperator<T>(constraint, other);
        }

        public static SetConstraint<T> Or<T>(this Constraint<T> constraint, Constraint<T> other)
        {
            return new ConstraintOrOperator<T>(constraint, other);
        }

        public static SetConstraint<T> Or<T>(this Constraint<T> constraint, SetConstraint<T> other)
        {
            return new ConstraintOrOperator<T>(constraint, other);
        }

        public static SetConstraint<T> Or<T>(this SetConstraint<T> constraint, Constraint<T> other)
        {
            return new ConstraintOrOperator<T>(constraint, other);
        }

        public static SetConstraint<T> Or<T>(this SetConstraint<T> constraint, SetConstraint<T> other)
        {
            return new ConstraintOrOperator<T>(constraint, other);
        }

        public static SetConstraint<T> XOr<T>(this Constraint<T> constraint, Constraint<T> other)
        {
            return new ConstraintExclusiveOrOperator<T>(constraint, other);
        }

        public static SetConstraint<T> XOr<T>(this Constraint<T> constraint, SetConstraint<T> other)
        {
            return new ConstraintExclusiveOrOperator<T>(constraint, other);
        }

        public static SetConstraint<T> XOr<T>(this SetConstraint<T> constraint, Constraint<T> other)
        {
            return new ConstraintExclusiveOrOperator<T>(constraint, other);
        }

        public static SetConstraint<T> XOr<T>(this SetConstraint<T> constraint, SetConstraint<T> other)
        {
            return new ConstraintExclusiveOrOperator<T>(constraint, other);
        }
    }
}
