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
using System.Text;
using System.Linq.Expressions;

namespace Zetbox.API
{
    /// <summary>
    /// This object represents an Illegal Linq Expression which was send to the server.
    /// </summary>
    [Serializable]
    public class IllegalExpression
    {
        /// <summary>
        /// Illegal Method Call name
        /// </summary>
        public string MethodCall { get; set; }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>Text</returns>
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

    /// <summary>
    /// Implements an Expression Filter. This class Ensures that only legal Expression are passed to the Server.
    /// Illegal Expression: Every MethodCall Expression not implemented on System.String
    /// 
    /// ** Rethink this!!
    /// Security should be implemented in querytranslator corretly
    /// That means, that Aggregations &amp; Co are allowed on the client side
    /// But that's an topic to discuss
    /// </summary>
    /// 
    public static class ExpressionFilter
    {
        /// <summary>
        /// Checks if the given Expression is legal.
        /// </summary>
        /// <param name="e">Expression</param>
        /// <returns>true, if the expression is legal. Otherwise false.</returns>
        public static bool IsLegal(this Expression e)
        {
            List<IllegalExpression> tmp;
            return IsLegal(e, out tmp);
        }

        /// <summary>
        /// Checks if the given Expression is legal.
        /// </summary>
        /// <param name="e">Expression</param>
        /// <param name="list">Out: List of illegal Expressions</param>
        /// <returns>true, if the expression is legal. Otherwise false.</returns>
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

                // Rethink this
                // Security should be implemented in querytranslator corretly
                // That means, that Aggregations & Co are allowed on the client side
                // But that's an topic to discuss
                //if (m.Method.DeclaringType != typeof(string))
                //{
                //    IsLegal = false;
                //    IllegalExpression.Add(new IllegalExpression() { MethodCall = m.Method.DeclaringType.FullName + "." + m.Method.Name });
                //}
            }
        }
    }
}