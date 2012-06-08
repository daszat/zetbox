
namespace Zetbox.Generator.Templates.Properties
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;

    public partial class PropertyListChangedEvent
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Property prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }

            string eventName = "On" + prop.Name + "_PostSetter";
            string objType = prop.ObjectClass.GetDataTypeString();

            Call(host, ctx, eventName, objType);
        }

        protected override System.CodeDom.MemberAttributes ModifyMemberAttributes(System.CodeDom.MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) | MemberAttributes.Static;
        }
    }
}
