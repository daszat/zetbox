using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Kistl.API.Client.LinqToKistl
{
    internal class QueryTranslator : ExpressionTreeTranslator
    {
        public static Expression Translate(Expression e)
        {
            var t = new QueryTranslator();
            return t.Visit(e);
        }

        protected override BinaryExpression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.Equal && typeof(IDataObject).IsAssignableFrom(b.Left.Type) && typeof(IDataObject).IsAssignableFrom(b.Right.Type))
            {
                return Expression.MakeBinary(b.NodeType,
                    Expression.MakeMemberAccess(Visit(b.Left), b.Left.Type.FindFirstOrDefaultMember("ID")),
                    Expression.MakeMemberAccess(Visit(b.Right), b.Right.Type.FindFirstOrDefaultMember("ID")));
            }
            return base.VisitBinary(b);
        }
    }
}
