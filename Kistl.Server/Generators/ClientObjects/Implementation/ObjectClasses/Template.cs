using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public class Template
        : Templates.Implementation.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[] { "Kistl.API.Client" });
        }

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name) + Kistl.API.Helper.ImplementationSuffix;
        }

        protected override string GetBaseClass()
        {
            if (this.ObjectClass.BaseObjectClass != null)
            {
                return MungeClassName(base.GetBaseClass());
            }
            else
            {
                return "BaseClientDataObject";
            }
        }

        protected override void ApplyObjectReferencePropertyTemplate(ObjectReferenceProperty prop)
        {
            //var rel = RelationExtensions.Lookup(ctx, prop);

            //Debug.Assert(rel.A.Navigator.ID == prop.ID || rel.B.Navigator.ID == prop.ID);
            //var relEnd = rel.GetEnd(prop);
            //var otherEnd = rel.GetOtherEnd(relEnd);

            this.WriteLine("        // object reference property");
            Implementation.ObjectClasses.ObjectReferencePropertyTemplate.Call(
                Host, ctx, this.MembersToSerialize,
                prop);
        }

        protected override void ApplyStructPropertyTemplate(StructProperty prop)
        {
            this.WriteLine("        // struct property");
            this.Host.CallTemplate("Implementation.ObjectClasses.StructPropertyTemplate", ctx,
                this.MembersToSerialize,
                prop);
        }

        protected override void ApplyObjectListPropertyTemplate(Relation rel, RelationEndRole endRole)
        {
            Implementation.ObjectClasses.ObjectListProperty.Call(Host, ctx,
                 this.MembersToSerialize,
                 rel.GetEnd(endRole).Navigator as ObjectReferenceProperty);
        }

        protected override void ApplyValueTypeListTemplate(ValueTypeProperty prop)
        {
            this.WriteLine("        // value list property");
            Implementation.ObjectClasses.ValueCollectionProperty.Call(Host, ctx, MembersToSerialize, prop);
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            this.CallTemplate("Implementation.ObjectClasses.UpdateParentTemplate",
                ctx, DataType);
        }
    }
}
