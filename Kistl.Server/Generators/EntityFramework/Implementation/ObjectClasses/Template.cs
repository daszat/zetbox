using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;
using System.Diagnostics;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public class Template : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Template
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

        protected override void ApplyPropertyTemplate(Property p)
        {
            if (p is ObjectReferenceProperty)
            {
                var orp = (ObjectReferenceProperty)p;
                var rel = FullRelation.Lookup(ctx, orp);

                Debug.Assert(rel.Right.Navigator == orp || rel.Left.Navigator == orp);
                var isRightEnd = (rel.Right.Navigator == orp);
                var relEnd = isRightEnd ? rel.Right : rel.Left;
                var otherEnd = isRightEnd ? rel.Left : rel.Right;

                switch (rel.GetPreferredStorage())
                {
                    case StorageHint.MergeLeft:
                    case StorageHint.MergeRight:
                    case StorageHint.NoHint:
                    case StorageHint.Replicate:
                        // simple and direct reference

                        if (relEnd.Multiplicity.UpperBound() > 1)
                        {
                            this.Host.CallTemplate("Implementation.ObjectClasses.ListProperty", ctx, relEnd.Referenced, p.GetPropertyType(), p.PropertyName, p);
                        }
                        else
                        {
                            this.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                                orp.PropertyName, rel.GetAssociationName(), relEnd.RoleName, otherEnd.Referenced.GetDataTypeString(), otherEnd.Referenced.GetDataTypeString() + Kistl.API.Helper.ImplementationSuffix);
                        }
                        break;
                    case StorageHint.Separate:
                        // references a CollectionEntry
                        //this.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                        //    orp.PropertyName, 
                        //    isRightEnd ? rel.GetRightToCollectionEntryAssociationName() : rel.GetLeftToCollectionEntryAssociationName(),
                        //    relEnd.RoleName, rel.GetCollectionEntryClassName(), rel.GetCollectionEntryClassName());

                        // TODO: currently delegates to ListProperty but should be replaced by something similar as above, 
                        // but with a different ED implementation
                        base.ApplyPropertyTemplate(p);
                        break;
                    default:
                        throw new InvalidOperationException("unknown StorageHint");
                }
            }
            else
            {
                base.ApplyPropertyTemplate(p);
            }
        }

        protected override void ApplyNamespaceTailTemplate()
        {
            base.ApplyNamespaceTailTemplate();
            var dt = this.DataType;
            foreach (var prop in ctx.GetAssociationPropertiesWithStorage()
                .Where(p => p.ObjectClass == this.DataType))
            {
                string template;
                if (prop is ValueTypeProperty)
                {
                    template = "Implementation.ObjectClasses.ValueCollectionEntry";
                }
                else
                {
                    return;
                }

                CallTemplate(template, ctx, prop);
            }
        }

    }
}
