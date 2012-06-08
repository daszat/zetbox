// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    using Zetbox.API;

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
