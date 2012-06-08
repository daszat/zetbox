
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
                var left = Visit(b.Left);
                var right = Visit(b.Right);

                return Expression.AndAlso(
                    Expression.NotEqual(left, Expression.Constant(null)),
                    Expression.AndAlso(
                        Expression.NotEqual(right, Expression.Constant(null)),
                        Expression.MakeBinary(
                            b.NodeType,
                            Expression.MakeMemberAccess(left, b.Left.Type.FindFirstOrDefaultMember("ID")),
                            Expression.MakeMemberAccess(right, b.Right.Type.FindFirstOrDefaultMember("ID")))
                        )
                    );
            }
            return base.VisitBinary(b);
        }
    }
}
