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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Diagnostics;

namespace Zetbox.API.Utils
{
    public class ExpressionDumper
        : ExpressionVisitor<bool>
    {

        public static void Dump(Expression expr)
        {
            var dumper = new ExpressionDumper();
            dumper.Visit(expr);
        }

        private static void DescribeExpression(Expression expr)
        {
            Debug.Print("{0} {{ NodeType={1}, Type={2} }}", expr.GetType().FullName, expr.NodeType, expr.Type);
        }

        protected override bool VisitExpression(BinaryExpression expr)
        {
            Debug.Indent();
            DescribeExpression(expr);

            Debug.Print("* Method: {0}.{1}({2})",
                expr.Method.DeclaringType.FullName, 
                expr.Method.Name, 
                String.Join(", ", expr.Method.GetParameters().Select(pi => pi.GetType().FullName).ToArray()));

            Debug.Print("* Conversion:");
            Visit(expr.Conversion);

            Debug.Print("* Right:");
            Visit(expr.Right);

            Debug.Print("* Left:");
            Visit(expr.Left);

            Debug.Unindent();
            return true;
        }

        protected override bool VisitExpression(ConditionalExpression expr)
        {
            Debug.Indent();
            DescribeExpression(expr);
            Debug.Unindent();
            return true;
        }

        protected override bool VisitExpression(ConstantExpression expr)
        {
            Debug.Indent();
            DescribeExpression(expr);

            Debug.Print("* Value:");
            Debug.WriteLine(expr.Value);

            Debug.Unindent();
            return true;
        }

        protected override bool VisitExpression(LambdaExpression expr)
        {
            Debug.Indent();
            DescribeExpression(expr);
            Debug.Unindent();
            return true;
        }

        protected override bool VisitExpression(MemberExpression expr)
        {
            Debug.Indent();
            DescribeExpression(expr);
            Debug.Print("* Member: {0}.{1}",
                expr.Member.DeclaringType.FullName,
                expr.Member.Name);

            Debug.Print("* Expression:");
            Visit(expr.Expression);

            Debug.Unindent();
            return true;
        }

        protected override bool VisitExpression(MethodCallExpression expr)
        {
            Debug.Indent();
            DescribeExpression(expr);
            Debug.Unindent();
            return true;
        }

        protected override bool VisitExpression(NewExpression expr)
        {
            Debug.Indent();
            DescribeExpression(expr);
            Debug.Unindent();
            return true;
        }

        protected override bool VisitExpression(ParameterExpression expr)
        {
            Debug.Indent();
            DescribeExpression(expr);
            Debug.Unindent();
            return true;
        }

        protected override bool VisitExpression(UnaryExpression expr)
        {
            Debug.Indent();
            DescribeExpression(expr);
            Debug.Unindent();
            return true;
        }
    }
}
