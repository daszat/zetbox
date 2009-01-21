using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

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
            foreach (var prop in this.DataType.Properties.OfType<Property>().Where(p => p.HasStorage()).ToList().Where(p => p.IsAssociation()))
            {
                var info = AssociationInfo.CreateInfo(ctx, prop);
                CreateEdmRelationshipAttribute(info);

                var reverse = info.GetReverse();
                if (reverse != null)
                {
                    this.WriteLine("// reversing:");
                    CreateEdmRelationshipAttribute(reverse);
                }
            }
        }

        private void CreateEdmRelationshipAttribute(AssociationInfo info)
        {
            this.WriteLine("// {0}", info.GetType().Name);
            this.WriteLine(
                "[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute(\"Model\", \"{0}\", \"{1}\", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof({2}), \"{3}\", System.Data.Metadata.Edm.RelationshipMultiplicity.{4}, typeof({5}))]",
                info.AssociationName,
                info.ASide.RoleName,
                info.ASide.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix,
                info.BSide.RoleName,
                info.BSide.Multiplicity,
                info.BSide.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix
                );
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
            var dt = this.DataType;
            foreach (var prop in ctx.GetAssociationPropertiesWithStorage()
                .Where(p => p.ObjectClass == this.DataType))
            {
                string template;
                if (prop is ObjectReferenceProperty)
                {
                    template = "Implementation.ObjectClasses.CollectionEntry";
                }
                else if (prop is ValueTypeProperty)
                {
                    template = "Implementation.ObjectClasses.ValueCollectionEntry";
                }
                else
                {
                    throw new InvalidOperationException();
                }
                // dynamic dispatch will cast prop for us and find correct constructor
                CallTemplate(template, ctx, prop);
            }
        }

    }
}
