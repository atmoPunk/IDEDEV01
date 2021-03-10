using System;
using System.Collections.Generic;
using System.Linq;

namespace Parser
{
    public class Parser
    {
        public Parser()
        {
            _exprStack = new List<IExpr>();
            _opStack = new List<char>();
        }
        
        public IExpr Parse(string text)
        {
            int curPos = 0;
            while (curPos < text.Length)
            {
                if (Char.IsLetter(text[curPos]))
                {
                    _exprStack.Add(new Variable(text[curPos].ToString()));
                }
                else if (Char.IsDigit(text[curPos]))
                {
                    _exprStack.Add(new Literal(text[curPos].ToString()));
                }
                else if (text[curPos] == '+' || text[curPos] == '-')
                {
                    while (_opStack.Count > 0 && _opStack.Last() != '(')
                    {
                        CreateBinaryExpr();
                    }
                    _opStack.Add(text[curPos]);
                }
                else if (text[curPos] == '*' || text[curPos] == '/')
                {
                    while (_opStack.Count > 0 && (_opStack.Last() == '*' || _opStack.Last() == '/') && _opStack.Last() != '(')
                    {
                        CreateBinaryExpr();
                    }
                    _opStack.Add(text[curPos]);
                }
                else if (text[curPos] == '(')
                {
                    _opStack.Add(text[curPos]);
                }
                else if (text[curPos] == ')')
                {
                    while (_opStack.Last() != '(')
                    {
                        CreateBinaryExpr();
                        if (_opStack.Count == 0)
                        {
                            throw new ArgumentException("Mismatching parenthesis");
                        }
                    }

                    _exprStack[^1] = new ParenExpr(_exprStack[^1]);
                    _opStack.RemoveAt(_opStack.Count - 1);
                }
                else
                {
                    throw new ArgumentException("Unknown symbol at pos " + curPos.ToString());
                }

                curPos++;
            }

            while (_opStack.Count > 0)
            {
                if (_opStack.Last() == '(')
                {
                    throw new ArgumentException("Mismatching parenthesis");
                }
                CreateBinaryExpr();
            }

            if (_exprStack.Count != 1)
            {
                throw new ArgumentException("Not enough operators");
            } 
            return _exprStack.Last();
        }

        private void CreateBinaryExpr()
        {
            if (_exprStack.Count < 2)
            {
                throw new ArgumentException("Not enough operands");
            }
            IExpr rhs = _exprStack.Last();
            _exprStack.RemoveAt(_exprStack.Count - 1);
            IExpr lhs = _exprStack.Last();
            _exprStack.RemoveAt(_exprStack.Count - 1);
            _exprStack.Add(new BinaryExpr(lhs, rhs, _opStack.Last().ToString()));
            _opStack.RemoveAt(_opStack.Count - 1);
        }

        private readonly List<IExpr> _exprStack;
        private readonly List<Char> _opStack;
    }
}