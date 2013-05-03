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

namespace Zetbox.API.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    public static class LinqExtensions
    {
        /// <summary>
        /// See IsIgnorableCast(Type, Type)
        /// </summary>
        /// <param name="e">An expression to check.</param>
        /// <returns></returns>
        public static bool IsIgnorableCastExpression(this Expression e)
        {
            if (e == null) return false;

            if (e.NodeType == ExpressionType.Convert)
            {
                var u = e as UnaryExpression;
                return IsIgnorableCast(
                    operandType: u.Operand.Type,
                    targetType: u.Type);
            }
            else if (e.NodeType == ExpressionType.Call)
            {
                var m = e as MethodCallExpression;
                return m.IsMethodCallExpression("Cast")
                    && IsIgnorableCast(
                        operandType: m.Arguments[0].Type.GetGenericArguments()[0],
                        targetType: m.Type.GetGenericArguments()[0]);
            }
            return false;
        }

        /// <summary>
        /// Checks whether the cast from operandType to targetType needs to be respected in a linq statement
        /// </summary>
        /// <param name="operandType"></param>
        /// <param name="targetType"></param>
        /// <returns>true, if the cast has no effect within the linq statement</returns>
        /// <remarks>
        /// Ignore Converts for all IExportable and IPersistenceObject casts. This may happen even if we don't yet
        /// know what the underlying type is. The database will catch that.
        /// Also remove casts to assignment compatible types, except when casting to Nullable&lt;T&gt;. The latter casts
        /// are necessary to keep operator methods happy, since they do not accept mixed nullability arguments.
        /// </remarks>
        public static bool IsIgnorableCast(Type operandType, Type targetType)
        {
            var castToIExportable = typeof(Zetbox.App.Base.IExportable).IsAssignableFrom(targetType) || targetType.IsIExportableInternal();
            var castToIPersistenceObject = targetType.IsIPersistenceObject();
            var upCast = targetType.IsAssignableFrom(operandType);
            var nullableCast = upCast && !operandType.IsGenericType && operandType.IsValueType && targetType == typeof(Nullable<>).MakeGenericType(operandType);

            return (castToIExportable || castToIPersistenceObject || (upCast && !nullableCast));
        }
    }
}
