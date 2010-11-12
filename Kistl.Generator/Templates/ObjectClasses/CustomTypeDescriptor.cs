
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Kistl.API;
    using Kistl.App.Base;

    public partial class CustomTypeDescriptor
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, ObjectClass cls, string implName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            string propertyDescriptorName = host.Settings["propertydescriptorname"];
            host.CallTemplate("ObjectClasses.CustomTypeDescriptor", ctx, cls, implName, propertyDescriptorName);
        }
    }
}
