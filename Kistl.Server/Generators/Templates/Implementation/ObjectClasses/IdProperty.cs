using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    /// <summary>
    /// Case: 705, Template Override Typesave machen
    /// Vorschlag: [OverrideTemplate(Kistl.Server.Generators.Templates.Implementation.ObjectClasses.NotifyingValueProperty)]
    /// Alternativ: alle Klassen gelten automatisch als Overrider, wenn sie von dem aufgerufenen Template ableiten.
    /// </summary>
    public class IdProperty
        : NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.IdProperty", ctx);
        }

        public IdProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host, ctx,
                // ID is currently serialized by the infrastructure, so ignore it here
                new Templates.Implementation.SerializationMembersList(),
                // hardcoded type, name, and namespace
                "int", "ID", "http://dasz.at/Kistl")
        {

        }

        protected override MemberAttributes ModifyMethodAttributes(MemberAttributes methodAttributes)
        {
            // add override flag to implement abstract ID member
            return base.ModifyMethodAttributes(methodAttributes) | MemberAttributes.Override;
        }

    }
}
