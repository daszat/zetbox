
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    using Kistl.API;

    internal class QueryTranslator : ExpressionTreeTranslator
    {
        public static Expression Translate(Expression e)
        {
            var t = new QueryTranslator();
            return t.Visit(e);
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.Equal && b.Left.Type.IsIDataObject() && b.Right.Type.IsIDataObject())
            {
                return Expression.MakeBinary(b.NodeType,
                    Expression.MakeMemberAccess(Visit(b.Left), b.Left.Type.FindFirstOrDefaultMember("ID")),
                    Expression.MakeMemberAccess(Visit(b.Right), b.Right.Type.FindFirstOrDefaultMember("ID")));
            }
            return base.VisitBinary(b);
        }
    }
}
