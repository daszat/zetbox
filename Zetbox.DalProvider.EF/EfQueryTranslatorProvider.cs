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

namespace Zetbox.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.API.Server.PerfCounter;

    internal sealed class EfQueryTranslatorProvider<T>
        : QueryTranslatorProvider<T>
    {
        internal EfQueryTranslatorProvider(IMetaDataResolver metaDataResolver, Identity identity, IQueryable source, IZetboxContext ctx, InterfaceType.Factory iftFactory, IPerfCounter perfCounter)
            : base(metaDataResolver, identity, source, ctx, iftFactory, perfCounter)
        {
        }

        protected override QueryTranslatorProvider<TElement> GetSubProvider<TElement>()
        {
            return new EfQueryTranslatorProvider<TElement>(MetaDataResolver, Identity, Source, Ctx, IftFactory, perfCounter);
        }

        protected override string ImplementationSuffix
        {
            get { return "Ef" + Zetbox.API.Helper.ImplementationSuffix; }
        }

        protected override System.Linq.Expressions.Expression VisitConstant(System.Linq.Expressions.ConstantExpression c)
        {
            // Ef cannot map enumerations to the database, we need to use ints instead
            if (c.Value != null && c.Type.IsEnum) // Handle Enums
            {
                return Expression.Constant((int)c.Value, typeof(int));
            }
            else if (c.Value != null && c.Type.IsGenericType && c.Type.GetGenericTypeDefinition() == typeof(Nullable<>) && c.Type.GetGenericArguments().Single().IsEnum)
            {
                return Expression.Constant((int)c.Value, typeof(int?)); // You can't extract a int? from an enum value
            }
            else
            {
                return base.VisitConstant(c);
            }
        }
    }
}
