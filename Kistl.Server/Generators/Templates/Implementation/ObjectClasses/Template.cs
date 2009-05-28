using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public class Template
        : Kistl.Server.Generators.Templates.Implementation.TypeBase
    {
        protected ObjectClass ObjectClass { get; private set; }

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass t)
            : base(_host, ctx, t)
        {
            this.ObjectClass = t;
        }

        /// <returns>The base class to inherit from.</returns>
        protected override string GetBaseClass()
        {
            var baseClass = this.ObjectClass.BaseObjectClass;
            if (baseClass != null)
            {
                return baseClass.Module.Namespace + "." + baseClass.ClassName;
            }
            else
            {
                return "";
            }
        }

        protected override void ApplyObjectReferenceListTemplate(ObjectReferenceProperty prop)
        {
            var rel = RelationExtensions.Lookup(ctx, prop);

            var relEnd = rel.GetEnd(prop);
            var otherEnd = rel.GetOtherEnd(relEnd);

            // without navigator, there should be no property
            if (relEnd.Navigator == null)
                return;

            switch ((StorageType)rel.Storage)
            {
                case StorageType.MergeIntoA:
                case StorageType.MergeIntoB:
                case StorageType.Replicate:
                    // simple and direct reference
                    this.WriteLine("        // object list property");
                    ApplyObjectListPropertyTemplate(rel, (RelationEndRole)relEnd.Role);
                    break;
                case StorageType.Separate:
                    this.WriteLine("        // collection reference property");
                    ApplyCollectionEntryListTemplate(rel, (RelationEndRole)relEnd.Role);
                    break;
                default:
                    throw new NotImplementedException("unknown StorageHint for ObjectReferenceProperty[IsList == true]");
            }
        }

        /// <summary>
        /// Call the ObjectListProperty template for a given RelationEnd
        /// </summary>
        /// <param name="rel"></param>
        /// <param name="endRole"></param>
        protected virtual void ApplyObjectListPropertyTemplate(Relation rel, RelationEndRole endRole)
        {
            Implementation.ObjectClasses.ObjectListProperty.Call(Host, ctx,
                this.MembersToSerialize,
                rel, endRole);
        }

        /// <summary>
        /// Call the CollectionEntryListProperty template for a given RelationEnd
        /// </summary>
        /// <param name="rel"></param>
        /// <param name="endRole"></param>
        protected virtual void ApplyCollectionEntryListTemplate(Relation rel, RelationEndRole endRole)
        {
            Implementation.ObjectClasses.CollectionEntryListProperty.Call(Host, ctx,
                this.MembersToSerialize,
                rel, endRole);
        }


        // HACK: workaround the fact this is missing on the server
        // TODO: remove this and move the client action "OnGetInheritedMethods_ObjectClass" into a common action assembly
        private static void GetMethods(ObjectClass obj, List<Kistl.App.Base.Method> e)
        {
            if (obj.BaseObjectClass != null)
                GetMethods(obj.BaseObjectClass, e);
            e.AddRange(obj.Methods);
        }

        protected override IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            var inherited = new List<Kistl.App.Base.Method>();
            GetMethods(this.ObjectClass, inherited);
            // TODO: fix Default methods in DB, remove the filter here and remove them from TailTemplate
            return inherited.Where(m => !m.IsDefaultMethod());
        }

        protected override void ApplyApplyChangesFromMethod()
        {
            base.ApplyApplyChangesFromMethod();
            Implementation.ObjectClasses.ApplyChangesFromMethod.Call(Host, ctx, this.DataType);
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            Implementation.ObjectClasses.Tail.Call(Host, ctx, this.DataType);
        }

    }
}
