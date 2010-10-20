
namespace Kistl.DalProvider.NHibernate.Generator.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Server.Generators.Extensions;
    using Templates = Kistl.Server.Generators.Templates;

    public class Template
        : Templates.Implementation.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.DalProvider.NHibernate",
            });
        }

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name)
                + Kistl.API.Helper.ImplementationSuffix
                + NhGenerator.Suffix;
        }

        protected override string GetBaseClass()
        {
            if (this.ObjectClass.BaseObjectClass != null)
            {
                return MungeClassName(base.GetBaseClass());
            }
            else
            {
                return "BaseNhDataObject";
            }
        }

        protected override void ApplyObjectReferencePropertyTemplate(ObjectReferenceProperty prop)
        {
            //var rel = RelationExtensions.Lookup(ctx, prop);

            //Debug.Assert(rel.A.Navigator.ID == prop.ID || rel.B.Navigator.ID == prop.ID);
            //var relEnd = rel.GetEnd(prop);
            //var otherEnd = rel.GetOtherEnd(relEnd);

            this.WriteLine("        // object reference property");
            this.WriteObjects("        public ", prop.GetPropertyTypeString(), " ", prop.Name, " { get; set; }");
            this.WriteLine();
        }

        protected override void ApplyCompoundObjectPropertyTemplate(CompoundObjectProperty prop)
        {
            this.WriteLine("        // CompoundObject property");
            this.WriteObjects("        public ", prop.GetPropertyTypeString(), " ", prop.Name, " { get; set; }");
            this.WriteLine();
        }
        protected override void ApplyCompoundObjectListTemplate(CompoundObjectProperty prop)
        {
            this.WriteLine("        // CompoundObject list property");
            this.WriteObjects("        public ", prop.GetPropertyTypeString(), " ", prop.Name, " { get; set; }");
            this.WriteLine();
        }

        protected override void ApplyObjectListPropertyTemplate(Relation rel, RelationEndRole endRole)
        {
            this.WriteLine("        // ApplyObjectListPropertyTemplate");
            ObjectListProperty.Call(Host, ctx, this.MembersToSerialize, rel.GetEndFromRole(endRole).Navigator as ObjectReferenceProperty);
        }

        protected override void ApplyCollectionEntryListTemplate(Relation rel, RelationEndRole endRole)
        {
            this.WriteLine("        // ApplyCollectionEntryListTemplate");
            ObjectListProperty.Call(Host, ctx, this.MembersToSerialize, rel.GetEndFromRole(endRole).Navigator as ObjectReferenceProperty);
        }

        protected override void ApplyValueTypeListTemplate(ValueTypeProperty prop)
        {
            this.WriteLine("        // value list property");
            this.WriteObjects("        public ", prop.GetPropertyTypeString(), " ", prop.Name, " { get; set; }");
            this.WriteLine();
        }

        protected override void ApplyConstructorTemplate()
        {
            base.ApplyConstructorTemplate();
            this.WriteObjects("            {");
            this.WriteLine();
            foreach (var prop in DataType.Properties.OfType<CompoundObjectProperty>().Where(p => !p.IsList).OrderBy(p => p.Name))
            {
                if (prop.IsNullable())
                    continue;

                string name = prop.Name;
                string backingName = name + Kistl.API.Helper.ImplementationSuffix + NhGenerator.Suffix;
                string coType = prop.GetPropertyTypeString();
                string coImplementationType = coType + Kistl.API.Helper.ImplementationSuffix + NhGenerator.Suffix;
                this.WriteObjects("                ", backingName, " = new ", coImplementationType, "(this, \"", name, "\");");
                this.WriteLine();
            }
            this.WriteObjects("            }");
            this.WriteLine();
        }

        protected override void ApplyAttachToContextMethod()
        {
            base.ApplyAttachToContextMethod();
            this.WriteLine("        // ApplyAttachToContextMethod");
            //ClientObjects.Implementation.ObjectClasses.AttachToContextTemplate.Call(Host, ctx, ObjectClass);
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            this.WriteLine("        // ApplyClassTailTemplate");
            //ClientObjects.Implementation.ObjectClasses.UpdateParentTemplate.Call(Host, ctx, DataType);
        }
    }
}
