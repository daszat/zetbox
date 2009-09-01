
namespace Kistl.Server.Packaging
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;

    internal static class PackagingHelper
    {
        public static IList<IPersistenceObject> GetMetaObjects(IKistlContext ctx, Module module)
        {
            IList<IPersistenceObject> result = new List<IPersistenceObject>();
            int moduleID = module.ID;

            AddMetaObjects(result, ctx.GetQuery<Module>().Where(i => i.ID == moduleID).OrderBy(m => m.ModuleName));

            AddMetaObjects(result, ctx.GetQuery<DataType>().Where(i => i.Module.ID == moduleID)
                .OrderBy(i => i.ClassName));
            AddMetaObjects(result, ctx.GetPersistenceObjectQuery<ObjectClass_implements_Interface_RelationEntry>().Where(i => i.A.Module.ID == moduleID || i.B.Module.ID == moduleID)
                .OrderBy(i => i.A.ClassName).ThenBy(i => i.B.ClassName));
            AddMetaObjects(result, ctx.GetQuery<Property>().Where(i => i.Module.ID == moduleID)
                .OrderBy(i => i.ObjectClass.ClassName).ThenBy(i => i.PropertyName));
            AddMetaObjects(result, ctx.GetQuery<Relation>().Where(i => i.Module.ID == moduleID)
                .OrderBy(i => i.A.Type.ClassName).ThenBy(i => i.Verb).ThenBy(i => i.B.Type.ClassName));
            AddMetaObjects(result, ctx.GetQuery<RelationEnd>().Where(i => (i.AParent != null && i.AParent.Module.ID == moduleID) || (i.BParent != null && i.BParent.Module.ID == moduleID))
                .OrderBy(i => i.Type.ClassName).ThenBy(i => i.RoleName).ThenBy(i => i.ExportGuid));
            AddMetaObjects(result, ctx.GetQuery<EnumerationEntry>().Where(i => i.Enumeration.Module.ID == moduleID)
                .OrderBy(i => i.Enumeration.ClassName).ThenBy(i => i.Name));

            AddMetaObjects(result, ctx.GetQuery<Method>().Where(i => i.Module.ID == moduleID)
                .OrderBy(i => i.ObjectClass.ClassName).ThenBy(i => i.MethodName));
            AddMetaObjects(result, ctx.GetQuery<BaseParameter>().Where(i => i.Method.Module.ID == moduleID)
                .OrderBy(i => i.Method.ObjectClass.ClassName).ThenBy(i => i.Method.MethodName).ThenBy(i => i.ParameterName));
            AddMetaObjects(result, ctx.GetQuery<MethodInvocation>().Where(i => i.Module.ID == moduleID)
                .OrderBy(i => i.InvokeOnObjectClass.ClassName).ThenBy(i => i.Method.MethodName).ThenBy(i => i.Implementor.FullName).ThenBy(i => i.MemberName));
            AddMetaObjects(result, ctx.GetQuery<PropertyInvocation>().Where(i => i.InvokeOnProperty.Module.ID == moduleID)
                .OrderBy(i => i.InvokeOnProperty.PropertyName).ThenBy(i => i.Implementor.FullName).ThenBy(i => i.MemberName));

            // TODO: Add Module to Constraint - or should that not be changable by other modules?
            AddMetaObjects(result, ctx.GetQuery<Constraint>().Where(i => i.ConstrainedProperty.Module.ID == moduleID).ToList().AsQueryable() // local sorting because of GetInterfaceType
                .OrderBy(i => i.ConstrainedProperty.ObjectClass.ClassName).ThenBy(i => i.ConstrainedProperty.PropertyName).ThenBy(i => i.GetInterfaceType().Type.Name).ThenBy(i => i.ExportGuid));

            // TODO: Add Module to DefaultPropertyValue - or should that not be changable by other modules?
            AddMetaObjects(result, ctx.GetQuery<DefaultPropertyValue>().Where(i => i.Property.Module.ID == moduleID)
                .OrderBy(i => i.Property.ObjectClass.ClassName).ThenBy(i => i.Property.PropertyName));

            AddMetaObjects(result, ctx.GetQuery<Assembly>().Where(i => i.Module.ID == moduleID)
                .OrderBy(i => i.AssemblyName));
            AddMetaObjects(result, ctx.GetQuery<TypeRef>().Where(i => i.Assembly.Module.ID == moduleID)
                .OrderBy(i => i.Assembly.AssemblyName).ThenBy(i => i.FullName).ThenBy(i => i.ExportGuid));
            AddMetaObjects(result, ctx.GetPersistenceObjectQuery<TypeRef_hasGenericArguments_TypeRef_RelationEntry>().Where(i => i.A.Assembly.Module.ID == moduleID || i.B.Assembly.Module.ID == moduleID)
                .OrderBy(i => i.A.Assembly.AssemblyName).ThenBy(i => i.B.Assembly.AssemblyName).ThenBy(i => i.A.FullName).ThenBy(i => i.B.FullName).ThenBy(i => i.A.ExportGuid).ThenBy(i => i.B.ExportGuid));

            AddMetaObjects(result, ctx.GetQuery<Icon>().Where(i => i.Module.ID == moduleID)
                .OrderBy(i => i.IconFile));
            AddMetaObjects(result, ctx.GetQuery<PresentableModelDescriptor>().Where(i => i.Module.ID == moduleID)
                .OrderBy(i => i.PresentableModelRef.Assembly.AssemblyName).ThenBy(i => i.PresentableModelRef.FullName).ThenBy(i => i.ExportGuid));
            AddMetaObjects(result, ctx.GetQuery<ViewDescriptor>().Where(i => i.Module.ID == moduleID)
                .OrderBy(i => i.ControlRef.Assembly.AssemblyName).ThenBy(i => i.ControlRef.FullName).ThenBy(i => i.ExportGuid));
            if (module.ModuleName == "GUI")
            {
                AddMetaObjects(result, ctx.GetQuery<ControlKind>()// TODO: .Where(i => i.Module.ID == moduleID)
                    .ToList().AsQueryable() // TODO: remove this workaround for GetInterfaceType()
                    .OrderBy(i => i.GetInterfaceType().Type.FullName).ThenBy(i => i.ExportGuid));
                AddMetaObjects(result, ctx.GetPersistenceObjectQuery<PresentableModelDescriptor_displayedBy_ControlKind_RelationEntry>()
                    .ToList().AsQueryable() // TODO: remove this workaround for GetType()
                    .OrderBy(i => i.A.PresentableModelRef.Assembly.AssemblyName).ThenBy(i => i.A.PresentableModelRef.FullName)
                    .ThenBy(i => i.B.GetType().FullName));
            }
            return result;
        }

        private static void AddMetaObjects<T>(IList<IPersistenceObject> result, IOrderedQueryable<T> objects)
            where T : IPersistenceObject
        {
            // TODO: always do a final stabilisation sort by ExportGuid
            // currently doesn't work, since EF doesn't like the cast
            foreach (IPersistenceObject obj in objects) //.ThenBy(o => ((IExportable)o).ExportGuid))
            {
                if (((IExportable)obj).ExportGuid == Guid.Empty)
                {
                    throw new InvalidOperationException(string.Format("At least one object of type {0} has an empty ExportGuid", typeof(T).FullName));
                }
                result.Add(obj);
            }
        }
    }
}
