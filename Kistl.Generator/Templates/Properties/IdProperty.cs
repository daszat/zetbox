
namespace Kistl.Generator.Templates.Properties
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    /// <summary>
    /// Case: 705, Template Override Typesave machen
    /// Vorschlag: [OverrideTemplate(Kistl.Generator.Templates.ObjectClasses.NotifyingValueProperty)]
    /// Alternativ: alle Klassen gelten automatisch als Overrider, wenn sie von dem aufgerufenen Template ableiten.
    /// </summary>
    public class IdProperty
        : NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.IdProperty", ctx);
        }

        public IdProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host, ctx,
                // ID is currently serialized by the infrastructure, so ignore it here
                new Serialization.SerializationMembersList(),
                // hardcoded type, name, and namespace
                "int", "ID", "http://dasz.at/Kistl", "_ID", false)
        {
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            // add override flag to implement abstract ID member
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final | MemberAttributes.Override;
        }

        protected override void ApplySecurityCheckTemplate()
        {
            // No security check. there is no information to hide.
        }
    }
}
