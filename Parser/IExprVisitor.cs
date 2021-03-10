using System.Text;

namespace Parser
{
    public interface IExprVisitor
    {
        void Visit(Literal expr);
        void Visit(Variable expr);
        void Visit(BinaryExpr expr);
        void Visit(ParenExpr expr);
    }

    public class DumpVisitor : IExprVisitor
    {
        private readonly StringBuilder _builder;

        public DumpVisitor()
        {
            _builder = new StringBuilder();
        }

        public void Visit(Literal expr)
        {
            _builder.Append("Literal(" + expr.Value + ")");
        }

        public void Visit(Variable expr)
        {
            _builder.Append("Variable(" + expr.Name + ")");
        }

        public void Visit(BinaryExpr expr)
        {
            _builder.Append("BinaryExpr(");
            expr.Lhs.Accept(this);
            _builder.Append(expr.Operator);
            expr.Rhs.Accept(this);
            _builder.Append(")");
        }

        public void Visit(ParenExpr expr)
        {
            _builder.Append("ParenExpr(");
            expr.Inner.Accept(this);
            _builder.Append(")");
        }

        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}