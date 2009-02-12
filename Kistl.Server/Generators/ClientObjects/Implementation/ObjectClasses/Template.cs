using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public class Template
        : Templates.Implementation.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, Kistl.App.Base.ObjectClass cls)
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
            var rel = NewRelation.Lookup(ctx, prop);

            Debug.Assert(rel.A.Navigator == prop || rel.B.Navigator == prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = relEnd.Other;

            this.WriteLine("        // object reference property");
            this.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                this.MembersToSerialize,
                prop);
        }

        protected override void ApplyObjectReferenceListTemplate(ObjectReferenceProperty prop)
        {
            var rel = NewRelation.Lookup(ctx, prop);

            Debug.Assert(rel.A.Navigator == prop || rel.B.Navigator == prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = relEnd.Other;

            this.WriteLine("        // object list property");
            this.Host.CallTemplate("Implementation.ObjectClasses.ObjectListProperty", ctx,
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

    }
}
