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
using NHibernate;
using NHibernate.Type;
using Zetbox.API;

namespace Zetbox.DalProvider.NHibernate
{
    /// <summary>
    /// Convert all DateTimes from/to the database into local time.
    /// </summary>
    /// <remarks>Based on Dan Morphi's http://www.milkcarton.com/blog/2007/01/19/NHibernate+DateTime+And+UTC.aspx </remarks>
    public class NHInterceptor : EmptyInterceptor
    {
        private readonly IZetboxContext _parent;
        private readonly Func<IFrozenContext> _lazyContext;
        public NHInterceptor(IZetboxContext parent, Func<IFrozenContext> lazyContext)
        {
            _parent = parent;
            _lazyContext = lazyContext;
        }

        public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            if (entity is IPersistenceObject)
            {
                ((IPersistenceObject)entity).AttachToContext(_parent, _lazyContext);
            }
            ConvertDateToLocal(state, types);
            return true;
        }

        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            ConvertDateToLocal(state, types);
            return true;
        }

        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
            ConvertDateToLocal(currentState, types);
            return true;
        }

        private void ConvertDateToLocal(object[] state, IType[] types)
        {
            for (int index = 0; index < types.Length; index++)
            {
                if(state[index] == null) continue;
                if ((types[index].ReturnedClass == typeof(DateTime)))
                {
                    DateTime cur = (DateTime)state[index];
                    switch (cur.Kind)
                    {
                        case DateTimeKind.Local:
                            break;
                        case DateTimeKind.Utc:
                            state[index] = cur.ToLocalTime();
                            break;
                        case DateTimeKind.Unspecified:
                            state[index] = DateTime.SpecifyKind(cur, DateTimeKind.Local);
                            break;
                    }
                }
                else if ((types[index].ReturnedClass == typeof(DateTime?)))
                {
                    DateTime? cur = (DateTime?)state[index];
                    if (cur != null)
                    {
                        switch (cur.Value.Kind)
                        {
                            case DateTimeKind.Local:
                                break;
                            case DateTimeKind.Utc:
                                state[index] = cur.Value.ToLocalTime();
                                break;
                            case DateTimeKind.Unspecified:
                                state[index] = DateTime.SpecifyKind(cur.Value, DateTimeKind.Local);
                                break;
                        }
                    }
                }
            }
        }
    }
}
