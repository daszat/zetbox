using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

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

        protected override void ApplyConstructorTemplate()
        {
            base.ApplyConstructorTemplate();
            this.WriteObjects("            {");
            this.WriteLine();
            foreach (var prop in DataType.Properties.OfType<StructProperty>())
            {
                string name = prop.PropertyName;
                string backingName = "_" + name;
                string structType = prop.GetPropertyTypeString();
                string structImplementationType = structType + Kistl.API.Helper.ImplementationSuffix;
                this.WriteObjects("                ", backingName, " = new ", structImplementationType, "(this, \"", name, "\");");
                this.WriteLine();
            }
            this.WriteObjects("            }");
            this.WriteLine();
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
            var rel = RelationExtensions.Lookup(ctx, prop);

            Debug.Assert(rel.A.Navigator.ID == prop.ID || rel.B.Navigator.ID == prop.ID);
            RelationEnd relEnd = rel.GetEnd(prop);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            if (rel.Storage == StorageType.Separate)
            {
                throw new InvalidOperationException("Separate Storage not implemented for ObjectReferenceProperty in 1:N");
            }

            this.WriteLine("    /*");
            this.CallTemplate("Implementation.RelationDebugTemplate", ctx, rel);
            this.WriteLine("    */");

            this.WriteLine("        // object reference property");
                this.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                    this.MembersToSerialize,
                    prop.PropertyName,
                    rel.GetAssociationName(), otherEnd.RoleName,
                    otherEnd.Type.GetDataTypeString(),
                    otherEnd.Type.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix,
                    rel.NeedsPositionStorage((RelationEndRole)relEnd.Role)
                    );
        }

        protected override void ApplyCollectionEntryListTemplate(Relation rel, RelationEndRole endRole)
        {
            this.CallTemplate("Implementation.ObjectClasses.CollectionEntryListProperty", ctx,
                this.MembersToSerialize,
                rel, endRole);
        }

        protected override void ApplyObjectReferenceListTemplate(ObjectReferenceProperty prop)
        {
            // TODO: move debugging output into Templates
            this.WriteLine("    /*");
            this.CallTemplate("Implementation.RelationDebugTemplate", ctx, RelationExtensions.Lookup(ctx, prop));
            this.WriteLine("    */");

            base.ApplyObjectReferenceListTemplate(prop);
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            this.CallTemplate("Implementation.ObjectClasses.ReloadReferences", ctx, this.DataType);
        }
    }
}
