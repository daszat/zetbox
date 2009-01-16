using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.API;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public class Template : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, Kistl.App.Base.ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override void ApplyGlobalPreambleTemplate()
        {
            base.ApplyGlobalPreambleTemplate();
            foreach (ObjectReferenceProperty prop in this.DataType.Properties.OfType<ObjectReferenceProperty>().ToList().Where(p => p.HasStorage()))
            {
                var info = AssociationInfo.CreateInfo(ctx, prop);
                this.WriteLine(
                    "[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute(\"Model\", \"{0}\", \"{1}\", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof({2}), \"{3}\", System.Data.Metadata.Edm.RelationshipMultiplicity.{4}, typeof({5}))]",
                    info.AssociationName,
                    info.Parent.RoleName,
                    info.Parent.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix,
                    info.Child.RoleName,
                    prop.GetRelationType() == RelationType.one_one ? "ZeroOrOne" : "Many",
                    info.Child.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix
                    );

                // construct reverse mapping
                if (prop.IsList)
                {
                    var refType = new TypeMoniker(prop.ReferenceObjectClass.GetDataTypeString());
                    this.WriteLine(
                        "[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute(\"Model\", \"{0}\", \"{1}\", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof({2}), \"B_ObjectClass_ImplementsInterfacesCollectionEntry\", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Base.ObjectClass_ImplementsInterfacesCollectionEntry__Implementation__))]",
                        Construct.AssociationName(refType, info.CollectionEntry, prop.PropertyName),
                        Construct.AssociationParentRoleName(prop.ReferenceObjectClass),
                        refType.NameDataObject + Kistl.API.Helper.ImplementationSuffix,
                        info.Child.RoleName,
                        info.Child.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix
                    );
                }

            }
        }

        protected override void ApplyClassAttributeTemplate()
        {
            WriteLine("    [EdmEntityTypeAttribute(NamespaceName=\"Model\", Name=\"{0}\")]", this.DataType.ClassName);
        }

        protected override void ApplyIDPropertyTemplate()
        {
            base.ApplyIDPropertyTemplate();
            Host.CallTemplate("Implementation.ObjectClasses.IdProperty", ctx);
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
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
            if (this.DataType.BaseObjectClass != null)
            {
                return MungeClassName(base.GetBaseClass());
            }
            else
            {
                return "BaseServerDataObject_EntityFramework";
            }
        }

        protected override string[] GetInterfaces()
        {
            return new string[] { this.DataType.ClassName }.Concat(base.GetInterfaces()).ToArray();
        }

        protected override void ApplyNamespaceTailTemplate()
        {
            base.ApplyNamespaceTailTemplate();
            foreach (ObjectReferenceProperty prop in this.DataType.Properties.OfType<ObjectReferenceProperty>().Where(p => p.IsList).ToList().Where(p => p.HasStorage()))
            {
                CallTemplate("Implementation.ObjectClasses.CollectionEntry", ctx, prop);
            }
        }

    }
}
