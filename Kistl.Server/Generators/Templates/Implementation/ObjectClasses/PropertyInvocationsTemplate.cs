using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public partial class PropertyInvocationsTemplate
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Property prop,
            bool isReadOnly)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }

            string eventName = "On" + prop.PropertyName;
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

            host.CallTemplate("Implementation.ObjectClasses.PropertyInvocationsTemplate", ctx, eventName, propType, objType, hasGetters, hasSetters);
        }
    }
}
