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

namespace Zetbox.Server.SchemaManagement
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Zetbox.API;

    [Serializable]
    public class CheckExpressionVisitor
    {
        public string TranslateCheckExpression(Expression<Func<string, bool>> checkExpression)
        {
            if (checkExpression == null) throw new ArgumentNullException("checkExpression");

            return Visit(checkExpression.Body);
        }

        protected virtual string Visit(Expression e)
        {
            if (e == null) return null;

            switch (e.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    return VisitUnary((UnaryExpression)e);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    return VisitBinary((BinaryExpression)e);
                case ExpressionType.Conditional:
                    return VisitConditional((ConditionalExpression)e);
                case ExpressionType.Constant:
                    return VisitConstant((ConstantExpression)e);
                case ExpressionType.Parameter:
                    return VisitParameter((ParameterExpression)e);
                case ExpressionType.MemberAccess:
                case ExpressionType.Call:
                case ExpressionType.Lambda:
                case ExpressionType.New:
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                case ExpressionType.Invoke:
                case ExpressionType.MemberInit:
                case ExpressionType.ListInit:
                case ExpressionType.TypeIs:
                default:
                    throw new NotSupportedException(string.Format("Cannot translate: '{0}'", e.NodeType));
            }
        }

        protected virtual string VisitUnary(UnaryExpression u)
        {
            var operand = Visit(u.Operand);
            switch (u.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    return "-" + operand;
                case ExpressionType.Not:
                    return "NOT " + operand;
                case ExpressionType.Quote:
                    return "(" + operand + ")";
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.TypeAs:
                default:
                    throw new NotSupportedException(u.NodeType.ToString());
            }
        }

        protected virtual string VisitBinary(BinaryExpression b)
        {
            if (b.Conversion != null) throw new NotSupportedException(string.Format("cannot translate binary expression {0} with conversion: {1}", b.NodeType, b.Conversion));

            string left = Visit(b.Left);
            string right = Visit(b.Right);

            switch (b.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return left + "+" + right;
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return left + "-" + right;
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return left + "*" + right;
                case ExpressionType.Divide:
                    return left + "/" + right;
                case ExpressionType.Modulo:
                    return left + "%" + right;
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return left + " AND " + right;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return left + " OR " + right;
                case ExpressionType.LessThan:
                    return left + "<" + right;
                case ExpressionType.LessThanOrEqual:
                    return left + "<=" + right;
                case ExpressionType.GreaterThan:
                    return left + ">" + right;
                case ExpressionType.GreaterThanOrEqual:
                    return left + ">=" + right;
                case ExpressionType.Equal:
                    if (left == "NULL")
                        return right + " IS NULL";
                    else if (right == "NULL")
                        return left + " IS NULL";
                    else
                        return left + "=" + right;
                case ExpressionType.NotEqual:
                    if (left == "NULL")
                        return right + " IS NOT NULL";
                    else if (right == "NULL")
                        return left + " IS NOT NULL";
                    else
                        return left + "<>" + right;
                case ExpressionType.Coalesce:
                    return "COALESCE(" + left + ", " + right + ")";
                case ExpressionType.ExclusiveOr:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ArrayIndex:
                default:
                    throw new NotSupportedException(b.NodeType.ToString());
            }
        }

        protected virtual string VisitConditional(ConditionalExpression c)
        {
            return "CASE WHEN " + Visit(c.Test) + " THEN " + Visit(c.IfTrue) + " ELSE " + Visit(c.IfFalse) + " END";
        }

        protected virtual string VisitConstant(ConstantExpression c)
        {
            var exprType = c.Type;
            if (exprType == typeof(object) && c.Value == null)
            {
                return "NULL";
            }
            else if (exprType == typeof(bool) || exprType == typeof(bool?))
            {
                var v = (bool?)c.Value;
                return v.HasValue
                    ? (v.GetValueOrDefault() ? "(0=0)" : "(0=1)")
                    : "NULL";
            }
            else if (exprType == typeof(int) || exprType == typeof(int?))
            {
                var v = (int?)c.Value;
                return v.HasValue
                    ? v.GetValueOrDefault().ToString(CultureInfo.InvariantCulture)
                    : "NULL";
            }
            else if (exprType == typeof(string))
            {
                var v = (string)c.Value;
                return v != null
                    ? "'" + v.Replace("'", "''") + "'"
                    : "NULL";
            }
            else
            {
                throw new NotSupportedException(string.Format("Cannot evaluate constant of type {0}: {1}", c.Type.AssemblyQualifiedName, c.Value));
            }
        }

        protected virtual string VisitParameter(ParameterExpression p)
        {
            return "{0}"; // will be string.Formatted afterwards
        }
    }
}

