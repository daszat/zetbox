
namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Kistl.API;
    using Kistl.App.Base;

    public partial class CustomTypeDescriptor
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, ObjectClass cls,string typeDescriptorClass, string implName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.CustomTypeDescriptor", ctx, cls, typeDescriptorClass, implName);
        }
    }
}
