using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

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
            var rel = NewRelation.Lookup(ctx, prop);

            var relEnd = rel.GetEnd(prop);
            var otherEnd = relEnd.Other;

            // without navigator, there should be no property
            if (relEnd.Navigator == null)
                return;

            switch (rel.GetPreferredStorage())
            {
                case StorageHint.MergeA:
                case StorageHint.MergeB:
                case StorageHint.Replicate:

                    // simple and direct reference
                    this.WriteLine("        // object list property");
                    ApplyObjectListPropertyTemplate(relEnd);
                    break;
                case StorageHint.Separate:
                    this.WriteLine("        // collection reference property");
                    ApplyCollectionEntryListTemplate(relEnd);
                    break;
                default:
                    throw new NotImplementedException("unknown StorageHint for ObjectReferenceProperty[IsList == true]");
            }
        }

        /// <summary>
        /// Call the ObjectListProperty template for a given RelationEnd
        /// </summary>
        /// <param name="relEnd"></param>
        protected virtual void ApplyObjectListPropertyTemplate(RelationEnd relEnd)
        {
            this.Host.CallTemplate("Implementation.ObjectClasses.ObjectListProperty", ctx,
                this.MembersToSerialize,
                relEnd);
        }

        /// <summary>
        /// Call the CollectionEntryListProperty template for a given RelationEnd
        /// </summary>
        /// <param name="relEnd"></param>
        protected virtual void ApplyCollectionEntryListTemplate(RelationEnd relEnd)
        {
            Implementation.ObjectClasses.CollectionEntryListProperty.Call(Host, ctx,
                this.MembersToSerialize,
                relEnd);
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

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            this.Host.CallTemplate("Implementation.ObjectClasses.Tail", ctx, this.DataType);
        }

    }
}
