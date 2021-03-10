namespace Parser
{
    public interface IExpr
    {
        void Accept(IExprVisitor visitor);
    }

    public class Literal : IExpr
    {
        public Literal(string value)
        {
            Value = value;
        }
        
        public void Accept(IExprVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string Value { get; }
    }

    public class Variable : IExpr
    {
        public Variable(string name)
        {
            Name = name;
        }

        public void Accept(IExprVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string Name { get; }
    }

    public class BinaryExpr : IExpr
    {
        public BinaryExpr(IExpr lhs, IExpr rhs, string @operator)
        {
            Lhs = lhs;
            Rhs = rhs;
            Operator = @operator;
        }

        public void Accept(IExprVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IExpr Lhs { get; }
        public IExpr Rhs { get; }
        public string Operator { get; }
    }

    public class ParenExpr : IExpr
    {
        public ParenExpr(IExpr inner)
        {
            Inner = inner;
        }

        public void Accept(IExprVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IExpr Inner { get;  }
    }
}