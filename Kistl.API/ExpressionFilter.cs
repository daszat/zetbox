using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Kistl.API
{
    [Serializable]
    public class IllegalExpression
    {
        public string MethodCall { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(MethodCall))
            {
                return "Illegal Method Call " + MethodCall;
            }
            else
            {
                throw new NotImplementedException("IllegalExpression.ToString() has a unkown Expression");
            }
        }
    }

    public static class ExpressionFilter
    {
        public static bool IsLegal(this Expression e)
        {
            CheckLegalExpression chk = new CheckLegalExpression();
            chk.Visit(e);
            return chk.IsLegal;
        }

        public static bool IsLegal(this Expression e, out List<IllegalExpression> list)
        {
            CheckLegalExpression chk = new CheckLegalExpression();
            chk.Visit(e);
            list = chk.IllegalExpression;
            return chk.IsLegal;
        }
        
        private class CheckLegalExpression : ExpressionTreeVisitor
        {
            public CheckLegalExpression()
            {
                IsLegal = true;
                IllegalExpression = new List<IllegalExpression>();
            }

            public bool IsLegal { get; private set; }
            public List<IllegalExpression> IllegalExpression { get; private set; }

            protected override void VisitMethodCall(MethodCallExpression m)
            {
                base.VisitMethodCall(m);

                if (m.Method.DeclaringType != typeof(string))
                {
                    IsLegal = false;
                    IllegalExpression.Add(new IllegalExpression() { MethodCall = m.Method.DeclaringType.FullName + "." + m.Method.Name });
                }
            }
        }
    }
}