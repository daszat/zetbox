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
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Extensions;

    [Implementor]
    public class AnyReferenceActions
    {
        private static IFrozenContext _frozenCtx;
        public AnyReferenceActions(IFrozenContext frozenCtx)
        {
            _frozenCtx = frozenCtx;
        }

        [Invocation]
        public static void ToString(Zetbox.App.Base.AnyReference obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0} - {1}/{2}",  obj.ObjClass, obj.ObjGuid, obj.ObjID);
        }

        [Invocation]
        public static void GetObject(AnyReference obj, MethodReturnEventArgs<Zetbox.API.IDataObject> e, Zetbox.API.IZetboxContext ctx)
        {
            if (obj.ObjClass == null)
            {
                e.Result = null;
            }
            else
            {
                var cls = _frozenCtx.FindPersistenceObject<ObjectClass>(obj.ObjClass.Value);
                var ifType = ctx.GetInterfaceType(cls.GetDataType());
                if (obj.ObjGuid != null)
                {
                    e.Result = (IDataObject)ctx.FindPersistenceObject(ifType, obj.ObjGuid.Value);
                }
                else
                {
                    e.Result = ctx.Find(ifType, obj.ObjID.Value);
                }
            }
        }

        [Invocation]
        public static void SetObject(AnyReference obj, Zetbox.API.IDataObject newObj)
        {
            if (newObj == null)
            {
                obj.ObjClass = null;
                obj.ObjGuid = null;
                obj.ObjID = null;
            }
            else
            {
                var cls = newObj.GetObjectClass(_frozenCtx);
                obj.ObjClass = cls.ExportGuid;
                if (cls.ImplementsIExportable())
                {
                    obj.ObjGuid = ((IExportable)newObj).ExportGuid;
                    obj.ObjID = null;
                }
                else
                {
                    obj.ObjGuid = null;
                    obj.ObjID = newObj.ID;
                }
            }
        }
    }
}
