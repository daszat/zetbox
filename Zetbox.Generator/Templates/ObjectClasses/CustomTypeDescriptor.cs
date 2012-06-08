
namespace Zetbox.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Zetbox.API;
    using Zetbox.App.Base;

    public partial class CustomTypeDescriptor
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx, ObjectClass cls, string ifName, string implName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            string propertyDescriptorName = host.Settings["propertydescriptorname"];
            host.CallTemplate("ObjectClasses.CustomTypeDescriptor", ctx, cls, ifName, implName, propertyDescriptorName);
        }
    }
}
