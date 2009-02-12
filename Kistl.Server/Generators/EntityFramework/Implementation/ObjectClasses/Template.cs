using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public class Template
        : Templates.Implementation.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, Kistl.App.Base.ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override void ApplyClassAttributeTemplate()
        {
            WriteLine("    [EdmEntityType(NamespaceName=\"Model\", Name=\"{0}\")]", this.DataType.ClassName);
        }

        protected override void ApplyIDPropertyTemplate()
        {
            bool hasId = false;
            // only implement ID if necessary
            if (this.DataType is ObjectClass && ((ObjectClass)this.DataType).BaseObjectClass == null)
            {
                hasId = true;
            }
            else if (!(this.DataType is ObjectClass))
            {
                hasId = true;
            }

            if (hasId)
            {
                base.ApplyIDPropertyTemplate();
                Host.CallTemplate("Implementation.ObjectClasses.IdProperty", ctx);
            }
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.API.Server",
                "Kistl.DALProvider.EF",
                "System.Data.Objects",
                "System.Data.Objects.DataClasses" 
            });
        }

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name) + "__Implementation__";
        }

        protected override string GetBaseClass()
        {
            if (this.ObjectClass.BaseObjectClass != null)
            {
                return MungeClassName(base.GetBaseClass());
            }
            else
            {
                return "BaseServerDataObject_EntityFramework";
            }
        }

        protected override void ApplyEnumerationPropertyTemplate(EnumerationProperty prop)
        {
            this.WriteLine("        // enumeration property");
            this.Host.CallTemplate("Implementation.ObjectClasses.EnumerationPropertyTemplate", ctx,
                this.MembersToSerialize,
                prop);
        }

        protected override void ApplyStructPropertyTemplate(StructProperty prop)
        {
            this.WriteLine("        // struct property");
            this.Host.CallTemplate("Implementation.ObjectClasses.StructPropertyTemplate", ctx,
                this.MembersToSerialize,
                prop);
        }

        protected override void ApplyValueTypeListTemplate(ValueTypeProperty prop)
        {
            this.WriteLine("        // value list property");
            this.Host.CallTemplate("Implementation.ObjectClasses.ValueCollectionProperty", ctx,
                this.MembersToSerialize,
                prop);
        }

        protected override void ApplyObjectReferencePropertyTemplate(ObjectReferenceProperty prop)
        {
            var rel = NewRelation.Lookup(ctx, prop);

            Debug.Assert(rel.A.Navigator == prop || rel.B.Navigator == prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = relEnd.Other;

            this.WriteLine("    /*");
            this.CallTemplate("Implementation.RelationDebugTemplate", ctx, rel);
            this.WriteLine("    */");

            switch (rel.GetPreferredStorage())
            {
                case StorageHint.MergeA:
                case StorageHint.MergeB:
                case StorageHint.Replicate:

                    this.WriteLine("        // object reference property");
                    this.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                        this.MembersToSerialize,
                        prop.PropertyName,
                        rel.GetAssociationName(), otherEnd.RoleName,
                        otherEnd.Type.NameDataObject,
                        otherEnd.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix,
                        (relEnd.Multiplicity.UpperBound() > 1 && relEnd.HasPersistentOrder)
                        );
                    break;
                case StorageHint.Separate:
                default:
                    throw new NotImplementedException("unknown StorageHint for ObjectReferenceProperty[IsList == false]");
            }
        }

        protected override void ApplyObjectReferenceListTemplate(ObjectReferenceProperty prop)
        {
            var rel = NewRelation.Lookup(ctx, prop);

            Debug.Assert(rel.A.Navigator == prop || rel.B.Navigator == prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = relEnd.Other;

            this.WriteLine("    /*");
            this.CallTemplate("Implementation.RelationDebugTemplate", ctx, rel);
            this.WriteLine("    */");

            switch (rel.GetPreferredStorage())
            {
                case StorageHint.MergeA:
                case StorageHint.MergeB:
                case StorageHint.Replicate:

                    // simple and direct reference
                    this.WriteLine("        // object list property");
                    this.Host.CallTemplate("Implementation.ObjectClasses.ObjectListProperty", ctx,
                        this.MembersToSerialize,
                        relEnd);
                    break;
                case StorageHint.Separate:
                    this.WriteLine("        // collection reference property");
                    this.CallTemplate("Implementation.ObjectClasses.CollectionEntryListProperty", ctx,
                        this.MembersToSerialize,
                        relEnd);
                    break;
                default:
                    throw new NotImplementedException("unknown StorageHint for ObjectReferenceProperty[IsList == true]");
            }
        }
    }
}
