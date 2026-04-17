public class AndPredicate : IPredicate
{
    private IPredicate[] predicates;

    public AndPredicate(params IPredicate[] predicates)
    {
        this.predicates = predicates;
    }

    public bool Evaluate()
    {
        foreach (var predicate in predicates)
        {
            if (!predicate.Evaluate())
            {
                return false;
            }
        }
        return true;
    }
}