
namespace Kistl.Generator.Templates.Properties
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Generator.Extensions;

    public partial class PropertyEvents
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Property prop,
            bool isReadOnly)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }

            string eventName = "On" + prop.Name;
            string propType = prop.ReferencedTypeAsCSharp();
            string objType = prop.ObjectClass.GetDataTypeString();

            Call(host, ctx, eventName, propType, objType, true, !isReadOnly);
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            string eventName,
            string propType,
            string objType,
            bool hasGetters,
            bool hasSetters)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.PropertyEvents", ctx, eventName, propType, objType, hasGetters, hasSetters);
        }

        protected override System.CodeDom.MemberAttributes ModifyMemberAttributes(System.CodeDom.MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) | MemberAttributes.Static;
        }
    }
}
