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
                var rel = NewRelation.Lookup(ctx, orp);

                Debug.Assert(rel.A.Navigator == orp || rel.B.Navigator == orp);
                var relEnd = rel.GetEnd(p);
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
                        if (otherEnd.Multiplicity.UpperBound() > 1)
                        {
                            this.WriteLine("        // object list property");
                            this.Host.CallTemplate("Implementation.ObjectClasses.ListProperty", ctx,
                                relEnd.Type.ToObjectClass(ctx),
                                p.GetPropertyType(),
                                p.PropertyName,
                                p);
                        }
                        else if (otherEnd.Multiplicity.UpperBound() == 1)
                        {
                            this.WriteLine("        // object reference property");
                            this.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                                p.PropertyName,
                                rel.GetAssociationName(), otherEnd.RoleName,
                                otherEnd.Type.NameDataObject,
                                otherEnd.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix);
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                        break;
                    case StorageHint.Separate:
                        this.WriteLine("        // collection reference property");
                        this.CallTemplate("Implementation.ObjectClasses.CollectionEntryListProperty", ctx, relEnd);
                        break;
                    default:
                        throw new NotImplementedException("unknown StorageHint");
                }
            }
            else
            {
                base.ApplyPropertyTemplate(p);
            }
        }

    }
}
