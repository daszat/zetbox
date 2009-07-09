using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using System.Collections;

namespace Kistl.Server.Packaging
{
    internal static class PackagingHelper
    {
        public static IDictionary<Guid, IPersistenceObject> GetMetaObjects(IKistlContext ctx, Module module)
        {
            IDictionary<Guid, IPersistenceObject> result = new Dictionary<Guid, IPersistenceObject>();
            int moduleID = module.ID;

            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.Module>().Where(i => i.ID == moduleID));

            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.DataType>().Where(i => i.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetPersistenceObjectQuery<Kistl.App.Base.ObjectClass_implements_Interface_RelationEntry>().Where(i => i.A.Module.ID == moduleID || i.B.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.Property>().Where(i => i.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.Relation>().Where(i => i.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.RelationEnd>().Where(i => i.AParent.Module.ID == moduleID || i.BParent.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.EnumerationEntry>().Where(i => i.Enumeration.Module.ID == moduleID));

            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.Method>().Where(i => i.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.BaseParameter>().Where(i => i.Method.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.MethodInvocation>().Where(i => i.Module.ID == moduleID));

            // TODO: Add Module to Constraint
            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.Constraint>().Where(i => i.ConstrainedProperty.Module.ID == moduleID));

            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.Assembly>().Where(i => i.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetQuery<Kistl.App.Base.TypeRef>().Where(i => i.Assembly.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetPersistenceObjectQuery<Kistl.App.Base.TypeRef_hasGenericArguments_TypeRef_RelationEntry>().Where(i => i.A.Assembly.Module.ID == moduleID || i.B.Assembly.Module.ID == moduleID));

            AddMetaObjects(result, ctx.GetQuery<Kistl.App.GUI.Icon>().Where(i => i.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetQuery<Kistl.App.GUI.PresentableModelDescriptor>().Where(i => i.Module.ID == moduleID));
            AddMetaObjects(result, ctx.GetQuery<Kistl.App.GUI.ViewDescriptor>().Where(i => i.Module.ID == moduleID));

            return result;
        }

        private static void AddMetaObjects(IDictionary<Guid, IPersistenceObject> result, IEnumerable objects)
        {
            foreach (IPersistenceObject obj in objects)
            {
                result.Add(((IExportableInternal)obj).ExportGuid, obj);
            }
        }
    }
}
