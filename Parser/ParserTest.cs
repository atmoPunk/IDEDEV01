using NUnit.Framework;

namespace Parser
{
    public class ParserTest
    {
        [Test]
        public void SumTest()
        {
            var visitor = new DumpVisitor();
            var parser = new Parser();
            string data = "1+2";
            var expr = parser.Parse(data);
            expr.Accept(visitor);
            Assert.AreEqual("BinaryExpr(Literal(1)+Literal(2))", visitor.ToString());
        }
        
        [Test]
        public void SumAndMultTest()
        {
            var visitor = new DumpVisitor();
            var parser = new Parser();
            string data = "1+2*3+4";
            var expr = parser.Parse(data);
            expr.Accept(visitor);
            Assert.AreEqual("BinaryExpr(BinaryExpr(Literal(1)+BinaryExpr(Literal(2)*Literal(3)))+Literal(4))", visitor.ToString());
        }

        [Test]
        public void OpsTest()
        {
            var visitor = new DumpVisitor();
            var parser = new Parser();
            string data = "1+a*3-b/5";
            var expr = parser.Parse(data);
            expr.Accept(visitor);
            Assert.AreEqual("BinaryExpr(BinaryExpr(Literal(1)+BinaryExpr(Variable(a)*Literal(3)))-BinaryExpr(Variable(b)/Literal(5)))", visitor.ToString());
        }

        [Test]
        public void SimpleParenTest()
        {
            var visitor = new DumpVisitor();
            var parser = new Parser();
            string data = "2*(a-3)";
            var expr = parser.Parse(data);
            expr.Accept(visitor);
            Assert.AreEqual("BinaryExpr(Literal(2)*ParenExpr(BinaryExpr(Variable(a)-Literal(3))))", visitor.ToString());
        }

        [Test]
        public void JustVarTest()
        {
            var visitor = new DumpVisitor();
            var parser = new Parser();
            string data = "a";
            var expr = parser.Parse(data);
            expr.Accept(visitor);
            Assert.AreEqual("Variable(a)", visitor.ToString());
        }

        [Test]
        public void MoreParenTest()
        {
            var visitor = new DumpVisitor();
            var parser = new Parser();
            string data = "(a-(3))";
            var expr = parser.Parse(data);
            expr.Accept(visitor);
            Assert.AreEqual("ParenExpr(BinaryExpr(Variable(a)-ParenExpr(Literal(3))))", visitor.ToString());
        }
    }
}