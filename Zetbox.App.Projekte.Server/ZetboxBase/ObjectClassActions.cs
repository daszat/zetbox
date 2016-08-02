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

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    [Implementor]
    public class ObjectClassActions
    {
        private static ILifetimeScope _scopeFactory;
        private static IFrozenContext _frozenCtx;

        public ObjectClassActions(ILifetimeScope scopeFactory, IFrozenContext frozenCtx)
        {
            if (scopeFactory == null) throw new ArgumentNullException("scopeFactory");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            _scopeFactory = scopeFactory;
            _frozenCtx = frozenCtx;
        }

        [Invocation]
        public static void ReplaceObject(ObjectClass obj, Zetbox.API.IDataObject target, Zetbox.API.IDataObject source)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (target == null) throw new ArgumentNullException("target");
            if (source == null) throw new ArgumentNullException("source");

            var cls = _frozenCtx.FindPersistenceObject<ObjectClass>(obj.ExportGuid);
            var clsType = cls.GetDescribedInterfaceType();

            if(clsType != _frozenCtx.GetInterfaceType(target) 
            || clsType != _frozenCtx.GetInterfaceType(source))
            {
                throw new ArgumentException("source and target must match the object class");
            }

            var relEnds = _frozenCtx.GetQuery<RelationEnd>()
                .Where(i => i.Type == cls)
                .ToList();

            using (var scope = _scopeFactory.BeginLifetimeScope())
            using (var ctx = scope.Resolve<IZetboxServerContext>())
            {
                var targetObj = ctx.FindPersistenceObject(clsType, target.ID);
                var sourceObj = ctx.FindPersistenceObject(clsType, source.ID);
                var sourceID = source.ID;

                foreach(var relEnd in relEnds)
                {
                    var otherEnd = relEnd.Parent.GetOtherEnd(relEnd);
                    var rel = relEnd.Parent;

                    if (otherEnd.Navigator != null)
                    {
                        var prop = otherEnd.Navigator;
                        var propName = prop.Name;
                        var refClass = (ObjectClass)prop.ObjectClass;
                        var ifType = refClass.GetDescribedInterfaceType();

                        if (prop.GetIsList())
                        {
                            foreach (var refObj in ctx.Internals().GetPersistenceObjectQuery(ifType).Where(string.Format("{0}.Any(ID == @0)", propName), sourceID))
                            {
                                refObj.RemoveFromCollection(propName, sourceObj);
                                refObj.AddToCollection(propName, targetObj, true);
                            }
                        }
                        else
                        {
                            foreach (var refObj in ctx.Internals().GetPersistenceObjectQuery(ifType).Where(string.Format("{0}.ID == @0", propName), sourceID))
                            {
                                refObj.SetPropertyValue(propName, targetObj);
                            }
                        }
                    } 
                    else 
                    {
                        // Not my business
                        // The only side that that mathers is the side, that POINTS to the object in question.
                    }                    
                }

                ctx.SubmitChanges();
            }
        }
    }
}
