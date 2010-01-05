using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public partial class CalculatedProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            SerializationMembersList serList,
            CalculatedObjectReferenceProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            Call(host, ctx, prop.ObjectClass.GetDataTypeString(), prop.ReferencedClass.GetDataTypeString(), prop.PropertyName, "On" + prop.PropertyName + "_Getter");
        }

        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            string className,
            string referencedType,
            string propertyName,
            string getterEventName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.CalculatedProperty", ctx, className, referencedType, propertyName, getterEventName);
        }
    }
}