
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public class IdProperty
        : Templates.Properties.NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.IdProperty", ctx);
        }

        public IdProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host, ctx,
                // ID is currently serialized by the infrastructure, so ignore it here
                new Templates.Serialization.SerializationMembersList(),
                // hardcoded type, name, and namespace
                "int", "ID", "http://dasz.at/Kistl", "_ID")
        {
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            // add override flag to implement abstract ID member
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final | MemberAttributes.Override;
        }

        protected override void ApplyOnGetTemplate()
        {
            this.WriteLine("                if (this.ObjectState != DataObjectState.New)");
            this.WriteLine("                    __result = Proxy.ID;");
            this.WriteLine();
        }

        protected override void ApplyPostSetTemplate()
        {
            this.WriteLine("                if (this.ObjectState != DataObjectState.New)");
            this.WriteLine("                    Proxy.ID = __newValue;");
            this.WriteLine();
        }
    }
}
