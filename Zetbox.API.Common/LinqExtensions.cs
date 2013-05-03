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
        public static bool IsIgnorableCastExpression(this Expression e)
        {
            if (e == null) return false;

            var targetType = e.Type;

            // Ignore Converts for all IExportable and IPersistenceObject casts. This may happen even if we don't yet
            // know what the underlying type is. The database will catch that.
            // Also remove casts to assignment compatible types, except when casting to Nullable<T>. The latter casts
            // are necessary to keep operator methods happy, since they do not accept mixed nullability arguments.
            if (e.NodeType == ExpressionType.Convert)
            {
                var u = e as UnaryExpression;
                var operandType = u.Operand.Type;

                var castToIExportable = typeof(Zetbox.App.Base.IExportable).IsAssignableFrom(targetType) || targetType.IsIExportableInternal();
                var castToIPersistenceObject = targetType.IsIPersistenceObject();
                var upCast = targetType.IsAssignableFrom(operandType);
                var nullableCast = upCast && !operandType.IsGenericType && operandType.IsValueType && targetType == typeof(Nullable<>).MakeGenericType(operandType);

                return (castToIExportable || castToIPersistenceObject || (upCast && !nullableCast));
            }
            return false;
        }
    }
}
